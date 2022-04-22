using EmployeeAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeAPI.Data.Mappings
{
    public class TaskEmployeeMapping : IEntityTypeConfiguration<TaskEmployee>
    {
        public void Configure(EntityTypeBuilder<TaskEmployee> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(t => t.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnName("Title")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnName("Description")
                .HasColumnType("TEXT")
                .HasMaxLength(2000);

            builder.Property(x => x.LastUpdateDate)
           .IsRequired()
           .HasColumnName("LastUpdateDate")
           .HasColumnType("SMALLDATETIME")
           .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.Done)
                .IsRequired()
                .HasColumnName("Done")
                .HasColumnType("BIT")
                .HasDefaultValue(false);

            builder.HasOne(X => X.Employee)
                .WithMany(x => x.Tasks)
                .HasConstraintName("Fk_Tasks_Employee");

        }
    }
}
