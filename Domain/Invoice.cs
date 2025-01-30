using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        public int CustomerId { get; set; } // Foreign key to Customer
        public virtual Customer Customer { get; set; } // Correctly spelled navigation property

        public DateTime OrderDate { get; set; }
        public DateTime? OrderShippedDate { get; set; }

        public List<InvoiceProduct> InvoiceProducts { get; set; } = new List<InvoiceProduct>();
    }


}
