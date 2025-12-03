using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zoo.DL.Entities;

namespace Zoo.DAL.Database.Configs
{
    public class ToyConfig : IEntityTypeConfiguration<Toy>
    {
        public void Configure(EntityTypeBuilder<Toy> builder)
        {
            builder.ToTable("Toy");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.Name).IsRequired();

            builder.HasOne(a => a.Species).WithMany(s => s.Toys).IsRequired();

            builder.HasMany(a => a.Donations).WithOne(td => td.Toy);

            builder.Property(a => a.Description).IsRequired();

            builder.Property(a => a.ImagePath);

            builder.Property(a => a.MinimumAmountperDonation).IsRequired();

            builder.Property(a => a.WishedTotalAmount).IsRequired();

            builder.Property(a => a.StartDate).IsRequired();
            builder.Property(a => a.EndDate);

            builder.Property(a => a.Status).IsRequired();

        }
    }
}
