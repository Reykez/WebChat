using System.Collections.Generic;
using WebChat.Domain.Interfaces;

namespace WebChat.Domain.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        
        public virtual ICollection<Message> MessagesSent { get; set; }
        public virtual ICollection<Message> MessagesReceived { get; set; }
    }
}
