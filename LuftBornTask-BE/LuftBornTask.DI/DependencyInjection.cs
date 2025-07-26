
using LuftBornTask.Application.Interfaces.Repository;
using LuftBornTask.Application.Interfaces.Services;
using LuftBornTask.Application.Interfaces.UnitOfWork;
using LuftBornTask.Infrastructure.Context;
using LuftBornTask.Infrastructure.Implementation.Repository;
using LuftBornTask.Infrastructure.Implementation.Services;
using LuftBornTask.Infrastructure.Implementation.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LuftBornTask.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProductRepository, ProductEFRepo>();
            services.AddScoped<IProductService, OrdinaryProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
