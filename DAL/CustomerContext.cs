using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;


using Domain;

namespace DAL
{
    public class CustomerContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InvoiceProduct> InvoiceProducts { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public CustomerContext(DbContextOptions<CustomerContext> options)
        : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceProduct>()
                .HasKey(ip => new { ip.InvoiceId, ip.ProductId }); // Composite primary key

            modelBuilder.Entity<InvoiceProduct>()
                .HasOne(ip => ip.Invoice)
                .WithMany(i => i.InvoiceProducts)
                .HasForeignKey(ip => ip.InvoiceId);

            modelBuilder.Entity<InvoiceProduct>()
                .HasOne(ip => ip.Product)
                .WithMany(p => p.InvoiceProducts)
                .HasForeignKey(ip => ip.ProductId);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Customer) // Correct navigation property
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Customer> Costumers { get; set; } = null!;
    }

}
