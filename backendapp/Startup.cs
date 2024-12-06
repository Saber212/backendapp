using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(backendapp.Startup))]

namespace backendapp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:DefaultConnection") ?? string.Empty;
            builder.Services.AddDbContext<BackendDbContext>(options =>
            options.UseSqlServer(connectionString));
        }
    }
}