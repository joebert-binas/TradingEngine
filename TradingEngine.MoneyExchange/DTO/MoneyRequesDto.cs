using System;
using System.Collections.Generic;
using System.Text;

namespace TradingEngine.MoneyExchange.DTO
{
    public class MoneyRequesDto
    {
        public int UserId { get; set; }
        public string CurrencyName { get; set; }
        public decimal Amount { get; set; }
    }
}
