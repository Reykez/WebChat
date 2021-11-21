using Microsoft.EntityFrameworkCore;
using WebChat.Domain.Entities;

namespace WebChat.Database
{
    public interface IWebChatDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        public int SaveChanges();
    }
}
