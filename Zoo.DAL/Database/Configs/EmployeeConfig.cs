using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zoo.DL.Entities.Humans;

namespace Zoo.DAL.Database.Configs
{
    public class EmployeeConfig:IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.EmployeeType).IsRequired();

            builder.HasOne(e => e.Address).WithMany(e => e.Employees).IsRequired();

            builder.HasOne(e => e.User).WithOne(u => u.Employee).HasForeignKey<User>(u => u.EmployeeId);

        }
    }
}
