using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Customer
{
    public int CustomerId { get; set; }

    [MaxLength(60)]
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public List<Invoice> Invoices { get; set; } = new List<Invoice>();
}


