using Microsoft.AspNetCore.Mvc;
using WebChat.Domain.Interfaces;

namespace WebChat.Controllers
{
    [Route("messages")]
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
