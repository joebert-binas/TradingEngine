using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using NUnit.Framework;
using Remotion.Linq.Parsing.ExpressionVisitors.Transformation.PredefinedTransformations;
using TradingEngine.MoneyExchange.ApplicationServices;
using TradingEngine.MoneyExchange.Domain.Models;
using TradingEngine.MoneyExchange.Domain.Repositories;
using TradingEngine.MoneyExchange.DTO;
using TradingEngine.MoneyExchange.User.Domain.Models;
using TradingEngine.MoneyExchange.User.Domain.Repositories;

namespace Tests.ApplicationServices
{
    [TestFixture]
    public class MoneyExchangeAppServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ICurrencyRepository> _currencyRepository;

        public MoneyExchangeAppServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _currencyRepository = new Mock<ICurrencyRepository>(); 
        }

        [Test]
        public async Task When_GetBalance_Then_ShouldReturn_Dictionary()
        {
            var fixture = new Fixture();

            var userId = fixture.Create<int>();

            var user = fixture.Build<User>().With(x => x.Balances, new List<Balance>()).Create(); 
            
            _userRepositoryMock.Setup(x => x.GetAsync(userId)).Returns(Task.FromResult(user));

            var moneyExchangeService =
                new MoneyExchangeAppService(_userRepositoryMock.Object, _currencyRepository.Object);

            var result = await moneyExchangeService.GetBalanceAsync(userId);
             
            Assert.AreEqual(typeof(Dictionary<string, decimal>), result.GetType());
        }

        [Test]
        public async  Task When_AddMoney_And_NoUserFound_Then_ShouldReturn_Exception()
        { 
            var userId = 0;
            var moneyRequest = new MoneyRequesDto();
            User user = null;

            try
            {
                _userRepositoryMock.Setup(x => x.GetAsync(userId)).Returns(Task.FromResult(user));

                var moneyExchangeService =
                    new MoneyExchangeAppService(_userRepositoryMock.Object, _currencyRepository.Object);

                 await moneyExchangeService.AddMoney(moneyRequest);

                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.Pass();
            }
        }


        [Test]
        public async Task When_SendMoney_And_NoCurrencyFound_Then_ShouldReturn_Exception()
        {
            var fixture = new Fixture();
            var userId = 0;
            var sendMoneyDto = new SendMoneyRequestDto(0,null,0, 0);
            var user = fixture.Build<User>().With(x => x.Balances, new List<Balance>()).Create();
            Currency currency = null;

            try
            {
                _userRepositoryMock.Setup(x => x.GetAsync(userId)).Returns(Task.FromResult(user));
                _currencyRepository.Setup(x => x.GetAsync(sendMoneyDto.CurrencyName))
                    .Returns(Task.FromResult(currency));

                var moneyExchangeService =
                    new MoneyExchangeAppService(_userRepositoryMock.Object, _currencyRepository.Object);

                await moneyExchangeService.SendMoney(sendMoneyDto);

                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.Pass();
            }
        }
    }
}