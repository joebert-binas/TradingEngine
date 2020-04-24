using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Shouldly;
using TradingEngine.API.Controllers;
using TradingEngine.MoneyExchange.ApplicationServices;
using TradingEngine.MoneyExchange.DTO;

namespace TradingEngine.API.Tests.Controllers
{
    [TestFixture]
    public class MoneyControllerTests
    {
        private readonly Mock<IMoneyExchangeAppService> _moneyExchangeAppServiceMock;

        public MoneyControllerTests()
        {
            _moneyExchangeAppServiceMock = new Mock<IMoneyExchangeAppService>();
        }

        [Test]
        public async Task When_GetBalance_Then_ShouldReturn_Ok()
        {
            var userId = 0;
            var fixture = new Fixture();

            var balanceResult = fixture.Create<IDictionary<string, decimal>>();

            _moneyExchangeAppServiceMock.Setup(x => x.GetBalanceAsync(userId))
                .Returns(Task.FromResult(balanceResult));

            var moneyController = new MoneyController(_moneyExchangeAppServiceMock.Object);

            var result =  await moneyController.GetBalance(userId);

            result.ShouldBeOfType<OkObjectResult>(); 
        }

        [Test]
        public async Task When_AddMoney_Then_ShouldReturn_Ok()
        { 
            var fixture = new Fixture();
            var moneyRequesDto = new MoneyRequesDto();
             

            _moneyExchangeAppServiceMock.Setup(x => x.AddMoney(moneyRequesDto));

            var moneyController = new MoneyController(_moneyExchangeAppServiceMock.Object);

            var result = await moneyController.AddMoney(moneyRequesDto);

            result.ShouldBeOfType<OkObjectResult>();
        }

        [Test]
        public async Task When_ExchangeMoney_Then_ShouldReturn_Ok()
        {
            var fixture = new Fixture();
            var moneyExchangeDto = new MoneyExchangeDto(); 

            _moneyExchangeAppServiceMock.Setup(x => x.ExchangeMoney(moneyExchangeDto));

            var moneyController = new MoneyController(_moneyExchangeAppServiceMock.Object);

            var result = await moneyController.ExchangeMoney(moneyExchangeDto);

            result.ShouldBeOfType<OkObjectResult>();
        }
    }
}
