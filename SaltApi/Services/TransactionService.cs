using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using SaltApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SaltApi.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly testContext _dbContext;
        private readonly IConfiguration _configuration;

        public TransactionService(testContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public List<TransactionModel> GetTransactions()
        {
            return _dbContext.Transactions
                .OrderByDescending(t => t.Transdate)
                .Select(t => new TransactionModel()
                {
                    Id = t.Id,
                    Transdate = t.Transdate,
                    Merchant = t.Merchant,
                    Amount = t.Amount,
                    Currency = t.Currency,
                    Pan = t.Pan,
                    Voided = t.Voided
                }).ToList();
        }

        public TransactionModel GetTransactionById(Guid id)
        {
            return _dbContext.Transactions
                .Where(t => t.Id == id)
                .Select(t => new TransactionModel()
                {
                    Id = t.Id,
                    Transdate = t.Transdate,
                    Merchant = t.Merchant,
                    Amount = t.Amount,
                    Currency = t.Currency,
                    Pan = t.Pan,
                    Voided = t.Voided
                }).FirstOrDefault();
        }

        public List<TransactionModel> GetTransactionsForMerchantByDate(string merchant, DateTime transdate)
        {
            return _dbContext.Transactions
                .Where(t => t.Merchant == merchant && 
                t.Transdate.Date == transdate.Date) // only match date of timestamp
                .Select(t => new TransactionModel()
                {
                    Id = t.Id,
                    Transdate = t.Transdate,
                    Merchant = t.Merchant,
                    Amount = t.Amount,
                    Currency = t.Currency,
                    Pan = t.Pan,
                    Voided = t.Voided
                }).ToList();
        }

        public bool VoidTransaction(Guid transactionId, string merchant)
        {
            Transaction transaction = _dbContext.Transactions
                .Where(t => t.Id == transactionId &&
                t.Merchant == merchant)
                .FirstOrDefault();

            if (transaction == null)
            {
                return false;
            }

            /* normal way of doing this with ef core - only works on tables with primary key
            _dbContext.Transactions.Attach(transaction);
            transaction.Voided = true;
            _dbContext.SaveChanges(); */


            // no primary key in table so we have to work around it
            var connection = new NpgsqlConnection(_configuration.GetConnectionString("TestDatabase"));
            
            connection.Open();
            using(var npgsqlTransaction = connection.BeginTransaction())
            {
                var command = connection.CreateCommand();

                command.CommandText = "update transactions set voided = @value where id = @transId";
                command.Parameters.AddWithValue("@value", true);
                command.Parameters.AddWithValue("@transId", transaction.Id);

                int numberOfRows = command.ExecuteNonQuery();

                npgsqlTransaction.Commit();
            }
            connection.Close();

            return true;
        }
    }

    public interface ITransactionService
    {
        List<TransactionModel> GetTransactions();
        TransactionModel GetTransactionById(Guid id);
        List<TransactionModel> GetTransactionsForMerchantByDate(string merchant, DateTime transdate);
        bool VoidTransaction(Guid transactionId, string merchant);
    }
}
