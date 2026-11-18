namespace PC28168.U202422620.Openpay.Platform.Settlements.Domain.ValueObjects;

public sealed record OpenpayReference
{
    private OpenpayReference()
    {
    }

    public OpenpayReference(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; private set; }
}
