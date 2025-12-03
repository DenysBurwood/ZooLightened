using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zoo.DL.Entities;

namespace Zoo.DAL.Database.Configs
{
    public class ToyDonationConfig : IEntityTypeConfiguration<ToyDonation>
    {
        public void Configure(EntityTypeBuilder<ToyDonation> builder)
        {
            builder.ToTable("ToyDonation");

            builder.HasKey(td => td.Id);
            builder.Property(td => td.Id).ValueGeneratedOnAdd();

            builder.HasOne(td => td.Toy).WithMany(t => t.Donations).IsRequired();

            builder.HasOne(td => td.User).WithMany(u => u.Donations).IsRequired();

            builder.Property(td => td.Amount).IsRequired();

            builder.Property(td => td.DonationDate).IsRequired();

        }
    }
}
