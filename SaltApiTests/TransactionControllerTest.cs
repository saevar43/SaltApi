using Microsoft.AspNetCore.Mvc;
using SaltApi.Controllers;
using SaltApi.Models;
using SaltApi.Services;
using SaltApiTests.Mocks;
using System;
using System.Collections.Generic;
using Xunit;

namespace SaltApiTests
{
    public class TransactionControllerTest
    {
        TransactionController _controller;
        ITransactionService _service;

        public TransactionControllerTest()
        {
            _service = new TransactionServiceMock();
            _controller = new TransactionController(_service);
        }

        /* GetTransactions tests */
        [Fact]
        public void GetTransactions_WhenCalled_ReturnsAllItems()
        {
            // Act
            var result = _controller.GetTransactions();

            // Assert
            var transactions = Assert.IsType<List<TransactionModel>>(result);
            Assert.Equal(5, transactions.Count);
        }

        /* GetTransactionsById tests */
        [Fact]
        public void GetTransactionById_WhenCalled_ReturnsCorrectTransaction()
        {
            // Arrange
            var testGuid = new Guid("e0741dc0-15f7-45ab-81e3-0c3a69570902");

            // Act
            var result = _controller.GetTransactionById(testGuid);

            // Assert
            Assert.IsType<TransactionModel>(result);
            Assert.Equal(testGuid, (result as TransactionModel).Id);
        }

        /* GetTransactionsForMerchantByDate tests */
        [Fact]
        public void GetTransactionsForMerchantByDate_WhenCalled_ReturnsCorrectAmount()
        {
            // Arrange
            var testDate = DateTime.Parse("2020-02-01");
            var testMerchant = "1234567";

            // Act
            var result = _controller.GetTransactionsForMerchantByDate(testMerchant, testDate);

            // Assert
            var transactions = Assert.IsType<List<TransactionModel>>(result);
            Assert.Equal(3, transactions.Count);
        }

        /* VoidTransaction tests */
        [Fact]
        public void VoidTransaction_UnkownGuidPassed_ReturnsNotFound()
        {
            // Arrange
            var testMerchant = "7654321";

            // Act
            var notFound = _controller.VoidTransaction(Guid.NewGuid(), testMerchant);

            // Assert
            Assert.IsType<NotFoundResult>(notFound);
        }

        [Fact]
        public void VoidTransaction_UnkownMerchantPassed_ReturnsNotFound()
        {
            // Arrange
            var testMerchant = "1111111";
            var testGuid = new Guid("e37eb0b1-a296-4acc-b11e-59839a0028e5");

            // Act
            var notFound = _controller.VoidTransaction(testGuid, testMerchant);

            // Assert
            Assert.IsType<NotFoundResult>(notFound);
        }

        [Fact]
        public void VoidTransaction_ExistingGuidMerchantPassed_ReturnsOkResult()
        {
            // Arrange
            var testMerchant = "7654321";
            var testGuid = new Guid("e37eb0b1-a296-4acc-b11e-59839a0028e5");

            // Act
            var ok = _controller.VoidTransaction(testGuid, testMerchant);

            // Assert
            Assert.IsType<OkObjectResult>(ok);
        }

        [Fact]
        public void VoidTransaction_ExistingGuidMerchantPassed_ReturnsRightTransaction()
        {
            // Arrange
            var testMerchant = "7654321";
            var testGuid = new Guid("e37eb0b1-a296-4acc-b11e-59839a0028e5");

            // Act
            var ok = _controller.VoidTransaction(testGuid, testMerchant) as OkObjectResult;

            // Assert
            Assert.IsType<TransactionModel>(ok.Value);
            Assert.Equal(testGuid, (ok.Value as TransactionModel).Id);
        }
    }
}
