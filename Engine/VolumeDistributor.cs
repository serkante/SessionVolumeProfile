public interface IVolumeDistributor
{
    void Distribute(
        PriceBucketCollection buckets,
        double open,
        double high,
        double low,
        double close,
        double volume,
        double tickSize);
}
