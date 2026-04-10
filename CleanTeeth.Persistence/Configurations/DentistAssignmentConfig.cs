using CleanTeeth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanTeeth.Persistence.Configurations;

public class DentistAssignmentConfig : IEntityTypeConfiguration<DentistAssignment>
{
    public void Configure(EntityTypeBuilder<DentistAssignment> builder)
    {
        builder.HasKey(da => da.Id);

        builder.Property(da => da.DentistId)
            .IsRequired();

        builder.Property(da => da.DentalOfficeId)
            .IsRequired();

        // Configure relationships
        builder.HasOne(da => da.Dentist)
            .WithMany(d => d.Assignments)
            .HasForeignKey(da => da.DentistId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(da => da.DentalOffice)
            .WithMany(o => o.Assignments)
            .HasForeignKey(da => da.DentalOfficeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Ensure unique assignment per dentist and dental office
        builder.HasIndex(da => new { da.DentistId, da.DentalOfficeId })
            .IsUnique();
    }
}