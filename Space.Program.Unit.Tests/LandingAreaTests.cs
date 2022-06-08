using System;
using NUnit.Framework;

namespace Space.Program.Unit.Tests
{
    internal class LandingAreaTests
    {
        [TestCase(96)]
        [TestCase(123)]
        public void ThrowsArgumentOutOfRangeExceptionForTooBigPlatformSize(byte platformSize)
        {
            #region Arrange data

            var expectedMessage =
                "Specified argument was out of the range of valid values. (Parameter 'landingPlatformSize')";

            #endregion

            Assert.That(() => new LandingArea(platformSize),
                Throws.InstanceOf<ArgumentOutOfRangeException>().With.Message.EqualTo(expectedMessage));
        }

        [Test]
        public void ThrowsThrowsArgumentOutOfRangeExceptionForTooSmallPlatformSize()
        {
            #region Arrange data

            var expectedMessage =
                "Specified argument was out of the range of valid values. (Parameter 'landingPlatformSize')";

            #endregion

            Assert.That(() => new LandingArea(0),
                Throws.InstanceOf<ArgumentOutOfRangeException>().With.Message.EqualTo(expectedMessage));
        }

        [TestCase(10, 4, 4)]
        [TestCase(15, 4, 5)]
        [TestCase(25, 5, 4)]
        [TestCase(20, 30, 30)]
        public void ReturnsOutOfPlatformIfLandingCoordinatesDoesNotMatchPlatform(byte platformSize, byte rocketX, byte rocketY)
        {
            var landingArea = new LandingArea(platformSize);

            var result = landingArea.IsLandingPossible(rocketX, rocketY);

            Assert.That(result, Is.EqualTo(LandingResult.OutOfPlatform));
        }

        [TestCase(10, 10, 10)]
        [TestCase(15, 19, 19)]
        [TestCase(20, 5, 24)]
        [TestCase(25, 29, 5)]
        public void ReturnsOkForLandingIfLandingCoordinatesMatchPlatform(byte platformSize, byte rocketX, byte rocketY)
        {
            var landingArea = new LandingArea(platformSize);

            var result = landingArea.IsLandingPossible(rocketX, rocketY);

            Assert.That(result, Is.EqualTo(LandingResult.OkForLanding));
        }

        [TestCase(10, 10, 12, 12)]
        [TestCase(15, 15, 13, 13)]
        [TestCase(20, 20, 18, 21)]
        [TestCase(25, 25, 26, 23)]
        public void ReturnsOkForLandingIfPreviousRocketLandedTwoSquaresAway(byte firstRocketX, byte firstRocketY,
            byte secondRocketX, byte secondRocketY)
        {
            var landingArea = new LandingArea(50);

            landingArea.IsLandingPossible(firstRocketX, firstRocketY);
            var result = landingArea.IsLandingPossible(secondRocketX, secondRocketY);

            Assert.That(result, Is.EqualTo(LandingResult.OkForLanding));
        }

        [TestCase(10, 10)]
        [TestCase(15, 15)]
        public void ReturnsClashIfPreviousRocketLandedInTheSamePlace(byte rocketX, byte rocketY)
        {

            var landingArea = new LandingArea(50);

            landingArea.IsLandingPossible(rocketX, rocketY);
            var result = landingArea.IsLandingPossible(rocketX, rocketY);

            Assert.That(result, Is.EqualTo(LandingResult.Clash));
        }

        [TestCase(10, 10, 11, 11)]
        [TestCase(15, 15, 14,14)]
        [TestCase(20, 20,19,21)]
        [TestCase(25, 25, 26, 24)]
        public void ReturnsClashIfPreviousRocketLandedOneSquareAway(byte firstRocketX, byte firstRocketY,
            byte secondRocketX, byte secondRocketY)
        {
            var landingArea = new LandingArea(50);

            landingArea.IsLandingPossible(firstRocketX, firstRocketY);
            var result = landingArea.IsLandingPossible(secondRocketX, secondRocketY);

            Assert.That(result, Is.EqualTo(LandingResult.Clash));
        }

        [TestCase(10, 10, 15, 15)]
        [TestCase(6, 6, 6, 8)]
        [TestCase(15, 15, 15, 18)]
        public void WhenCalculatingResultTakesOnlyPreviousRocketPositionIntoAccount(byte firstRocketX, byte firstRocketY,
            byte secondRocketX, byte secondRocketY)
        {
            var landingArea = new LandingArea(50);

            landingArea.IsLandingPossible(10, 10);
            landingArea.IsLandingPossible(15, 15);
            var result = landingArea.IsLandingPossible(10, 10);

            Assert.That(result, Is.EqualTo(LandingResult.OkForLanding));
        }
    }
}
