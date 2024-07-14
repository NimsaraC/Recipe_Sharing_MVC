using Microsoft.EntityFrameworkCore;
using System.Configuration;

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
            var connectionString = "Host=ccpa7stkruda3o.cluster-czrs8kj4isg7.us-east-1.rds.amazonaws.com;Port=5432;Username=uc3batrenl2oje;Password=p84188410d7e7f27a02905db276f210ff1c3d4e429840a11a0f6a47b9f200624d;Database=d6hv6qpoffl4i;";
            optionsBuilder.UseNpgsql(connectionString);
        }


    }
}
