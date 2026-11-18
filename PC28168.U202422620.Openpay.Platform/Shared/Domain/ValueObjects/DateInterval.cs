namespace PC28168.U202422620.Openpay.Platform.Shared.Domain.ValueObjects;

public sealed record DateInterval
{
    private DateInterval()
    {
    }

    public DateInterval(DateOnly scheduledFrom, DateOnly scheduledTo)
    {
        if (scheduledTo <= scheduledFrom)
        {
            throw new ArgumentException("ScheduledTo must be later than ScheduledFrom.", nameof(scheduledTo));
        }

        ScheduledFrom = scheduledFrom;
        ScheduledTo = scheduledTo;
    }

    public DateOnly ScheduledFrom { get; private set; }

    public DateOnly ScheduledTo { get; private set; }

    public bool HasElapsed(DateOnly referenceDate)
    {
        return referenceDate > ScheduledTo;
    }
}
