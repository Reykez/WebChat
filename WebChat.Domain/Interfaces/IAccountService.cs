using WebChat.Domain.Models;

namespace WebChat.Domain.Interfaces
{
    public interface IAccountService
    {
        public void RegisterUser(RegisterUserDto dto);
        public string LoginUser(LoginUserDto dto);
    }
}
