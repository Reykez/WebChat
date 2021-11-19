using Microsoft.AspNetCore.Mvc;
using WebChat.Domain.Interfaces;
using WebChat.Domain.Models;

namespace WebChat.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody]RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginUserDto dto)
        {
            string token = _accountService.LoginUser(dto);
            return Ok(token);
        }
    }
}
