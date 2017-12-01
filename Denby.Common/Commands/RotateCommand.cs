using Denby.Contracts;

namespace Denby.Common.Commands
{
    public class RotateCommand :  RoverCommand
    {
        public Rotate Rotation { get; set; }

        public RotateCommand(IRover rover) : base(rover)
        {
        }

        public override void Execute()
        {
            if (CanExecute())
            {
                Rover.Rotate(Rotation);
            }
        }
    }
}
