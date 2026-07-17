using SessionVolumeProfile;

namespace SessionVolumeProfile.Sessions
{
    public class SessionProfile
    {
        public SessionInfo Info { get; set; }

        public VolumeProfileEngine Engine { get; set; }

        public int FirstBarIndex { get; set; }

        public int LastBarIndex { get; set; }

        public bool Completed { get; set; }

        public double Poc =>
            Engine?.Poc?.Price ?? 0;

        public double ValueAreaHigh =>
            Engine?.ValueAreaHigh ?? 0;

        public double ValueAreaLow =>
            Engine?.ValueAreaLow ?? 0;
    }
}
