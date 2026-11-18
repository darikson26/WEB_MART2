using System.ComponentModel.DataAnnotations;

namespace PC28168.U202422620.Openpay.Platform.Settlements.Interfaces.REST.Resources;

public sealed class CreateDisbursementOrderResource
{
    [Required]
    public string? OrderTrackingCode { get; init; }

    [Required]
    public string? AffiliateStoreId { get; init; }

    [Required]
    public string? ScheduledFrom { get; init; }

    [Required]
    public string? ScheduledTo { get; init; }

    [Required]
    public decimal? TransferCapAmount { get; init; }

    [Required]
    public string? TransferCapCurrencyCode { get; init; }

    public string? Status { get; init; }

    [Required]
    [StringLength(20)]
    public string? User { get; init; }

    [StringLength(85)]
    public string? Remarks { get; init; }
}
