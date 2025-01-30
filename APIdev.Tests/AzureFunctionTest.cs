using System;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using DAL;
using APIDev;
using static APIdevAzureFunctions.OrderShippedAF;
using Assert = Xunit.Assert;

namespace APIDev.Tests
{
    public class UpdateInvoiceShippedDateTests
    {
        [Fact]
        public async Task Run_ShouldUpdateShippedDate_WhenInvoiceExists()
        {
            // Arrange
            var mockDbContext = new Mock<CustomerContext>();
            var mockLogger = new Mock<ILogger>();
            var invoiceId = 123;

            // Create a fake invoice
            var fakeInvoice = new Invoice
            {
                InvoiceId = invoiceId,
                OrderDate = DateTime.UtcNow,
                OrderShippedDate = null
            };

            // Mock DbSet
            var mockInvoices = new Mock<DbSet<Invoice>>();
            mockInvoices.Setup(m => m.FindAsync(invoiceId))
                .ReturnsAsync(fakeInvoice);

            // Mock DbContext behavior
            mockDbContext.Setup(c => c.Invoices).Returns(mockInvoices.Object);

            // Create the function
            var function = new UpdateInvoiceShippedDate(mockDbContext.Object);

            // Mock HTTP Request
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var result = await function.Run(mockHttpRequest.Object, invoiceId, mockLogger.Object);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Contains("shipped date updated", okResult.Value.ToString());
            Assert.NotNull(fakeInvoice.OrderShippedDate); // Ensure date is updated
        }

        [Fact]
        public async Task Run_ShouldReturnNotFound_WhenInvoiceDoesNotExist()
        {
            // Arrange
            var mockDbContext = new Mock<CustomerContext>();
            var mockLogger = new Mock<ILogger>();
            var invoiceId = 999;

            // Mock DbSet
            var mockInvoices = new Mock<DbSet<Invoice>>();
            mockInvoices.Setup(m => m.FindAsync(invoiceId))
                .ReturnsAsync((Invoice)null); // Simulate not found

            // Mock DbContext behavior
            mockDbContext.Setup(c => c.Invoices).Returns(mockInvoices.Object);

            // Create the function
            var function = new UpdateInvoiceShippedDate(mockDbContext.Object);

            // Mock HTTP Request
            var mockHttpRequest = new Mock<HttpRequest>();

            // Act
            var result = await function.Run(mockHttpRequest.Object, invoiceId, mockLogger.Object);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains("not found", notFoundResult.Value.ToString());
        }
    }
}
