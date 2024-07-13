using Microsoft.EntityFrameworkCore;

namespace Recipe.Models
{
    public class RecipeDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RecipeM> Recipes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public RecipeDBContext(DbContextOptions<RecipeDBContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = @"Server=localhost;Database=RecipeDB;Trusted_Connection=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
