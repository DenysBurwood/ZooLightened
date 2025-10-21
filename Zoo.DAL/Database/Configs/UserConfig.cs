using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zoo.DL.Entities.Humans;

namespace Zoo.DAL.Database.Configs
{
    internal class UserConfig:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            
            builder.HasKey(u => u.Id).HasName("PK_User");
            builder.Property(u => u.Id).ValueGeneratedOnAdd();

            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.Email).IsRequired().HasMaxLength(180);

            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(65);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(120);

            builder.Property(u => u.Password).IsRequired();

            
            builder.HasOne(u => u.Employee).WithOne(e => e.User).HasForeignKey<Employee>(e => e.Id);


        }
    }
}
