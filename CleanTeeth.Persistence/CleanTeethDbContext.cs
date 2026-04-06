using CleanTeeth.Application.Contracts.Security;
using CleanTeeth.Domain.Common;
using CleanTeeth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanTeeth.Persistence;

public class CleanTeethDbContext : DbContext
{
    private readonly IUserService? _userService;

    public CleanTeethDbContext(DbContextOptions<CleanTeethDbContext> options, IUserService userService) : base(options)
    {
        _userService = userService;
    }

    protected CleanTeethDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CleanTeethDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (_userService is not null)
        {
            foreach (var entry in ChangeTracker.Entries<Auditable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        entry.Entity.CreatedBy = _userService.GetUserId();
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = _userService.GetUserId();
                        break;
                }
            }
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<DentalOffice> DentalOffices { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Dentist> Dentists { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
}