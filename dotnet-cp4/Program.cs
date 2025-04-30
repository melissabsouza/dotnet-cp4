using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using HealthChecks.UI.Client;
using dotnet_cp4.Configuration;
using dotnet_cp4.Extensions;

namespace dotnet_cp4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuração das opções do app
            IConfiguration configuration = builder.Configuration;
            AppConfiguration appConfiguration = new AppConfiguration();
            configuration.Bind(appConfiguration);
            builder.Services.Configure<AppConfiguration>(configuration);

            // Adiciona os serviços ao contêiner
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddContext(appConfiguration);

            builder.Services.AddRepositories();

            builder.Services.AddSwagger(appConfiguration);

            builder.Services.AddHealthCheck(appConfiguration);

            builder.Services.AddHealthChecksUI().AddInMemoryStorage();

            var app = builder.Build();

            // Usando o Swagger apenas no ambiente de desenvolvimento
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configuração dos middlewares
            app.UseHttpsRedirection();
            app.UseAuthorization();

            // Mapeamento de controllers da API
            app.MapControllers();

            // Configuração do HealthCheck
            app.MapHealthChecks("/health-check", new HealthCheckOptions()
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            // Configuração da UI de HealthCheck
            app.UseHealthChecksUI(options =>
            {
                options.UIPath = "/healthcheck-ui";
            });

            // Inicia a aplicação
            app.Run();
        }
    }
}
