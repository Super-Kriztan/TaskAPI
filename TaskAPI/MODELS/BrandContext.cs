using Microsoft.EntityFrameworkCore;
namespace TaskAPI.MODELS
{
    public class BrandContext : DbContext
    {
        public BrandContext(DbContextOptions<BrandContext> options) : base(options) 
        { 
        
        }
        public DbSet<TaskModel> Brands { get; set; }
    }
}
