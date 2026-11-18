using PC28168.U202422620.Openpay.Platform.Shared.Domain.ValueObjects;
using PC28168.U202422620.Openpay.Platform.Settlements.Domain.Enums;
using PC28168.U202422620.Openpay.Platform.Settlements.Domain.ValueObjects;

namespace PC28168.U202422620.Openpay.Platform.Settlements.Domain.Aggregates;

public sealed class DisbursementOrder
{
    private DisbursementOrder()
    {
    }

    private DisbursementOrder(
        OpenpayReference orderTrackingCode,
        StoreIdentifier affiliateStoreId,
        DateInterval executionSchedule,
        FinancialAmount transferCap,
        DisbursementStatus status,
        string user,
        string? remarks,
        DateTime createdAt)
    {
        OrderTrackingCode = orderTrackingCode;
        AffiliateStoreId = affiliateStoreId;
        ExecutionSchedule = executionSchedule;
        TransferCap = transferCap;
        Status = status;
        User = user;
        Remarks = remarks;
        CreatedAt = createdAt;
        UpdatedAt = createdAt;
    }

    public int Id { get; private set; }

    public OpenpayReference OrderTrackingCode { get; private set; } = null!;

    public StoreIdentifier AffiliateStoreId { get; private set; } = null!;

    public DateInterval ExecutionSchedule { get; private set; } = null!;

    public FinancialAmount TransferCap { get; private set; } = null!;

    public DisbursementStatus Status { get; private set; }

    public string User { get; private set; } = string.Empty;

    public string? Remarks { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public static DisbursementOrder Create(
        OpenpayReference orderTrackingCode,
        StoreIdentifier affiliateStoreId,
        DateInterval executionSchedule,
        FinancialAmount transferCap,
        string? user,
        string? remarks,
        DisbursementStatus status = DisbursementStatus.Queued)
    {
        ArgumentNullException.ThrowIfNull(orderTrackingCode);
        ArgumentNullException.ThrowIfNull(affiliateStoreId);
        ArgumentNullException.ThrowIfNull(executionSchedule);
        ArgumentNullException.ThrowIfNull(transferCap);

        if (string.IsNullOrWhiteSpace(user))
        {
            throw new ArgumentException("User is required.", nameof(user));
        }

        if (user.Length > 20)
        {
            throw new ArgumentException("User cannot exceed 20 characters.", nameof(user));
        }

        if (remarks is { Length: > 85 })
        {
            throw new ArgumentException("Remarks cannot exceed 85 characters.", nameof(remarks));
        }

        if (!Enum.IsDefined(typeof(DisbursementStatus), status))
        {
            throw new ArgumentOutOfRangeException(nameof(status), "The disbursement status is not supported.");
        }

        return new DisbursementOrder(
            orderTrackingCode,
            affiliateStoreId,
            executionSchedule,
            transferCap,
            status,
            user,
            remarks,
            DateTime.UtcNow);
    }
}
