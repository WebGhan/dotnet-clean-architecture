using CleanTeeth.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanTeeth.Persistence.Configurations;

public class AppointmentConfig: IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ComplexProperty(prop => prop.TimeInterval, action =>
        {
            action.Property(e => e.Start).HasColumnName("StartDate");
            action.Property(e => e.End).HasColumnName("EndDate");
        });
    }
}