namespace _Scripts
{
    public static class FeatureLocker
    {
        public static bool PlayerInputEnabled { get; private set; }
        public static bool ReplayingEnabled { get; private set; }

        public static void SetPlayerInputEnabled(bool enabled)
        {
            PlayerInputEnabled = enabled;
        }

        public static void SetReplayingEnabled(bool enabled)
        {
            ReplayingEnabled = enabled;
        }
    }
}