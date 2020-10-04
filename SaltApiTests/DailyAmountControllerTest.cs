using SaltApi.Controllers;
using SaltApi.Models;
using SaltApi.Services;
using SaltApiTests.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SaltApiTests
{
    public class DailyAmountControllerTest
    {
        DailyAmountController _controller;
        IDailyAmountService _service;

        public DailyAmountControllerTest()
        {
            _service = new DailyAmountServiceMock();
            _controller = new DailyAmountController(_service);
        }

        /* GetTotalAmountForMerchantByDay */
        [Fact]
        public void GetTotalAmountForMerchantByDay_WhenCalled_ReturnsCorrectValues()
        {
            // Arrange
            var testMerchant = "1234567";
            var testDate = DateTime.Parse("2020-02-01");

            // Act
            var result = _controller.GetTotalAmountForMerchantByDate(testMerchant, testDate);

            // Assert
            Assert.IsType<DailyAmountModel>(result);
            Assert.Equal(4200, result.TotalAmount);
            Assert.Equal("ISK", result.Currency);
        }
    }
}
