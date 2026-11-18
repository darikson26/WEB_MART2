using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PC28168.U202422620.Openpay.Platform.Shared.Domain.ValueObjects;
using PC28168.U202422620.Openpay.Platform.Settlements.Domain.Aggregates;
using PC28168.U202422620.Openpay.Platform.Settlements.Domain.ValueObjects;

namespace PC28168.U202422620.Openpay.Platform.Infrastructure.Persistence.Configurations;

public sealed class DisbursementOrderConfiguration : IEntityTypeConfiguration<DisbursementOrder>
{
    public void Configure(EntityTypeBuilder<DisbursementOrder> builder)
    {
        builder.ToTable("disbursement_orders");
        builder.HasKey(order => order.Id).HasName("pk_disbursement_orders");
        builder.Property(order => order.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder.OwnsOne(order => order.OrderTrackingCode, reference =>
        {
            reference.Property(value => value.Value)
                .HasColumnName("order_tracking_code")
                .HasColumnType("varchar(36)")
                .HasConversion(value => value.ToString("D"), value => Guid.Parse(value))
                .IsRequired();
            reference.HasIndex(value => value.Value)
                .IsUnique()
                .HasDatabaseName("ux_disbursement_orders_order_tracking_code");
        });
        builder.Navigation(order => order.OrderTrackingCode).IsRequired();

        builder.OwnsOne(order => order.AffiliateStoreId, identifier =>
        {
            identifier.Property(value => value.Value)
                .HasColumnName("affiliate_store_id")
                .HasColumnType("varchar(36)")
                .HasConversion(value => value.ToString("D"), value => Guid.Parse(value))
                .IsRequired();
        });
        builder.Navigation(order => order.AffiliateStoreId).IsRequired();

        builder.OwnsOne(order => order.ExecutionSchedule, interval =>
        {
            interval.Property(value => value.ScheduledFrom)
                .HasColumnName("scheduled_from")
                .HasColumnType("date")
                .HasConversion(value => value.ToDateTime(TimeOnly.MinValue), value => DateOnly.FromDateTime(value))
                .IsRequired();
            interval.Property(value => value.ScheduledTo)
                .HasColumnName("scheduled_to")
                .HasColumnType("date")
                .HasConversion(value => value.ToDateTime(TimeOnly.MinValue), value => DateOnly.FromDateTime(value))
                .IsRequired();
        });
        builder.Navigation(order => order.ExecutionSchedule).IsRequired();

        builder.OwnsOne(order => order.TransferCap, amount =>
        {
            amount.Property(value => value.Amount)
                .HasColumnName("transfer_cap_amount")
                .HasColumnType("decimal(65,30)")
                .IsRequired();
            amount.Property(value => value.CurrencyCode)
                .HasColumnName("transfer_cap_currency_code")
                .HasColumnType("longtext")
                .IsRequired();
        });
        builder.Navigation(order => order.TransferCap).IsRequired();

        builder.Property(order => order.Status)
            .HasColumnName("status")
            .HasColumnType("int")
            .IsRequired();
        builder.Property(order => order.User)
            .HasColumnName("user")
            .HasColumnType("varchar(20)")
            .HasMaxLength(20)
            .IsRequired();
        builder.Property(order => order.Remarks)
            .HasColumnName("remarks")
            .HasColumnType("varchar(85)")
            .HasMaxLength(85);
        builder.Property(order => order.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("datetime(6)")
            .IsRequired();
        builder.Property(order => order.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("datetime(6)")
            .IsRequired();
    }
}
