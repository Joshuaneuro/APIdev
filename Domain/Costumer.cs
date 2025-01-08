namespace Domain;

public class Costumer
{
    public int CostumerID { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public List<Invoice> Invoices { get; set; } = new List<Invoice>();
}

