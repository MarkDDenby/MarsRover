using Denby.Contracts;

namespace Denby.Common.Commands
{
    public class MoveCommand : RoverCommand
    {
        public int Distance { get; set; }

        public MoveCommand(IRover rover) : base(rover)
        {
        }

        public override void Execute()
        {
            if (CanExecute())
            {
                Rover.Move(Distance);
            }
        }
    }
}
