using System;
using System.ComponentModel.DataAnnotations;

namespace SaltApi
{
    public partial class Transaction
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Transdate { get; set; }
        public string Merchant { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Pan { get; set; }
        public bool Voided { get; set; }
    }
}
