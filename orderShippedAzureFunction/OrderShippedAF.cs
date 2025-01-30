using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace APIdevAzureFunctions
{
    public class OrderShippedAF
    {
        public class UpdateInvoiceShippedDate
        {
            private readonly CustomerContext _customersContext;

            public UpdateInvoiceShippedDate(CustomerContext customersContext)
            {
                _customersContext = customersContext;
            }

            [Function("UpdateInvoiceShippedDate")]
            public async Task<IActionResult> Run(
                [HttpTrigger(AuthorizationLevel.Function, "put", Route = "invoice/{invoiceId}/ship")] HttpRequest req,
                int invoiceId,
                ILogger log)
            {
                log.LogInformation($"Processing request to update shipped date for invoice ID: {invoiceId}");

                // Retrieve the invoice by ID
                var invoice = await _customersContext.Invoices.FirstOrDefaultAsync(i => i.InvoiceId == invoiceId);

                if (invoice == null)
                {
                    log.LogWarning($"Invoice with ID {invoiceId} not found.");
                    return new NotFoundObjectResult($"Invoice with ID {invoiceId} not found.");
                }

                // Update the shipped date to the current date
                invoice.OrderShippedDate = DateTime.UtcNow;

                try
                {
                    await _customersContext.SaveChangesAsync();
                    log.LogInformation($"Invoice {invoiceId} updated successfully.");
                    return new OkObjectResult($"Invoice {invoiceId} shipped date updated to {invoice.OrderShippedDate}");
                }
                catch (Exception ex)
                {
                    log.LogError($"Error updating invoice {invoiceId}: {ex.Message}");
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
            }
        }
    }
}
