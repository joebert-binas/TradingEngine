using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradingEngine.MoneyExchange.ApplicationServices;
using TradingEngine.MoneyExchange.DTO;

namespace TradingEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyController : ControllerBase
    {
        private readonly IMoneyExchangeAppService _moneyExchangeAppService;

        public MoneyController(IMoneyExchangeAppService moneyExchangeAppService)
        {
            _moneyExchangeAppService = moneyExchangeAppService;
        }
        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance(int userId)
        { 
            return Ok(await _moneyExchangeAppService.GetBalanceAsync(userId));
        }

        [HttpPost("request")]
        public async Task<IActionResult> AddMoney([FromBody] MoneyRequesDto moneyRequest)
        { 
            await  _moneyExchangeAppService.AddMoney(moneyRequest);

            return Ok(1);
        }

        [HttpPost("exchange")]
        public async Task<IActionResult> ExchangeMoney([FromBody] MoneyExchangeDto moneyExchangeDto)
        { 

            await _moneyExchangeAppService.ExchangeMoney(moneyExchangeDto);

            return Ok(1);
        }


        [HttpPost("send")]
        public async Task<IActionResult> ExchangeMoney1([FromBody] SendMoneyRequestDto sendMoneyRequestDto)
        {

            await _moneyExchangeAppService.SendMoney(sendMoneyRequestDto);

            return Ok(1);
        }
    }
}