using HtmlEncodeTests.IntegrationTests.Encode;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace HtmlEncodeTests.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "Order Service Project",
                    Description = "Backend Api For Order",
                });
                c.DescribeAllParametersInCamelCase();
                c.CustomSchemaIds(t => t.Name);
            });
            services.AddSingleton(new List<TypedModel>());
            services.AddTransient<IEncodedContentContainer, EncodedContentContainer>();
            services.AddControllers(options =>
            {
                options.Filters.Add(new HtmlEncodeActionFilterAttribute());
            }).AddNewtonsoftJson();


        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Service API V1");
            });
            app.UseRouting();
            app.UseEndpoints(endpoints =>
                endpoints.MapDefaultControllerRoute()
                );
        }
    }
}
