using Microsoft.OpenApi.Models;

namespace Ecommerce_Api.Extensions
{
    public static class AddSwaggerGenCollection
    {
        public static IServiceCollection AddSwaggerGenServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Ecommerce API",
                    Version = "v1"
                });
            });

            return services;
        }
    }
}