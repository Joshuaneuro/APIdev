using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{  
    public class Invoice() 
    {
        public int InvoiceId { get; set; }
        public int CostumerID { get; set; }
        public virtual Costumer Costumer { get; set; }

        public List<Product> Invoices { get; set; } = new List<Product>();
    }
    
}
