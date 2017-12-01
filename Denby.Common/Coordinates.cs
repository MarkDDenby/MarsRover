using Denby.Contracts;

namespace Denby.Common
{
    public class Coordinates : ICoordinates
    {
        public Coordinates()
        {
            X = 1;
            Y = 0;
        }

        public int X
        {
            get;
            set;
        }

        public int Y
        {
            get;
            set;
        }
    }
}
