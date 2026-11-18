using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using MySql.EntityFrameworkCore.Metadata;
using PC28168.U202422620.Openpay.Platform.Infrastructure.Persistence.Configurations;
using PC28168.U202422620.Openpay.Platform.Settlements.Domain.Aggregates;

namespace PC28168.U202422620.Openpay.Platform.Infrastructure.Persistence.Migrations;

[DbContext(typeof(SettlementsDbContext))]
public partial class SettlementsDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("ProductVersion", "10.0.12");
        modelBuilder.HasAnnotation("Relational:MaxIdentifierLength", 64);
        modelBuilder.HasAnnotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);
        modelBuilder.ApplyConfiguration(new DisbursementOrderConfiguration());
        modelBuilder.Entity<DisbursementOrder>()
            .Property(order => order.Id)
            .HasAnnotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);
    }
}
