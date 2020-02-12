using PaymentGateway.Models;
using Microsoft.EntityFrameworkCore;

namespace PaymentGateway.Data
{
    public class PaymentGatewayContext : DbContext
    {
        public PaymentGatewayContext(DbContextOptions<PaymentGatewayContext> options) : base(options)
        {
        }

        public DbSet<Card> Card { get; set; }
        public DbSet<Payment> Payment { get; set; }
    }
}
