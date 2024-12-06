using Microsoft.EntityFrameworkCore;

namespace backendapp
{
    public class BackendDbContext : DbContext
    {
        public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options)
        {
            Users = Set<Users>();
        }
        public DbSet<Users> Users { get; set; }
    }
}