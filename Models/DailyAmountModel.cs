using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaltApi.Models
{
    public class DailyAmountModel
    {
        public DateTime Transdate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; }
    }
}
