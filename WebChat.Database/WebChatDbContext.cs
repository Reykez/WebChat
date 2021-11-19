using Microsoft.EntityFrameworkCore;
using WebChat.Domain.Entities;

namespace WebChat.Database
{
    public class WebChatDbContext : DbContext, IWebChatDbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(p => p.Username)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(p => p.PasswordHash)
                .IsRequired();
        }

        public WebChatDbContext(DbContextOptions<WebChatDbContext> options) : base(options)
        {
        }
    }
}
