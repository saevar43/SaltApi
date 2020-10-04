using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SaltApi.Models;
using SaltApi.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SaltApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        [Description("Gets all transactions.")]
        public IEnumerable<TransactionModel> GetTransactions()
        {
            return _transactionService.GetTransactions();
        }

        [HttpGet]
        [Route("{id}")]
        [Description("Gets a single transaction by Id.")]
        public TransactionModel GetTransactionById(Guid id)
        {
            return _transactionService.GetTransactionById(id);
        }

        [HttpGet]
        [Route("{merchant}/transactions")]
        [Description("Gets all transactions for merchant on a given day.")]
        public IEnumerable<TransactionModel> GetTransactionsForMerchantByDate(string merchant, DateTime transdate)
        {
            return _transactionService.GetTransactionsForMerchantByDate(merchant, transdate);
        }

        [HttpPatch]
        [Route("{merchant}/{transactionId}")]
        [Description("Voids a merchant's transaction with given transaction Id.")]
        public ActionResult VoidTransaction(Guid transactionId, string merchant)
        {
            bool result = _transactionService.VoidTransaction(transactionId, merchant);

            if (!result) { return NotFound(); }
            
            return Ok(this.GetTransactionById(transactionId));
        }
    }
}
