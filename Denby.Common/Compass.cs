using System;
using Denby.Contracts;

namespace Denby.Common
{
    public class Compass : ICompass
    {
        public CardinalHeading Bearing { get; set; }

        public Compass()
        {
            Bearing = CardinalHeading.North;
        }

        public void Rotate(Rotate rotation)
        {
            switch (rotation)
            {
                case Contracts.Rotate.Left:
                    Bearing = RotateLeft(Bearing);
                    break;
                case Contracts.Rotate.Right:
                    Bearing = RotateRight(Bearing);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("rotation", rotation, null);
            }
        }

        private CardinalHeading RotateLeft(CardinalHeading heading)
        {
            CardinalHeading newHeading = heading;

            switch (heading)
            {
                case CardinalHeading.North:
                    newHeading = CardinalHeading.West;
                    break;
                case CardinalHeading.East:
                    newHeading = CardinalHeading.North;
                    break;
                case CardinalHeading.South:
                    newHeading = CardinalHeading.East;
                    break;
                case CardinalHeading.West:
                    newHeading = CardinalHeading.South;
                    break;
            }
            return newHeading;
        }

        private CardinalHeading RotateRight(CardinalHeading heading)
        {
            CardinalHeading newHeading = heading;

            switch (heading)
            {
                case CardinalHeading.North:
                    newHeading = CardinalHeading.East;
                    break;
                case CardinalHeading.East:
                    newHeading = CardinalHeading.South;
                    break;
                case CardinalHeading.South:
                    newHeading = CardinalHeading.West;
                    break;
                case CardinalHeading.West:
                    newHeading = CardinalHeading.North;
                    break;
            }
            return newHeading;
        }
    }
}
