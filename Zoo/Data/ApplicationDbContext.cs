using Microsoft.EntityFrameworkCore;
using Zoo.Models;


namespace Zoo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Arraza> Arrazak { get; set; }

        public DbSet<Lekua> Lekuak { get; set; }
    }
}
