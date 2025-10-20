using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zoo.DL.Entities.Humans;

namespace Zoo.DAL.Database.Configs
{
    /*/      I don't know if we should use this table, we'll discuss how to handle former employees and their salaries/functions
     *       For instance, do we keep record of the old employees work, how to store data if they became users.
     *       What aout a user deleting their account, ...
//   */
    public class EmployeeConfig:IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            //builder.ToTable("Employee");

            //builder.HasKey(e => e.Id);
            //builder.Property(e => e.Id).ValueGeneratedOnAdd();

            //builder.Property(e => e.EmployeeType).IsRequired();
            //builder.Property(e => e.Address).IsRequired();
            //builder.Property(e => e.Email).IsRequired();

            //builder.HasOne(e => e.User).WithOne(u => u.Employee).HasForeignKey<Employee>(e => e.Email);

        }
    }
}
