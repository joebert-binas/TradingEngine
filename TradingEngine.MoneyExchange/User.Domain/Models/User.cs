using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading;
using TradingEngine.MoneyExchange.Domain.Exceptions;
using TradingEngine.MoneyExchange.Domain.Models;

namespace TradingEngine.MoneyExchange.User.Domain.Models
{
    public class User 
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public ICollection<Balance> Balances { get; set; } 
        public IDictionary<string, decimal> GetBalance()
        {
            return Balances != null ? Balances.ToDictionary(x => x.Currency.Name, x => x.Amount) : new Dictionary<string, decimal>();
        }

        public Balance GetBalance(string currencyName)
        {
            return Balances.FirstOrDefault(x => x.Currency.Name == currencyName); 
        }

        public void AddMoney(Money money)
        {
            var balance = GetBalance(money.Currency.Name) ?? new Balance();
            balance.AddMoney(money);

            if (balance.Id == 0)
            {
               Balances.Add(balance);
            }
        }
       
        public void ExchangeMoney(Money money, Currency currency)
        {
            var balance = GetBalance(money.Currency.Name);

            if (balance == null)
                throw new CurrencyNotFoundException($"{money.Currency.Name} is not found.");

            if(balance.Amount < money.Amount) 
                throw new InsufficientBalanceException($"Insufficient balance");

            balance.ChargeMoney(money);
             
            AddMoney(new Money(money.Amount * currency.Ratio, currency)); 
        }

        public void ReceiveMoney(Money money)
        {
            AddMoney(money);
        }

        public void SendMoney(User userTo, Money money)
        {
            var balance = GetBalance(money.Currency.Name);

            balance.ChargeMoney(money);

            userTo.ReceiveMoney(money);
        }

    }
}
