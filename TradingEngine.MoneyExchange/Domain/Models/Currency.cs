using System.Collections.Generic;

namespace TradingEngine.MoneyExchange.Domain.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Ratio { get; set; } 
    }
}
