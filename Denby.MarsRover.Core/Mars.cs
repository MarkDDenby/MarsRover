using System;
using Denby.Common;
using Denby.Contracts;

namespace Denby.MarsRover.Core
{
    public class Mars : IPlanet
    {
        public IAxis XAxis { get; private set; }
        public IAxis YAxis { get; private set; }
        
        public Mars()
        {
            XAxis = new Axis() { MinimumPoint = 1, MaximumPoint = 100 };
            YAxis = new Axis() { MinimumPoint = 1, MaximumPoint = 100 };
        }

        public bool IsLocationWithinPerimeter(ICoordinates position)
        {
            if (position == null)
            {
                throw new ArgumentNullException("position");
            }

            return position.X <= XAxis.MaximumPoint && position.X >= XAxis.MinimumPoint &&
                              position.Y <= YAxis.MaximumPoint && position.Y >= YAxis.MinimumPoint;
        }
    }
}
