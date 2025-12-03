using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zoo.DL.Entities;

namespace Zoo.DAL.Database.Configs
{
    public class AnimalSpeciesConfig:IEntityTypeConfiguration<AnimalSpecies>
    {
        public void Configure(EntityTypeBuilder<AnimalSpecies> builder)
        {
            builder.ToTable("AnimalSpecies");

            builder.HasKey(asp => asp.Id);
            builder.Property(asp => asp.Id).ValueGeneratedOnAdd();
            builder.Property(asp => asp.Name).IsRequired();
            builder.Property(asp => asp.Description).IsRequired();

            builder.HasMany(asp => asp.Animals).WithOne(a => a.Species);
        }
    }
}
