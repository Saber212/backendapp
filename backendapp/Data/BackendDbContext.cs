using Microsoft.EntityFrameworkCore;

namespace backendapp
{
    public class BackendDbContext : DbContext
    {
        public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options)
        {
            Users = Set<Users>();
            Tasks = Set<Tasks>();
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
    }
}