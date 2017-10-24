using Microsoft.EntityFrameworkCore;
 
namespace CSharp_belt_exam_project_two.Models
{
    public class BrightIdeasContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public BrightIdeasContext(DbContextOptions<BrightIdeasContext> options) : base(options) { }

        public DbSet<User> users { get; set; }
        public DbSet<Idea> ideas { get; set; }
        public DbSet<Like> likes { get; set; }
    }
}