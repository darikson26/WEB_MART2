using PC28168.U202422620.Openpay.Platform.Shared.Domain.ValueObjects;
using PC28168.U202422620.Openpay.Platform.Settlements.Application.Commands;
using PC28168.U202422620.Openpay.Platform.Settlements.Domain.Aggregates;
using PC28168.U202422620.Openpay.Platform.Settlements.Domain.Enums;
using PC28168.U202422620.Openpay.Platform.Settlements.Domain.Exceptions;
using PC28168.U202422620.Openpay.Platform.Settlements.Domain.Repositories;
using PC28168.U202422620.Openpay.Platform.Settlements.Domain.ValueObjects;

namespace PC28168.U202422620.Openpay.Platform.Settlements.Application.Handlers;

public sealed class CreateDisbursementOrderCommandHandler(IDisbursementOrderRepository repository)
{
    public async Task<DisbursementOrder> HandleAsync(
        CreateDisbursementOrderCommand command,
        CancellationToken cancellationToken)
    {
        var order = DisbursementOrder.Create(
            new OpenpayReference(command.OrderTrackingCode),
            new StoreIdentifier(command.AffiliateStoreId),
            new DateInterval(command.ScheduledFrom, command.ScheduledTo),
            new FinancialAmount(command.TransferCapAmount, command.TransferCapCurrencyCode),
            command.User,
            command.Remarks,
            command.Status ?? DisbursementStatus.Queued);

        if (await repository.ExistsByOrderTrackingCodeAsync(command.OrderTrackingCode, cancellationToken))
        {
            throw new DuplicateOrderTrackingCodeException();
        }

        await repository.AddAsync(order, cancellationToken);
        return order;
    }
}
