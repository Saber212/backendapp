using backendapp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults().ConfigureServices((context, services) =>
    {
        services.AddDbContext<BackendDbContext>(options =>
        options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));
    })
    .Build();

host.Run();
