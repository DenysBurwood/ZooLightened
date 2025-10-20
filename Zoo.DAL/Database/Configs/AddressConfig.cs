using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zoo.DL.Entities;

namespace Zoo.DAL.Database.Configs
{
    public class AddressConfig:IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address", a => a.HasCheckConstraint ("CK_Address__Number", "Number > 0"));
            builder.ToTable(a => a.HasCheckConstraint("CK_Address__PostalCode", "PostalCode > 0"));

            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.Street).HasMaxLength(130);
            builder.Property(a => a.Street).IsRequired();

            builder.Property(a => a.City).HasMaxLength(45);
            builder.Property(a => a.City).IsRequired();


            builder.Property(a => a.Country).HasMaxLength(40);
            builder.Property(a => a.Country).IsRequired();

            builder.HasMany(a => a.Employees).WithOne(e => e.Address);

        }
    }
}
