using System;
using WebChat.Domain.Interfaces;

namespace WebChat.Domain.Entities
{
    public class Message : IEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int SenderId { get; set; }
        public virtual User Sender { get; set; }

        public int ReceiverId { get; set; }
        public virtual User Receiver { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}