using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.DL.Entities;

namespace Zoo.DAL.Database.Configs
{
    internal class OwnerConfig : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.ToTable("Owner");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedOnAdd();

            builder.Property(o => o.Name).IsRequired();
            builder.HasOne(o => o.Address).WithMany(a => a.Owners).IsRequired();

            builder.Property(o => o.ContactName).IsRequired();
            builder.Property(o => o.PhoneNumber).IsRequired();
            builder.Property(o => o.Email).IsRequired();

            builder.HasMany(o => o.Animals).WithOne(a => a.Owner);
        }
    }
}
