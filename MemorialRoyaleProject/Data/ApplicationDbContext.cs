using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MemorialRoyaleProject.Models;

namespace MemorialRoyaleProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MemorialRoyaleProject.Models.Memorial> Memorial { get; set; }
        public DbSet<MemorialRoyaleProject.Models.News> News { get; set; }
        public DbSet<MemorialRoyaleProject.Models.WorkExample> WorkExample { get; set; }
    }
}