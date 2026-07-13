using E_Commerce01.Application.Services.Calsses;
using E_Commerce01.Application.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(c => { }, typeof(ApplicationServicesRegistration).Assembly);
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IBasketService , BasketService>();
            services.AddScoped<ICacheService , CacheService>();
            services.AddScoped<IAuthService , AuthService>();

            return services;
        }
    }
}
