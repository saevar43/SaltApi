using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaltApi.Models
{
    public class TransactionModel
    {
        public Guid Id { get; set; }
        public DateTime Transdate { get; set; }
        public string Merchant { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Pan { get; set; }
        public bool Voided { get; set; }
    }
}
