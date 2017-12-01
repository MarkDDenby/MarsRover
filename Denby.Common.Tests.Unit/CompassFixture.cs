using NUnit.Framework;
using Denby.Common;
using Denby.Contracts;


namespace Denby.Common.Tests.Unit
{
    [TestFixture]
    public class CompassFixture
    {
        [Test]
        public void WhenCompassCreatedItDefaultsToABearingOfNorth()
        {
            // Arrange - Act
            var sut = new Compass();

            // Assert
            Assert.AreEqual(CardinalHeading.North, sut.Bearing);
        }

        [Test]
        public void WhenCompassIsBearingNorthAndIsRotatedRightItBearsEast()
        {
            // Arrange 
            var sut = new Compass();
            sut.Bearing = CardinalHeading.North;

            // Act
            sut.Rotate(Rotate.Right);

            // Assert
            Assert.AreEqual(CardinalHeading.East, sut.Bearing);
        }

        [Test]
        public void WhenCompassIsBearingNorthAndIsRotatedLeftItBearsWest()
        {
            // Arrange 
            var sut = new Compass();
            sut.Bearing = CardinalHeading.North;

            // Act
            sut.Rotate(Rotate.Left);

            // Assert
            Assert.AreEqual(CardinalHeading.West, sut.Bearing);
        }

        [Test]
        public void WhenCompassIsBearingEastAndIsRotatedRightItBearsSouth()
        {
            // Arrange 
            var sut = new Compass();
            sut.Bearing = CardinalHeading.East;

            // Act
            sut.Rotate(Rotate.Right);

            // Assert
            Assert.AreEqual(CardinalHeading.South, sut.Bearing);
        }

        [Test]
        public void WhenCompassIsBearingEastAndIsRotatedLeftItBearsNorth()
        {
            // Arrange 
            var sut = new Compass();
            sut.Bearing = CardinalHeading.East;

            // Act
            sut.Rotate(Rotate.Left);

            // Assert
            Assert.AreEqual(CardinalHeading.North,sut.Bearing);
        }

        [Test]
        public void WhenCompassIsBearingSouthAndIsRotatedRightItBearsWest()
        {
            // Arrange 
            var sut = new Compass();
            sut.Bearing = CardinalHeading.South;

            // Act
            sut.Rotate(Rotate.Right);

            // Assert
            Assert.AreEqual(CardinalHeading.West, sut.Bearing);
        }

        [Test]
        public void WhenCompassIsBearingSouthAndIsRotatedLeftItBearsEast()
        {
            // Arrange 
            var sut = new Compass();
            sut.Bearing = CardinalHeading.South;

            // Act
            sut.Rotate(Rotate.Left);

            // Assert
            Assert.AreEqual(CardinalHeading.East, sut.Bearing);
        }

        [Test]
        public void WhenCompassIsBearingWestAndIsRotatedRightItBearsNorth()
        {
            // Arrange 
            var sut = new Compass();
            sut.Bearing = CardinalHeading.West;

            // Act
            sut.Rotate(Rotate.Right);

            // Assert
            Assert.AreEqual(CardinalHeading.North, sut.Bearing);
        }

        [Test]
        public void WhenCompassIsBearingWestAndIsRotatedLeftItBearsSouth()
        {
            // Arrange 
            var sut = new Compass();
            sut.Bearing = CardinalHeading.West;

            // Act
            sut.Rotate(Rotate.Left);

            // Assert
            Assert.AreEqual(CardinalHeading.South, sut.Bearing);
        }
    }
}
