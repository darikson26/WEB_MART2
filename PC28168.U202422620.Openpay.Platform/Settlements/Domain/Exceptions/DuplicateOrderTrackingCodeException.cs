namespace PC28168.U202422620.Openpay.Platform.Settlements.Domain.Exceptions;

public sealed class DuplicateOrderTrackingCodeException : Exception
{
    public DuplicateOrderTrackingCodeException()
        : base("A disbursement order with this order tracking code already exists.")
    {
    }
}
