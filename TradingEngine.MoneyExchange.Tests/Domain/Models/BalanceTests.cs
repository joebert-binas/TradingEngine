using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture;
using NUnit.Framework;
using Shouldly;
using TradingEngine.MoneyExchange.Domain.Models;

namespace TradingEngine.MoneyExchange.Tests.Domain.Models
{
    [TestFixture]
    public class BalanceTests
    {

        [Test]
        [TestCase(20, 320, 340)]
        [TestCase(500, 20, 520)]
        public void When_AddMoney_Then_ShouldReturn_ExpectedValue(decimal input1, decimal input2, decimal expected)
        {
            var balance = new Balance();

            balance.AddMoney(new Money(input1, new Currency()));
            balance.AddMoney(new Money(input2, new Currency()));

            balance.Amount.ShouldBe(expected); 
        }

        [Test]
        [TestCase(60, 20, -80)]
        [TestCase(500, 20, -520)]
        public void When_ChargeMoney_Then_ShouldReturn_ExpectedValue(decimal input1, decimal input2, decimal expected)
        {
            var balance = new Balance();

            balance.ChargeMoney(new Money(input1, new Currency()));
            balance.ChargeMoney(new Money(input2, new Currency()));

            balance.Amount.ShouldBe(expected);
        }
    }
}
