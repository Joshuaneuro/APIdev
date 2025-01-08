using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
//using Microsoft.EntityFrameworkCore.Relational;

using Domain;

namespace DAL
{
    public class CostumerContext : DbContext
    {
        public CostumerContext(DbContextOptions<CostumerContext> options)
        : base(options)
        {
        }

        public DbSet<Costumer> Costumers { get; set; } = null!;
    }
}
