using System;

namespace TradingEngine.MoneyExchange.Domain.Exceptions
{
    public class CurrencyNotFoundException : Exception
    {
        public CurrencyNotFoundException(string message) : base(message)
        {
            
        }
    }
}
