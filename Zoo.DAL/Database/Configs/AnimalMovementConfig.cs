using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zoo.DL.Entities;

namespace Zoo.DAL.Database.Configs
{
    public class AnimalMovementConfig : IEntityTypeConfiguration<AnimalMovement>
    {
        public void Configure(EntityTypeBuilder<AnimalMovement> builder)
        {
            builder.ToTable("AnimalMovement");

            builder.HasKey(am => am.Id);
            builder.Property(am => am.Id).ValueGeneratedOnAdd();

            // --- Relations ---
            builder.HasOne(am => am.Animal)
                .WithMany(a => a.AnimalMovements)
                .HasForeignKey(am => am.AnimalId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(am => am.CounterPart)
                .WithMany(o => o.AnimalMovements)
                .HasForeignKey(am => am.CounterPartId)
                .OnDelete(DeleteBehavior.NoAction); 

            builder.Property(am => am.Direction).IsRequired();
            builder.Property(am => am.StartDate).IsRequired();
            builder.Property(am => am.EndDate);

            builder.Property(am => am.Type).IsRequired();
        }
    }

}
