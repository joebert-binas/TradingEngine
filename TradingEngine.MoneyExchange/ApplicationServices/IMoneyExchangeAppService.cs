using System.Collections.Generic;
using System.Threading.Tasks;
using TradingEngine.MoneyExchange.DTO;

namespace TradingEngine.MoneyExchange.ApplicationServices
{
    public interface IMoneyExchangeAppService
    {
        Task AddMoney(MoneyRequesDto moneyRequest);
        Task<IDictionary<string, decimal>> GetBalanceAsync(int userId);
        Task ExchangeMoney(MoneyExchangeDto moneyExchangeDto);
        Task SendMoney(SendMoneyRequestDto sendMoneyDto);
    }
}
