using System;
using NUnit.Framework;
using Denby.MarsRover.Core;

namespace Denby.Common.Tests.Unit
{
    [TestFixture]
    public class MarsFixture
    {
        [Test]
        public void WhenPlanetMarsHasValidCoordinatesDoesValidPlanetLocationReturnsTrue()
        {
            // Arrange 
            var sut = new Mars();
            var coordinates = new Coordinates() { X = 10, Y = 10};

            // Act
            bool result = sut.IsLocationWithinPerimeter(coordinates);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void WhenPlanetMarsHasInvalidXCoordinatesIsValidPlanetLocationReturnsFalse()
        {
            // Arrange 
            var sut = new Mars();
            var coordinates = new Coordinates() { X = 500, Y = 10 };

            // Act
            bool result = sut.IsLocationWithinPerimeter(coordinates);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void WhenPlanetMarsHasInvalidYCoordinatesIsValidPlanetLocationReturnsFalse()
        {
            // Arrange 
            var sut = new Mars();
            var coordinates = new Coordinates() { X = 10, Y = 500 };

            // Act
            bool result = sut.IsLocationWithinPerimeter(coordinates);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void WhenPlanetMarsHasValidCoordinatesThatMatchMaximumXAndYValuesIsValidPlanetLocationReturnsTrue()
        {
            // Arrange 
            var sut = new Mars();
            var coordinates = new Coordinates() { X = 100, Y = 100 };

            // Act
            bool result = sut.IsLocationWithinPerimeter(coordinates);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void WhenPlanetMarsHasValidCoordinatesThatMatchMiniumXAndYValuesIsValidPlanetLocationReturnsTrue()
        {
            // Arrange 
            var sut = new Mars();
            var coordinates = new Coordinates() { X = 1, Y = 1 };

            // Act
            bool result = sut.IsLocationWithinPerimeter(coordinates);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
