using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TradingEngine.MoneyExchange.Context;

namespace TradingEngine.MoneyExchange.User.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MoneyExchangeDbContext _dbContext;

        public UserRepository(MoneyExchangeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Models.User> GetAsync(int userId)
        { 
            return await _dbContext.Users
                .Include(x => x.Balances)
                .ThenInclude(x => x.Currency)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task UpdateBalanceSync(Models.User user)
        {
            _dbContext.AttachRange(user.Balances);
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
