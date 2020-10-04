using Microsoft.AspNetCore.Mvc;
using SaltApi.Models;
using SaltApi.Services;
using System;
using System.ComponentModel;

namespace SaltApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DailyAmountController : ControllerBase
    {
        private readonly IDailyAmountService _dailyAmountService;

        public DailyAmountController(IDailyAmountService dailyAmountService)
        {
            _dailyAmountService = dailyAmountService;
        }

        [HttpGet]
        [Route("{merchant}")]
        [Description("Gets the total amount a merchant is due for a given day.")]
        public DailyAmountModel GetTotalAmountForMerchantByDate(string merchant, DateTime transdate)
        {
            return _dailyAmountService.GetTotalAmountForMerchantByDate(merchant, transdate);
        }
    }
}
