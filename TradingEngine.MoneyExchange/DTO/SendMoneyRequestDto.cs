using System;
using System.Collections.Generic;
using System.Text;

namespace TradingEngine.MoneyExchange.DTO
{
    public class SendMoneyRequestDto
    {
        public SendMoneyRequestDto(int userIdTo, string currencyName, decimal amount, int userId)
        {
            UserIdTo = userIdTo;
            CurrencyName = currencyName;
            Amount = amount;
            UserId = userId;
        }

        public int UserId { get; }
        public int UserIdTo { get; }
        public string CurrencyName { get; }
        public decimal Amount { get;  }

    }
}
