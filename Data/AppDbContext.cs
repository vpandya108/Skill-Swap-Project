using Microsoft.EntityFrameworkCore;
using Skill_Swap_Project.Models;

namespace Skill_Swap_Project.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<SwapRequest> SwapRequests { get; set; }
    }
}
