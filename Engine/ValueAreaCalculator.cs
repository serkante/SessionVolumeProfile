public sealed class ValueAreaResult
{
    public double PocPrice { get; init; }
    public double ValueAreaHigh { get; init; }
    public double ValueAreaLow { get; init; }

    public double TotalVolume { get; init; }
    public double ValueAreaVolume { get; init; }
}
public interface IValueAreaCalculator
{
    ValueAreaResult Calculate(
        IReadOnlyList<PriceBucket> buckets,
        double valueAreaPercent = 0.70);
}
