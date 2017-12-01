using Denby.Contracts;
using Denby.Contracts.Enums;
using NUnit.Framework;
using Rhino.Mocks;

namespace Denby.Common.Tests.Unit
{
    [TestFixture]
    public class PlanetNavigatorFixture
    {
        private IPlanet _planet;
        private ICompass _compass;

        [SetUp]
        public void Setup()
        {
           _planet = GeneratePlanetStubThatReturnsValidPlanetLocation();
           _compass = MockCompassWithHeadingNorth();
        }

        private ICompass MockCompassWithHeadingNorth()
        {
            var compass = MockRepository.GenerateMock<ICompass>();
            compass.Stub(o => o.Bearing).Return(CardinalHeading.North);
            return compass;
        }

        private IPlanet GeneratePlanetStubThatReturnsValidPlanetLocation()
        {
            GeneratePlanetStub();

            _planet.Stub(o => o.IsLocationWithinPerimeter(Arg<ICoordinates>.Is.Anything)).Return(true);
            return _planet;
        }

        private IPlanet GeneratePlanetStubThatReturnsInvalidPlanetLocation()
        {
            GeneratePlanetStub();

            _planet.Stub(o => o.IsLocationWithinPerimeter(Arg<ICoordinates>.Is.Anything)).Return(false);
            return _planet;
        }

        private void GeneratePlanetStub()
        {
            var _xaxis = MockRepository.GenerateMock<IAxis>();
            _xaxis.Stub(o => o.MinimumPoint).Return(1);
            _xaxis.Stub(o => o.MaximumPoint).Return(100);

            var _yaxis = MockRepository.GenerateMock<IAxis>();
            _yaxis.Stub(o => o.MinimumPoint).Return(1);
            _yaxis.Stub(o => o.MaximumPoint).Return(100);

            _planet = MockRepository.GenerateMock<IPlanet>();
            _planet.Stub(o => o.XAxis).Return(_xaxis);
            _planet.Stub(o => o.YAxis).Return(_yaxis);
        }

        [Test]
        public void WhenPlanetNavigatorPositionChangedPositionPersists()
        {
            // Arrange - Act
            var sut = new Navigation(_planet, _compass, new Coordinates() { X = 10, Y = 20 }, CardinalHeading.North );
            
            // Assert
            Assert.AreEqual(10, sut.Location.X);
            Assert.AreEqual(20, sut.Location.Y);
            Assert.AreEqual(NavigationState.Ready, sut.State);
        }

        [Test]
        public void WhenPlanetNavigatorIsMoved5PositionsSouthPositionIsCorrect()
        {
            // Arrange 
            var sut = new Navigation(_planet, _compass, new Coordinates() { X = 10, Y = 10 }, CardinalHeading.South);
            
            // Act
            sut.Move(5);

            // Assert
            Assert.AreEqual(10, sut.Location.X);
            Assert.AreEqual(15, sut.Location.Y);
            Assert.AreEqual(NavigationState.Ready, sut.State);
        }

        [Test]
        public void WhenPlanetNavigatorIsMoved5PositionsEastPositionIsCorrect()
        {
            // Arrange 
            var sut = new Navigation(_planet, _compass, new Coordinates() { X = 10, Y = 10 }, CardinalHeading.East);

            // Act
            sut.Move(5);

            // Assert
            Assert.AreEqual(15, sut.Location.X);
            Assert.AreEqual(10, sut.Location.Y); 
            Assert.AreEqual(NavigationState.Ready, sut.State);
        }

        [Test]
        public void WhenPlanetNavigatorIsMoved5PositionsWestPositionIsCorrect()
        {
            // Arrange 
            var sut = new Navigation(_planet, _compass, new Coordinates() { X = 10, Y = 10 }, CardinalHeading.West);

            // Act
            sut.Move(5);

            // Assert
            Assert.AreEqual(5, sut.Location.X);
            Assert.AreEqual(10, sut.Location.Y);
            Assert.AreEqual(NavigationState.Ready, sut.State);
        }

        [Test]
        public void WhenPlanetNavigatorIsMoved5PositionsNorthPositionIsCorrect()
        {
            // Arrange 
            var sut = new Navigation(_planet, _compass, new Coordinates() { X = 10, Y = 10 }, CardinalHeading.North);

            // Act
            sut.Move(5);

            // Assert
            Assert.AreEqual(10, sut.Location.X);
            Assert.AreEqual(5, sut.Location.Y);
            Assert.AreEqual(NavigationState.Ready, sut.State);
        }

