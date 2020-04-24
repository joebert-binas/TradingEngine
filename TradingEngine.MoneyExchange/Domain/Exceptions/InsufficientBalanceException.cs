using System;

namespace TradingEngine.MoneyExchange.Domain.Exceptions
{
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException(string message) : base(message)
        {
            
        }
    }
}
