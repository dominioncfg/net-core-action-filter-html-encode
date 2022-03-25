using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;

namespace HtmlEncodeTests.IntegrationTests.Encode
{
    public class HtmlEncodeActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!HttpMethodApplies(context.HttpContext.Request))
                return;

            if (IsBeingExplicitMarkAsIgnored(context))
                return;

            var inspectArguments = context.ActionArguments
                .Where(x => ShouldInspectObject(x.Value))
                .ToList();

            if (!inspectArguments.Any())
                return;

            var ignoreFields = LoadIgnoreFields(context);

            foreach (var argument in inspectArguments)
            {
                var value = argument.Value;
                switch (value)
                {
                    case IRequireHtlmEncoding encodeObject:
                        InspectAndEncodeObjectSringProperties(encodeObject, ignoreFields);
                        break;
                }
            }

        }

        private static bool IsBeingExplicitMarkAsIgnored(ActionExecutingContext context)
        {
            return context
                .ActionDescriptor
                .EndpointMetadata
                .Any(x => x is NeverEncodeAsHtmlAttribute);
        }

        private static HashSet<string> LoadIgnoreFields(ActionExecutingContext context)
        {
            var attr = context
                .ActionDescriptor
                .EndpointMetadata
                .OfType<IgnoreEncodeAsHtmlAttribute>()
                .FirstOrDefault();

            if (attr is null || attr.Fields is null)
                return new HashSet<string>();

            return attr
                .Fields
                .Where(x => (!string.IsNullOrWhiteSpace(x)))
                .ToHashSet();
        }

        private static bool HttpMethodApplies(HttpRequest request)
        {
            var method = request.Method;
            return method == "GET" || method == "POST" || method == "PUT";
        }

        private static bool ShouldInspectObject(object? value)
        {
            if (value is null)
                return false;

            bool hasIgnoreAttribute = value.GetType().GetCustomAttribute<NeverEncodeAsHtmlAttribute>() is not null;
            if (hasIgnoreAttribute)
                return false;

            return value is IRequireHtlmEncoding;
        }

        private static void InspectAndEncodeObjectSringProperties(IRequireHtlmEncoding objectToEncodeStringProperties, HashSet<string> ignoreFields, string path = "")
        {
            if (objectToEncodeStringProperties == null)
                return;

            var propertiesToInspect = objectToEncodeStringProperties
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.GetCustomAttribute<NeverEncodeAsHtmlAttribute>() is null)
                .ToList();

            foreach (var property in propertiesToInspect)
            {
                string propertyName = property.Name;
                string innerPath = string.IsNullOrEmpty(path) ? propertyName : $"{path}.{propertyName}";
                if (ignoreFields.Contains(innerPath))
                    continue;


                if (property.PropertyType == typeof(string))
                {
                    if (property.SetMethod != null)
                    {
                        string? strValue = (string?)property.GetValue(objectToEncodeStringProperties);
                        property.SetValue(objectToEncodeStringProperties, HtmlEnconde(strValue));
                    }
                }
                else
                {

                    object? propertyValue = property.GetValue(objectToEncodeStringProperties);
                    if (ShouldInspectObject(propertyValue))
                    {
                        switch (propertyValue)
                        {
                            case IRequireHtlmEncoding encodeInnerObject:
                                
                                InspectAndEncodeObjectSringProperties(encodeInnerObject, ignoreFields, innerPath);
                                break;
                        }
                    }

                }
            }
        }

        private static string? HtmlEnconde(string? original)
        {
            return original is null ? null : WebUtility.HtmlEncode(original);
        }
    }
}
