using System.Globalization;
using PC28168.U202422620.Openpay.Platform.Settlements.Application.Commands;
using PC28168.U202422620.Openpay.Platform.Settlements.Domain.Aggregates;
using PC28168.U202422620.Openpay.Platform.Settlements.Domain.Enums;
using PC28168.U202422620.Openpay.Platform.Settlements.Interfaces.REST.Resources;

namespace PC28168.U202422620.Openpay.Platform.Settlements.Interfaces.REST.Transform;

public sealed class DisbursementOrderResourceAssembler
{
    public CreateDisbursementOrderCommand ToCommand(CreateDisbursementOrderResource resource)
    {
        var orderTrackingCode = ParseGuid(resource.OrderTrackingCode, nameof(resource.OrderTrackingCode));
        var affiliateStoreId = ParseGuid(resource.AffiliateStoreId, nameof(resource.AffiliateStoreId));
        var scheduledFrom = ParseDate(resource.ScheduledFrom, nameof(resource.ScheduledFrom));
        var scheduledTo = ParseDate(resource.ScheduledTo, nameof(resource.ScheduledTo));

        if (resource.TransferCapAmount is null)
        {
            throw new ArgumentException("TransferCapAmount is required.", nameof(resource.TransferCapAmount));
        }

        return new CreateDisbursementOrderCommand(
            orderTrackingCode,
            affiliateStoreId,
            scheduledFrom,
            scheduledTo,
            resource.TransferCapAmount.Value,
            resource.TransferCapCurrencyCode ?? string.Empty,
            ParseStatus(resource.Status),
            resource.User ?? string.Empty,
            resource.Remarks);
    }

    public DisbursementOrderResource ToResource(DisbursementOrder order)
    {
        return new DisbursementOrderResource(
            order.Id,
            order.OrderTrackingCode.Value.ToString("D"),
            order.AffiliateStoreId.Value.ToString("D"),
            order.ExecutionSchedule.ScheduledFrom.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            order.ExecutionSchedule.ScheduledTo.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            order.TransferCap.Amount,
            order.TransferCap.CurrencyCode,
            order.Status.ToString(),
            order.User,
            order.Remarks ?? string.Empty,
            order.UpdatedAt);
    }

    private static Guid ParseGuid(string? value, string parameterName)
    {
        if (!Guid.TryParse(value, out var result))
        {
            throw new ArgumentException("The value must be a valid GUID.", parameterName);
        }

        return result;
    }

    private static DateOnly ParseDate(string? value, string parameterName)
    {
        if (!DateOnly.TryParseExact(
                value,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var result))
        {
            throw new ArgumentException("The value must use the yyyy-MM-dd date format.", parameterName);
        }

        return result;
    }

    private static DisbursementStatus? ParseStatus(string? value)
    {
        if (value is null)
        {
            return null;
        }

        if (!Enum.TryParse<DisbursementStatus>(value, true, out var status)
            || !Enum.IsDefined(typeof(DisbursementStatus), status))
        {
            throw new ArgumentException("Status must be Queued, Processed, Cancelled, or Failed.", nameof(value));
        }

        return status;
    }
}
