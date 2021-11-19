using System.Collections.Generic;
using System.Linq;
using WebChat.Domain.Entities;

namespace WebChat.Database
{
    class WebChatDbSeeder
    {
        private readonly IWebChatDbContext _dbContext;

        public WebChatDbSeeder(IWebChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if(!_dbContext.Users.Any())
            {
                var users = GetUsers();
                _dbContext.Users.AddRange(users);
                _dbContext.SaveChanges();
            }
        }

        private List<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                   Username = "root",
                   PasswordHash = "passwordHash" // type password hash here!
                }
            };
            return users;
        }
    }
}
