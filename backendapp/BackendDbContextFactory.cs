using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace backendapp
{

    public class BackendDbContextFactory : IDesignTimeDbContextFactory<BackendDbContext>
    {
        public BackendDbContext CreateDbContext(string[] args)
        {
            // var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            // .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true).Build();
            // Console.WriteLine(config);
            // var connectionString = config.GetConnectionString("ConnectionStrings");
            var connectionString = "Server=tcp:sabertestdb.database.windows.net,1433;Initial Catalog=saberdb;Persist Security Info=False;User ID=sabertestdb;Password=Samadii21;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            var optionsBuilder = new DbContextOptionsBuilder<BackendDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new BackendDbContext(optionsBuilder.Options);
        }
    }
}