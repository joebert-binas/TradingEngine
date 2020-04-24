using System;
using System.Collections.Generic;
using System.Text;

namespace TradingEngine.MoneyExchange.DTO
{
    public class MoneyExchangeDto
    {
        public int UserId { get; set; }
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
        public decimal Amount { get; set; }
    }
}
