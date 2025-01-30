using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Product
{
    public int ProductID { get; set; }
    [Required]
    [MaxLength(60)]
    public string? ProductName { get; set; }
    public int ProductPrice { get; set; }

    public string? ImageURL { get; set; }

    public List<InvoiceProduct> InvoiceProducts { get; set; } = new List<InvoiceProduct>();
    public List<Review> Reviews { get; set; } = new List<Review>();

}

