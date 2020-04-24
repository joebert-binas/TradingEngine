using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shouldly;
using TradingEngine.MoneyExchange.Domain.Models;

namespace TradingEngine.MoneyExchange.Tests.User.Domain.Models
{
    [TestFixture]
    public class UserTests
    {

        [Test]
        public void When_ExchangeMoney_And_InsufficientBalance_Then_ShouldReturn_Exception()
        {
            try
            {
                var currency = new Currency() { Name = "USD" };

                var user = new MoneyExchange.User.Domain.Models.User()
                {
                    Balances = new List<Balance>(){
                        new Balance()
                        {
                            Currency = currency,
                            Amount = 20
                        }
                    }
                };

                user.ExchangeMoney(new Money(90, currency), currency);

                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.Pass();
            } 
        }

        [Test]
        public void When_GetBalance_And_NoCurrencyFound_Then_ShouldReturn_Null()
        {
            var user = new MoneyExchange.User.Domain.Models.User()
            {
                Balances = new List<Balance>()
            };

            var result = user.GetBalance("");

            result.ShouldBeNull();
        }

        [Test]
        [TestCase(20, 320, 340)]
        [TestCase(500, 20, 520)]
        public void When_AddMoney_Then_ShouldReturn_ExpectedValue(decimal input1, decimal input2, decimal expected)
        {
            var currency = new Currency() { Name = "USD" };

            var user = new MoneyExchange.User.Domain.Models.User()
            {
                Balances = new List<Balance>(){
                    new Balance()
                    {
                        Id = 1,
                        Currency = currency,
                        Amount = 0
                    }
                }
            };

            user.AddMoney(new Money(input1, currency));
            user.AddMoney(new Money(input2, currency));

            user.GetBalance()[currency.Name].ShouldBe(expected);
        }

        [Test]
        [TestCase(500, 200, 300)]
        [TestCase(500, 20, 480)]
        public void When_ExchangeMoney_Then_ShouldReturn_ExpectedValue(decimal balance, decimal amount, decimal expected)
        {
            var currencyFrom = new Currency() { Name = "USD" };
            var currencyTo = new Currency() { Name = "PHP" };

            var user = new MoneyExchange.User.Domain.Models.User()
            {
                Balances = new List<Balance>(){
                    new Balance()
                    {
                        Id = 1,
                        Currency = currencyFrom,
                        Amount = balance
                    }
                }
            };

            user.ExchangeMoney(new Money(amount, currencyFrom), currencyTo);
        
            user.GetBalance()[currencyFrom.Name].ShouldBe(expected);
        }
    }
}
