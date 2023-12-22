using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesSystem.DAL.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
