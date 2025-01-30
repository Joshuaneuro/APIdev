using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InvoiceProduct
    {
        public int InvoiceId { get; set; } // Foreign key to Invoice
        public virtual Invoice Invoice { get; set; } // Navigation property

        public int ProductId { get; set; } // Foreign key to Product
        public virtual Product Product { get; set; } // Navigation property

        public int Quantity { get; set; } // Quantity of this product in the invoice
    }

}
