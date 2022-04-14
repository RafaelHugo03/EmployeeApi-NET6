using EmployeeAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeAPI.Data.Mappings
{
    public class EmployeeMapping : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employess");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Office)
                .IsRequired(false)
                .HasColumnName("Office")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(30);

            builder.HasOne(x => x.Department)
                .WithMany(x => x.Employees)
                .HasConstraintName("FK_Employee_Department");
        }
    }
}
