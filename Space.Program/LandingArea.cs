namespace Space.Program
{
    public class LandingArea
    {
        private const byte MaxLandingPlatformSize = 95;
        private const byte MinLandingPlatformSize = 1;
        private const byte LandingPlatformStartingPoint = 5;

        private readonly byte _landingPlatformSize;

        private byte _lastRocketX;
        private byte _lastRocketY;

        public LandingArea(byte landingPlatformSize)
        {
            if (landingPlatformSize is < MinLandingPlatformSize or > MaxLandingPlatformSize)
            {
                throw new ArgumentOutOfRangeException(nameof(landingPlatformSize));
            }

            _landingPlatformSize = landingPlatformSize;
        }

        public string IsLandingPossible(byte rocketX, byte rocketY)
        {
            if (rocketX >= LandingPlatformStartingPoint + _landingPlatformSize || rocketX < LandingPlatformStartingPoint
                || rocketY >= LandingPlatformStartingPoint + _landingPlatformSize || rocketY < LandingPlatformStartingPoint)
            {
                return LandingResult.OutOfPlatform;
            }

            if (CheckForClash(rocketX, rocketY))
            {
                return LandingResult.Clash;
            }

            _lastRocketX = rocketX;
            _lastRocketY = rocketY;
            return LandingResult.OkForLanding;
        }

        private bool CheckForClash(byte rocketX, byte rocketY) => Math.Abs(rocketX - _lastRocketX) < 2 && Math.Abs(rocketY - _lastRocketY) < 2;
    }
}
