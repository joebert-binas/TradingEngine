using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TradingEngine.MoneyExchange.User.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<Models.User> GetAsync(int userId);
        Task UpdateBalanceSync(Models.User user);
    }
}
