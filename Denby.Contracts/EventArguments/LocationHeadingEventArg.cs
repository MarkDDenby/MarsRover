using System;

namespace Denby.Contracts.EventArguments
{
    public class LocationHeadingEventArg : EventArgs
    {
        public ICoordinates Location { get; private set; }
        public CardinalHeading Heading { get; private set; }

        public LocationHeadingEventArg(ICoordinates location, CardinalHeading heading)
        {
            Location = location;
            Heading = heading;
        }
    }
}
