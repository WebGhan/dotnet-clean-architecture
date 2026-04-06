using CleanTeeth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanTeeth.Persistence.Configurations;

public class DentistConfig : IEntityTypeConfiguration<Dentist>
{
    public void Configure(EntityTypeBuilder<Dentist> builder)
    {
        builder.Property(p => p.Name)
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