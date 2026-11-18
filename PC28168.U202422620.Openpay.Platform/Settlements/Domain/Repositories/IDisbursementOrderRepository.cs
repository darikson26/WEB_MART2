using PC28168.U202422620.Openpay.Platform.Settlements.Domain.Aggregates;

namespace PC28168.U202422620.Openpay.Platform.Settlements.Domain.Repositories;

public interface IDisbursementOrderRepository
{
    Task<bool> ExistsByOrderTrackingCodeAsync(Guid orderTrackingCode, CancellationToken cancellationToken);

    Task AddAsync(DisbursementOrder disbursementOrder, CancellationToken cancellationToken);
}
