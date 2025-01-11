using Microsoft.EntityFrameworkCore;
using ProjectB;

namespace ProjectB.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Models.SignUp> SignUps { get; set; }
        public DbSet<Models.Lessons> Lessons { get; set; }
    }
}
