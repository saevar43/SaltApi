using SaltApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaltApi.Services
{
    public class DailyAmountService : IDailyAmountService
    {
        private readonly testContext _dbContext;

        public DailyAmountService(testContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DailyAmountModel GetTotalAmountForMerchantByDate(string merchant, DateTime transdate)
        {
            List<Transaction> transactions = _dbContext.Transactions
                .Where(t => t.Merchant == merchant &&
                t.Transdate.Date == transdate.Date &&
                t.Voided == false)
                .ToList();

            DailyAmountModel totalAmount = new DailyAmountModel()
            {
                Transdate = transdate.Date,
                TotalAmount = CalculateTotalAmount(transactions),
                Currency = transactions.FirstOrDefault().Currency // currency is uniform for merchant
            };

            return totalAmount;
        }

        /* PRIVATE HELPER FUNCTIONS */
        private decimal CalculateTotalAmount(List<Transaction> transactions)
        {
            decimal totalAmount = 0;

            foreach (Transaction t in transactions)
            {
                totalAmount += t.Amount;
            }

            return totalAmount;
        }
    }

    public interface IDailyAmountService
    {
        public DailyAmountModel GetTotalAmountForMerchantByDate(string merchant, DateTime transdate);
    }
}
