namespace PC28168.U202422620.Openpay.Platform.Settlements.Interfaces.REST.Resources;

public sealed record DisbursementOrderResource(
    int Id,
    string OrderTrackingCode,
    string AffiliateStoreId,
    string ScheduledFrom,
    string ScheduledTo,
    decimal TransferCapAmount,
    string TransferCapCurrencyCode,
    string Status,
    string User,
    string Remarks,
    DateTime UpdatedAt);
