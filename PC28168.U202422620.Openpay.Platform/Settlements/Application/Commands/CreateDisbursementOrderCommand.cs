using PC28168.U202422620.Openpay.Platform.Settlements.Domain.Enums;

namespace PC28168.U202422620.Openpay.Platform.Settlements.Application.Commands;

public sealed record CreateDisbursementOrderCommand(
    Guid OrderTrackingCode,
    Guid AffiliateStoreId,
    DateOnly ScheduledFrom,
    DateOnly ScheduledTo,
    decimal TransferCapAmount,
    string TransferCapCurrencyCode,
    DisbursementStatus? Status,
    string User,
    string? Remarks);
