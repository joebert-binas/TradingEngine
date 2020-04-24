using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TradingEngine.MoneyExchange.ApplicationServices;
using TradingEngine.MoneyExchange.Context;
using TradingEngine.MoneyExchange.Domain.Models;

namespace TradingEngine.MoneyExchange.Domain.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly MoneyExchangeDbContext _dbContext;

        public CurrencyRepository(MoneyExchangeDbContext dbContext)
        {
            _dbContext = dbContext;
        } 

        public async Task<Currency> GetAsync(string currencyName)
        {
            return await _dbContext.Currencies.FirstOrDefaultAsync(x => x.Name == currencyName);
        }
    }
}
