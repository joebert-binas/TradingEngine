using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using TradingEngine.MoneyExchange.Domain.Models;

namespace TradingEngine.MoneyExchange.Context
{
    public class DataSeeder
    { 
        public static void SeedCurrencies(MoneyExchangeDbContext dbContext)
        {
            if (!dbContext.Currencies.Any())
            {

                var currencies = new List<Currency>()
                {
                    new Currency()
                    {
                        Name = "USD",
                        Ratio = 1
                    },
                    new Currency()
                    {
                        Name = "EUR",
                        Ratio = 1.5m
                    }
                };


                dbContext.AddRange(currencies);
                dbContext.SaveChanges(); 
            }
        }

    }
}
