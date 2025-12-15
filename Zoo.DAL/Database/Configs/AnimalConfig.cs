using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zoo.DL.Entities;

namespace Zoo.DAL.Database.Configs
{
    public class AnimalConfig:IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> builder)
        {
            builder.ToTable("Animal");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.Name).IsRequired();
            builder.Property(a => a.Sex).IsRequired();

            builder.HasOne(a => a.Species)
               .WithMany(asp => asp.Animals)
               .HasForeignKey(a => a.SpeciesId)
               .IsRequired();

            builder.HasOne(a => a.Owner)
               .WithMany(o => o.Animals)
               .HasForeignKey(a => a.OwnerId)
               .IsRequired();

            builder.HasMany(a => a.AnimalMovements).WithOne(am => am.Animal);

            builder.Property(a => a.IsAvailable).IsRequired();

            builder.Property(a => a.BirthDate).IsRequired();

            builder.Property(a => a.RIPDate);
        }
    }
}
