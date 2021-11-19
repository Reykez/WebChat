using System.Threading.Tasks;
using WebChat.Domain.Interfaces;

namespace WebChat.Services
{
    public class MessageService : IMessageService
    {
        public Task SendMessage(string sender, string receiver, string message)
        {
            return null;
        }
    }
}
