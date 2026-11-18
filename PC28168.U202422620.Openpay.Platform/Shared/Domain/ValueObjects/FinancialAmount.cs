namespace PC28168.U202422620.Openpay.Platform.Shared.Domain.ValueObjects;

public sealed record FinancialAmount
{
    private FinancialAmount()
    {
    }

    public FinancialAmount(decimal amount, string? currencyCode)
    {
        if (amount < decimal.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than or equal to zero.");
        }

        ArgumentNullException.ThrowIfNull(currencyCode);

        if (string.IsNullOrWhiteSpace(currencyCode))
        {
            throw new ArgumentException("Currency code cannot be empty or white space.", nameof(currencyCode));
        }

        Amount = amount;
        CurrencyCode = currencyCode;
    }

    public decimal Amount { get; private set; }

    public string CurrencyCode { get; private set; } = string.Empty;

    public FinancialAmount MultiplyBy(decimal factor)
    {
        return new FinancialAmount(Amount * factor, CurrencyCode);
    }
}
