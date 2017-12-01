using System;
using Denby.Contracts;
using Denby.Contracts.Enums;

namespace Denby.Common
{
    public abstract class RoverCommand : ICommand
    {
        protected IRover Rover;

        protected RoverCommand(IRover rover)
        {
            if (rover == null)
            {
                throw new ArgumentNullException("rover");
            }
            
            Rover = rover;
        }

        public abstract void Execute();

        public virtual bool CanExecute()
        {
            return Rover.State == NavigationState.Ready;
        }
    }
}
