using System;
using Denby.Contracts;
using Denby.Contracts.Enums;

namespace Denby.MarsRover.Core
{
    public class MarsRover : IRover
    {
        private readonly INavigation _navigation;

        public MarsRover(INavigation navigation)
        {
            if (navigation == null)
            {
                throw new ArgumentNullException("navigation");
            }
            
            _navigation = navigation;
        }

        public NavigationState State
        {
            get { return _navigation.State; }
        }
        
        public ICoordinates Location
        {
            get { return _navigation.Location; }
        }

        public CardinalHeading Heading 
        {
            get { return _navigation.Heading; }
        }

        public void ResetStateToReady()
        {
            _navigation.ResetStateToReady();
        }
        
        public void Move(int distance)
        {
            _navigation.Move(distance);
        }

        public void Rotate(Rotate rotation)
        {
            _navigation.Rotate(rotation);
        }
   }
}