        [Test]
        public void WhenPlanetNavigatorIsMoved500PositionsNorthPositionIsReturnedAsPlanetMinimum()
        {
            // Arrange 
            var sut = new Navigation(_planet, _compass, new Coordinates() { X = 1, Y = 1 }, CardinalHeading.North);

            // Act
            sut.Move(500);

            // Assert
            Assert.AreEqual(1, sut.Location.X);
            Assert.AreEqual(_planet.YAxis.MinimumPoint, sut.Location.Y);
        }

        [Test]
        public void WhenPlanetNavigatorIsMoved500PositionsNorthTheNavigatorReturnsCorrectStatusOfHitPerimeter()
        {
            _planet = GeneratePlanetStubThatReturnsInvalidPlanetLocation();

            // Arrange 
            var sut = new Navigation(_planet, _compass, new Coordinates() { X = 1, Y = 1 }, CardinalHeading.North);

            // Act
            sut.Move(500);

            //Assert
            Assert.AreEqual(NavigationState.HitPerimeter, sut.State);
        }

        [Test]
        public void WhenPlanetNavigatorIsMoved500PositionsEastPositionIsReturnedAsPlanetMaximum()
        {
            // Arrange 
            var sut = new Navigation(_planet, _compass, new Coordinates() { X = 1, Y = 1 }, CardinalHeading.East);

            // Act
            sut.Move(500);

            // Assert
            Assert.AreEqual(_planet.YAxis.MaximumPoint, sut.Location.X);
            Assert.AreEqual(1 ,sut.Location.Y);
        }

        [Test]
        public void WhenPlanetNavigatorIsMoved500PositionsEastTheNavigatorReturnsCorrectStatusOfHitPerimeter()
        {
            _planet = GeneratePlanetStubThatReturnsInvalidPlanetLocation();

            // Arrange 
            var sut = new Navigation(_planet, _compass, new Coordinates() { X = 1, Y = 1 }, CardinalHeading.East);

            // Act
            sut.Move(500);

            //Assert
            Assert.AreEqual(NavigationState.HitPerimeter, sut.State);
        }

        [Test]
        public void WhenPlanetNavigatorIsMoved500PositionsSouthPositionIsReturnedAsPlanetMaximum()
        {
            // Arrange 
            var sut = new Navigation(_planet, _compass, new Coordinates() { X = 1, Y = 1 },CardinalHeading.South);

            // Act
            sut.Move(500);

            // Assert
            Assert.AreEqual(1, sut.Location.X, 1);
            Assert.AreEqual(_planet.YAxis.MaximumPoint, sut.Location.Y);
        }

        [Test]
        public void WhenPlanetNavigatorIsMoved500PositionsSouthTheNavigatorReturnsCorrectStatusOfHitPerimeter()
        {
            _planet = GeneratePlanetStubThatReturnsInvalidPlanetLocation();

            // Arrange 
            var sut = new Navigation(_planet, _compass, new Coordinates() { X = 1, Y = 1 },CardinalHeading.South);

            // Act
            sut.Move(500);

            //Assert
            Assert.AreEqual(NavigationState.HitPerimeter, sut.State);
        }

        [Test]
        public void WhenPlanetNavigatorIsMoved500PositionsWestPositionIsReturnedAsPlanetMinimum()
        {
            // Arrange 
            var sut = new Navigation(_planet, _compass, new Coordinates() { X = 1, Y = 1 }, CardinalHeading.West);

            // Act
            sut.Move(500);

            // Assert
            Assert.AreEqual(_planet.YAxis.MinimumPoint, sut.Location.X);
            Assert.AreEqual(1, sut.Location.Y);
        }

        [Test]
        public void WhenPlanetNavigatorIsMoved500PositionsWestTheNavigatorReturnsCorrectStatusOfHitPerimeter()
        {
            _planet = GeneratePlanetStubThatReturnsInvalidPlanetLocation();

            // Arrange 
            var sut = new Navigation(_planet, _compass, new Coordinates() { X = 1, Y = 1 }, CardinalHeading.West);

            // Act
            sut.Move(500);

            //Assert
            Assert.AreEqual(NavigationState.HitPerimeter, sut.State);
        }
    }
}
