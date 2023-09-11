using Helper.Payments.Core.Models.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Helper.Payments.Core.Data.Configs
{
    internal class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(x => x.InvoiceId);
            builder.Property(x => x.InvoiceId);
            builder.Property(x => x.Email);

            builder.Property(x => x.Price);

            builder.Property(x => x.Fullname);

            builder.Property(x => x.Street);

            builder.Property(x => x.BankAccountNumber);

        }
    }
}
