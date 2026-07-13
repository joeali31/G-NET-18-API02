using E_Commerce01.Application.Services.Contracts;
using E_Commerce01.Domain.Contract;
using E_Commerce01.Domain.Contract.Repositories;
using E_Commerce01.Domain.Identity;
using E_Commerce01.Infrastructure.Data;
using E_Commerce01.Infrastructure.DataSeeding;
using E_Commerce01.Infrastructure.Identity.Data;
using E_Commerce01.Infrastructure.Identity.Services;
using E_Commerce01.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_Commerce01.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<StoreIdentityDbContext>();

            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();

            services.AddKeyedScoped<IDataSeeder, CatalogDataSeeder>("Catalog");
            services.AddKeyedScoped<IDataSeeder, IdentityDataSeeder>("Identity");
            services.AddScoped<IUnitOfWork , UnitOfWork>();

            services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"));
            });

            services.AddScoped<IBasketRepository , BasketRepository>();
            services.AddScoped<ICacheRepository , CacheRepository>();
            services.AddScoped<IIdentityService , IdentityService>();

            return services;
        }
    }
}
