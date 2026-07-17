using System;
using SessionVolumeProfile.Models;

namespace SessionVolumeProfile.Engine
{
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

    public sealed class DefaultVolumeDistributor : IVolumeDistributor
    {
        public void Distribute(
            PriceBucketCollection buckets,
            double open,
            double high,
            double low,
            double close,
            double volume,
            double tickSize)
        {
            if (volume <= 0)
                return;

            if (tickSize <= 0)
                throw new ArgumentException(nameof(tickSize));

            high = Normalize(high, tickSize);
            low = Normalize(low, tickSize);

            if (high < low)
            {
                var temp = high;
                high = low;
                low = temp;
            }

            int levels = (int)Math.Round((high - low) / tickSize) + 1;

            if (levels <= 0)
                levels = 1;

            double volumePerLevel = volume / levels;

            bool bullish = close >= open;

            for (int i = 0; i < levels; i++)
            {
                double price = Normalize(low + i * tickSize, tickSize);

                var bucket = buckets.GetOrCreate(price);

                if (bullish)
                    bucket.AddBuy(volumePerLevel);
                else
                    bucket.AddSell(volumePerLevel);
            }
        }

        private static double Normalize(double price, double tickSize)
        {
            return Math.Round(price / tickSize) * tickSize;
        }
    }
}
