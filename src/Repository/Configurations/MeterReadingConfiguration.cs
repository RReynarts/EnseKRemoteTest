using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    internal class MeterReadingConfiguration : IEntityTypeConfiguration<MeterReading>
    {
        public void Configure(EntityTypeBuilder<MeterReading> builder)
        {

            builder.Property(p => p.AccountId).ValueGeneratedOnAdd();
            builder.HasKey(o => o.MeterReadingId);
            builder.HasOne<Account>().WithMany().HasForeignKey(o => o.AccountId).IsRequired();
        }
    }
}
