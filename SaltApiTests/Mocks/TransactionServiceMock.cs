using SaltApi.Models;
using SaltApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaltApiTests.Mocks
{
    class TransactionServiceMock : ITransactionService
    {
        private readonly List<TransactionModel> _transactions;

        public TransactionServiceMock()
        {
            _transactions = new List<TransactionModel>()
            {
                new TransactionModel()
                {
                    Id = new Guid("e0741dc0-15f7-45ab-81e3-0c3a69570902"),
                    Transdate = DateTime.Parse("2020-02-01T00:00:00"),
                    Merchant = "1234567",
                    Amount = 1000,
                    Currency = "ISK",
                    Pan = "123456******1234",
                    Voided = false
                },
                new TransactionModel()
                {
                    Id = new Guid("12dee4fc-82ce-4904-9606-cbaa5dc1cde9"),
                    Transdate = DateTime.Parse("2020-02-01T07:00:00"),
                    Merchant = "1234567",
                    Amount = 3200,
                    Currency = "ISK",
                    Pan = "234561*****2341",
                    Voided = false
                },
                new TransactionModel()
                {
                    Id = new Guid("ea2d2f68-e8c0-4602-ba71-dd3f61fafa5c"),
                    Transdate = DateTime.Parse("2020-02-01T15:00:00"),
                    Merchant = "1234567",
                    Amount = 1,
                    Currency = "ISK",
                    Pan = "345612******3412",
                    Voided = true
                },
                new TransactionModel()
                {
                    Id = new Guid("e37eb0b1-a296-4acc-b11e-59839a0028e5"),
                    Transdate = DateTime.Parse("2020-02-01T18:00:00"),
                    Merchant = "7654321",
                    Amount = 1000,
                    Currency = "ISK",
                    Pan = "123456******1234",
                    Voided = false
                },
                new TransactionModel()
                {
                    Id = new Guid("3807dc5b-11d0-49a3-b1f1-bde386e820d4"),
                    Transdate = DateTime.Parse("2020-01-31T10:00:00"),
                    Merchant = "1234567",
                    Amount = 1000,
                    Currency = "ISK",
                    Pan = "123456******1234",
                    Voided = false
                }
            };
        }

        public List<TransactionModel> GetTransactions()
        {
            return _transactions;
        }

        public TransactionModel GetTransactionById(Guid id)
        {
            return _transactions.Where(t => t.Id == id).FirstOrDefault();
        }

        public List<TransactionModel> GetTransactionsForMerchantByDate(string merchant, DateTime transdate)
        {
            return _transactions
                .Where(t => t.Merchant == merchant &&
                t.Transdate.Date == transdate.Date)
                .ToList();
        }

        public bool VoidTransaction(Guid transactionId, string merchant)
        {
            TransactionModel transaction = _transactions
                .Where(t => t.Id == transactionId &&
                t.Merchant == merchant)
                .FirstOrDefault();

            if (transaction == null)
            {
                return false;
            }

            transaction.Voided = true;

            return true;
        }
    }
}
