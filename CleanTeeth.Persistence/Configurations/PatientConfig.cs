using CleanTeeth.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanTeeth.Persistence.Configurations;

public class PatientConfig : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(250)
            .IsRequired();

        builder.ComplexProperty(prop => prop.Email, action =>
        {
            action.Property(e => e.Value)
                .HasColumnName("Email")
                .HasMaxLength(254);
        });
    }
}