using System.Threading.Tasks;

namespace WebChat.Domain.Interfaces
{
    public interface IMessageService
    {
        public Task SendMessage(string sender, string receiver, string message);
    }
}
