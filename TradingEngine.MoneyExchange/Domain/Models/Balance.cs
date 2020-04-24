namespace TradingEngine.MoneyExchange.Domain.Models
{
    public class Balance
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CurrencyId { get; set; }
        public decimal Amount { get; set; }
        public User.Domain.Models.User User { get; set; }
        public Currency Currency { get; set; } 
        public void AddMoney(Money money)
        { 
            Amount += money.Amount;
            Currency = money.Currency;
        } 
        public void ChargeMoney(Money money)
        {
            Amount -= money.Amount;
            Currency = money.Currency; 
        }
    }
}
