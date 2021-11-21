using Microsoft.EntityFrameworkCore;
using WebChat.Domain.Entities;

namespace WebChat.Database
{
    public class WebChatDbContext : DbContext, IWebChatDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(p => p.Username)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(p => p.PasswordHash)
                .IsRequired();

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender).WithMany(m => m.MessagesSent).IsRequired().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver).WithMany(m => m.MessagesReceived).IsRequired().OnDelete(DeleteBehavior.NoAction);
        }

        public WebChatDbContext(DbContextOptions<WebChatDbContext> options) : base(options)
        {
        }
    }
}
