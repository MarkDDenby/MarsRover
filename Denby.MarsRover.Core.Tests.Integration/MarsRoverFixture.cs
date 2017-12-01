using NUnit.Framework;
using Denby.Common;
using Denby.Common.Commands;
using Denby.Contracts;
using Denby.Contracts.EventArguments;

namespace Denby.MarsRover.Core.Tests.Integration
{
    [TestFixture]
    public class MarsRoverFixture
    {
        private ICompass _compass;
        private INavigation _navigation;
        private IPlanet _planet;
        private IRover _rover;
        private IRoverController _roverController;

        [SetUp]
        public void Setup()
        {
            _compass = new Compass();
            _planet = new Mars();

            _navigation = new Navigation(_planet, _compass, new Coordinates() { X = 1, Y = 0 }, CardinalHeading.South);

            _rover = new Core.MarsRover(_navigation);
            _roverController = new MarsRoverController(5, _rover);
        }

        [Test]
        public void DoesRoverNavigateToExampleCorrectPositionAndHeading()
        {
            //Arrange & Act
            _roverController.AddCommand(new MoveCommand(_rover) { Distance = 50 });
            _roverController.AddCommand(new RotateCommand(_rover) { Rotation = Rotate.Left });
            _roverController.AddCommand(new MoveCommand(_rover) { Distance = 23 });
            _roverController.AddCommand(new RotateCommand(_rover) { Rotation = Rotate.Left });
            _roverController.AddCommand(new MoveCommand(_rover) { Distance = 4 });

            //Assert
            Assert.AreEqual(24, _roverController.Rover.Location.X, 24);
            Assert.AreEqual(46, _roverController.Rover.Location.Y, 46);
            Assert.AreEqual(CardinalHeading.North, _roverController.Rover.Heading);
        }

        [Test]
        public void DoesRoverStopProcessingCommandsWhenPerimeterHit()
        {
            //Arrange & Act
            _roverController.AddCommand(new MoveCommand(_rover) { Distance = 50 });
            _roverController.AddCommand(new RotateCommand(_rover) { Rotation = Rotate.Left });
            _roverController.AddCommand(new MoveCommand(_rover) { Distance = 23 });
            _roverController.AddCommand(new MoveCommand(_rover) { Distance = _planet.XAxis.MaximumPoint });
            _roverController.AddCommand(new RotateCommand(_rover) { Rotation = Rotate.Left });

            //Assert
            Assert.AreEqual(24, _roverController.Rover.Location.X, _planet.XAxis.MaximumPoint);
            Assert.AreEqual(46, _roverController.Rover.Location.Y, 23);
            Assert.AreEqual(CardinalHeading.East, _roverController.Rover.Heading);
        }

        [Test]
        public void AfterMaximumNumberOfCommandsExecutedDoesRoverReportItsPositionAndHeading()
        {
            //Arrange
            CardinalHeading reportedHeading = CardinalHeading.South;
            ICoordinates reportedCoordinates = new Coordinates() { X = 0, Y = 0 };

            _roverController.RaiseLocationReport += delegate (object sender, LocationHeadingEventArg e)
            {
                reportedHeading = e.Heading;
                reportedCoordinates = e.Location;
            };

            //Act
            _roverController.AddCommand(new MoveCommand(_rover) { Distance = 50 });
            _roverController.AddCommand(new RotateCommand(_rover) { Rotation = Rotate.Left });
            _roverController.AddCommand(new MoveCommand(_rover) { Distance = 23 });
            _roverController.AddCommand(new RotateCommand(_rover) { Rotation = Rotate.Left });
            _roverController.AddCommand(new MoveCommand(_rover) { Distance = 4 });

            //Assert
            Assert.AreEqual(24, reportedCoordinates.X);
            Assert.AreEqual(46, reportedCoordinates.Y);
            Assert.AreEqual(CardinalHeading.North, reportedHeading);
        }
    }
}

