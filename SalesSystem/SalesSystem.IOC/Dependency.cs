﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesSystem.DAL.DBContext;
using SalesSystem.DAL.Repositories;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.Utility;

namespace SalesSystem.IOC
{
    public static class Dependency
    {
        public static void DependencyInyections(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbsalesContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("connSQL"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ISaleRepository, SaleRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}
