using System.Collections.Generic;
using System.Threading.Tasks;
using TradingEngine.MoneyExchange.Domain.Exceptions;
using TradingEngine.MoneyExchange.Domain.Models;
using TradingEngine.MoneyExchange.Domain.Repositories;
using TradingEngine.MoneyExchange.DTO;
using TradingEngine.MoneyExchange.User.Domain.Repositories;

namespace TradingEngine.MoneyExchange.ApplicationServices
{
    public class MoneyExchangeAppService : IMoneyExchangeAppService
    {
        private readonly IUserRepository _userRepository;
        public readonly ICurrencyRepository _currencyRepository; 

        public MoneyExchangeAppService(IUserRepository userRepository, ICurrencyRepository currencyRepository)
        {
            _userRepository = userRepository;
            _currencyRepository = currencyRepository; 
        }

        public async Task<IDictionary<string, decimal>> GetBalanceAsync(int userId)
        {
            var user = await _userRepository.GetAsync(userId);

            return user.GetBalance();
        }

        public async Task AddMoney(MoneyRequesDto moneyRequest)
        {

            var user = await _userRepository.GetAsync(moneyRequest.UserId);

            if (user == null) throw new UserNotFoundException($"UserId={moneyRequest.UserId} is not found.");

            var currency = await _currencyRepository.GetAsync(moneyRequest.CurrencyName);

            user.AddMoney(new Money(moneyRequest.Amount, currency));

            await _userRepository.UpdateBalanceSync(user);
        }

        public async Task ExchangeMoney(MoneyExchangeDto moneyExchangeDto)
        { 
            var user = await _userRepository.GetAsync(moneyExchangeDto.UserId);

            if (user == null)
                throw new UserNotFoundException($"{moneyExchangeDto.UserId} is not found."); 

            var currencyFrom = await _currencyRepository.GetAsync(moneyExchangeDto.CurrencyFrom);
            if (currencyFrom == null) throw new CurrencyNotFoundException($"{moneyExchangeDto.CurrencyFrom} is not found.");

            var currencyTo = await _currencyRepository.GetAsync(moneyExchangeDto.CurrencyTo);
            if (currencyTo == null) throw new CurrencyNotFoundException($"{moneyExchangeDto.CurrencyTo} is not found.");

            user.ExchangeMoney(new Money(moneyExchangeDto.Amount, currencyFrom), currencyTo);

            await _userRepository.UpdateBalanceSync(user);
        }

        public async Task SendMoney(SendMoneyRequestDto sendMoneyDto)
        {
            var user = await _userRepository.GetAsync(sendMoneyDto.UserId);

            if (user == null)
                throw new UserNotFoundException($"{sendMoneyDto.UserId} is not found.");

            var userTo = await _userRepository.GetAsync(sendMoneyDto.UserIdTo);

            if (userTo == null)
                throw new UserNotFoundException($"{sendMoneyDto.UserIdTo} is not found.");
             
            var currency = await _currencyRepository.GetAsync(sendMoneyDto.CurrencyName);

            if (currency == null) throw new CurrencyNotFoundException($"{sendMoneyDto.CurrencyName} is not found.");
 
            user.SendMoney(userTo, new Money(sendMoneyDto.Amount, currency));

            await _userRepository.UpdateBalanceSync(user);
            await _userRepository.UpdateBalanceSync(userTo);
        }


    }
}
