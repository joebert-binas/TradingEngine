using Microsoft.EntityFrameworkCore;
using TradingEngine.MoneyExchange.Domain.Models;

namespace TradingEngine.MoneyExchange.Context
{
    public class MoneyExchangeDbContext : DbContext
    {
        public MoneyExchangeDbContext()
        {
            
        }
        public MoneyExchangeDbContext(DbContextOptions<MoneyExchangeDbContext> options) : base(options) { }

        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Balance> Balances { get; set; }
        public virtual DbSet<User.Domain.Models.User> Users { get; set; } 
 

    }
}
