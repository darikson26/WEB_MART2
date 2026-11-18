using Microsoft.EntityFrameworkCore;
using PC28168.U202422620.Openpay.Platform.Infrastructure.Persistence.Configurations;
using PC28168.U202422620.Openpay.Platform.Settlements.Domain.Aggregates;

namespace PC28168.U202422620.Openpay.Platform.Infrastructure.Persistence;

public sealed class SettlementsDbContext(DbContextOptions<SettlementsDbContext> options) : DbContext(options)
{
    public DbSet<DisbursementOrder> DisbursementOrders => Set<DisbursementOrder>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DisbursementOrderConfiguration());
    }
}
