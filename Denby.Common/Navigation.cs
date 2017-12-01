using System;
using Denby.Contracts;
using Denby.Contracts.Enums;

namespace Denby.Common
{
    public class Navigation : INavigation
    {
        public IPlanet Planet { get; private set; }
        public ICompass Compass { get; private set; }
        public ICoordinates Location { get; set; }
        public CardinalHeading Heading { get; set; }
        public NavigationState State { get; private set; }

        public Navigation(IPlanet planet, ICompass compass, ICoordinates initialLocation, CardinalHeading initialHeading)
        {
            if (planet == null)
            {
                throw new ArgumentNullException("planet");
            }
            if (compass == null)
            {
                throw new ArgumentNullException("compass");
            }
            if (initialLocation == null)
            {
                throw new ArgumentNullException("initialLocation");
            }

            Location = initialLocation;
            Heading = initialHeading;
            Planet = planet;
            Compass = compass;
        }

        public void ResetStateToReady()
        {
            State = NavigationState.Ready;
        }

        public void Rotate(Rotate rotation)
        {
            State = NavigationState.Rotating;

            Compass.Bearing = Heading;
            Compass.Rotate(rotation);
            Heading = Compass.Bearing;

            State = NavigationState.Ready;
        }
        
        public void Move(int distance)
        {
            State = NavigationState.Moving;

            var targetPosition = new Coordinates() {X = Location.X, Y = Location.Y};
            
            switch (Heading)
            {
                case CardinalHeading.North:
                    targetPosition.Y = targetPosition.Y - distance;
                    break;
                case CardinalHeading.East:
                    targetPosition.X = targetPosition.X + distance;
                    break;
                case CardinalHeading.South:
                    targetPosition.Y = targetPosition.Y + distance;
                    break;
                case CardinalHeading.West:
                    targetPosition.X = targetPosition.X - distance;
                    break;
            }

            bool requestedMoveInsidePlanetBoundary = Planet.IsLocationWithinPerimeter(targetPosition);

            targetPosition.X = Math.Min(Planet.XAxis.MaximumPoint, targetPosition.X);
            targetPosition.X = Math.Max(Planet.XAxis.MinimumPoint, targetPosition.X);

            targetPosition.Y = Math.Min(Planet.YAxis.MaximumPoint, targetPosition.Y);
            targetPosition.Y = Math.Max(Planet.XAxis.MinimumPoint, targetPosition.Y);
          
            Location = targetPosition;

            if (!requestedMoveInsidePlanetBoundary)
            {
                State = NavigationState.HitPerimeter;
            }
            else 
            {
                State = NavigationState.Ready;
            }
        }
    }
}
