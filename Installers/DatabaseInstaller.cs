using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Store.Database;

namespace Store.Installers
{
    public class DatabaseInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString;

            if(Environment.GetEnvironmentVariable("STAGE") == "PRODUCTION") connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            else connectionString = configuration.GetConnectionString("StoreConnection");
            
            services.AddDbContext<DatabaseContext>(opt => opt.UseSqlServer(connectionString));

            services.AddDefaultIdentity<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<DatabaseContext>();

            services.AddControllers().AddNewtonsoftJson(s => {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
        }
    }
}