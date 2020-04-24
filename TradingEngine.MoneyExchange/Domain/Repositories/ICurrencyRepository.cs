using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradingEngine.MoneyExchange.Domain.Models;

namespace TradingEngine.MoneyExchange.Domain.Repositories
{
    public interface ICurrencyRepository
    {
        Task<Currency> GetAsync(string currencyName);
    }
}
