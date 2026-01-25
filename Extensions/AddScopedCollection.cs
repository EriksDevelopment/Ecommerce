using Ecommerce_Api.Core.Interfaces;
using Ecommerce_Api.Core.Security;
using Ecommerce_Api.Core.Service;
using Ecommerce_Api.Data.Interfaces;
using Ecommerce_Api.Data.Repositories;

namespace Ecommerce_Api.Extensions
{
    public static class AddScopedCollection
    {
        public static IServiceCollection AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped<JwtService>();

            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IWalletRepo, WalletRepo>();

            return services;
        }
    }
}