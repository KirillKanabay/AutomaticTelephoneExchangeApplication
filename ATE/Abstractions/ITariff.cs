namespace ATE.Core.Entities
{
    public interface ITariff
    {
        string Name { get; }
        decimal PricePerMinuteCall { get; }
    }
}