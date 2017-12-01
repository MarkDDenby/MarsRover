using System;
using NUnit.Framework;

namespace Denby.Common.Tests.Unit
{
    [TestFixture]
    public class CoordinatesFixture
    {
        [Test]
        public void WhenCoordinateCreatedItDefaultsToALocationOfX1Y1()
        {
            // Arrange - Act
            var sut = new Coordinates();

            // Assert
            Assert.AreEqual(1, sut.X);
            Assert.AreEqual(0, sut.Y);
        }

        [Test]
        public void WhenCoordinateXIsChangedValueXPersists()
        {
            // Arrange 
            var sut = new Coordinates();

            // Act
            sut.X = 10;

            // Assert
            Assert.AreEqual(10, sut.X);
            Assert.AreEqual(0, sut.Y);
        }

        [Test]
        public void WhenCoordinateYIsChangedValueYPersists()
        {
            // Arrange 
            var sut = new Coordinates();

            // Act
            sut.Y = 10;

            // Assert
            Assert.AreEqual(1, sut.X);
            Assert.AreEqual(10, sut.Y);
        }
    }
}
