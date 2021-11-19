using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebChat.Domain.Interfaces;

namespace WebChat.Controllers
{
    [Route("messages")]
    [Authorize] // Now we using 'authorize'. In the future - better option is use AuthorizationHandler. 
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("send")]
        public ActionResult SendMessage()
        {
            _messageService.SendMessage("", "", "");
            return Ok();
        }
    }
}
