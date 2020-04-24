using System.Threading.Tasks;
using EasyEvents.Core;
using Microsoft.AspNetCore.Mvc;
using TradingEngine.User.Domain.Events;


namespace TradingEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IEasyEvents _events;

        public UserController(IEasyEvents  events)
        {
            _events = events;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreatedEvent userCreatedEvent)
        {
            await _events.RaiseEventAsync(userCreatedEvent);

            return Ok(1);
        }
    }
}