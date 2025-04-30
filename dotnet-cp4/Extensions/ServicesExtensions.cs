using dotnet_cp4.Configuration;
using dotnet_cp4.Persistence;
using dotnet_cp4.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace dotnet_cp4.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Event>, Repository<Event>>();
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, AppConfiguration appConfiguration)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = appConfiguration.Swagger.Title,
                    Version = "v1",
                    Description = appConfiguration.Swagger.Description,
                    Contact = new OpenApiContact()
                    {
                        Email = appConfiguration.Swagger.Email,
                        Name = appConfiguration.Swagger.Name
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "Teste de licença",
                        Url = new Uri("http://license.com.br")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                swagger.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static IServiceCollection AddContext(this IServiceCollection services, AppConfiguration appConfiguration)
        {
            services.AddDbContext<FIAPDbContext>(options =>
            {
                options.UseOracle(appConfiguration.ConnectionStrings.OracleFIAPDbContext);
            });

            return services;
        }


        public static IServiceCollection AddHealthCheck(this IServiceCollection services, AppConfiguration appConfiguration)
        {

            services
                .AddHealthChecks()
                .AddOracle(appConfiguration.ConnectionStrings.OracleFIAPDbContext, name: "ORACLE")
                .AddUrlGroup(new Uri("https://fiap.com.br"), "FIAP")
                .AddUrlGroup(new Uri("https://viacep.com.br/ws/01001000/json/"), "CEP");

            return services;
        }
    
}
}
