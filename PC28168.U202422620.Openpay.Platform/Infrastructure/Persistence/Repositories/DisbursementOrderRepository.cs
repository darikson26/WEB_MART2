using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using PC28168.U202422620.Openpay.Platform.Settlements.Domain.Aggregates;
using PC28168.U202422620.Openpay.Platform.Settlements.Domain.Exceptions;
using PC28168.U202422620.Openpay.Platform.Settlements.Domain.Repositories;

namespace PC28168.U202422620.Openpay.Platform.Infrastructure.Persistence.Repositories;

public sealed class DisbursementOrderRepository(SettlementsDbContext context) : IDisbursementOrderRepository
{
    public Task<bool> ExistsByOrderTrackingCodeAsync(Guid orderTrackingCode, CancellationToken cancellationToken)
    {
        return context.DisbursementOrders
            .AnyAsync(order => order.OrderTrackingCode.Value == orderTrackingCode, cancellationToken);
    }

    public async Task AddAsync(DisbursementOrder disbursementOrder, CancellationToken cancellationToken)
    {
        context.DisbursementOrders.Add(disbursementOrder);

        try
        {
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException exception) when (HasDuplicateKey(exception))
        {
            throw new DuplicateOrderTrackingCodeException();
        }
    }

    private static bool HasDuplicateKey(Exception exception)
    {
        for (Exception? current = exception; current is not null; current = current.InnerException)
        {
            if (current is MySqlException { Number: 1062 })
            {
                return true;
            }
        }

        return false;
    }
}
