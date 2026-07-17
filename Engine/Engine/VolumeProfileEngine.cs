public sealed class VolumeProfileEngine
{
    private readonly PriceBucketCollection _buckets;
    private readonly IVolumeDistributor _distributor;

    public PriceBucketCollection Buckets => _buckets;

    public VolumeProfileEngine(
        IVolumeDistributor distributor)
    {
        _distributor = distributor;
        _buckets = new PriceBucketCollection();
    }

    public void Reset()
    {
        _buckets.Clear();
    }

    public void AddBar(
        double open,
        double high,
        double low,
        double close,
        double volume,
        double tickSize)
    {
        _distributor.Distribute(
            _buckets,
            open,
            high,
            low,
            close,
            volume,
            tickSize);
    }

    public PriceBucket GetPointOfControl()
    {
        return _buckets.PointOfControl;
    }
}
