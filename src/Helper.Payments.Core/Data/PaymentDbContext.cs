using Helper.Payments.Core.Models.Invoices;
using Microsoft.EntityFrameworkCore;

namespace Helper.Payments.Core.Data
{
    internal class PaymentDbContext : DbContext
    {
        //public DbSet<User> Users { get; set; }
        //public DbSet<Offer> Offers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {
           
        }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        //    modelBuilder.Entity<User>()
        //    .ToTable("Users", schema: "api");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.Entity<Invoice>()
            .ToTable("Invoices", schema: "payment");
            //modelBuilder.Entity<Models.Offer>(ob =>
            //{
            //    ob.ToTable("Users", schema: "api");

            //});

        }
    }
}
