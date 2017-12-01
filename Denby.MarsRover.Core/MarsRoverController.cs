using System;
using System.Collections.Generic;
using Denby.Contracts.EventArguments;
using Denby.Contracts;

namespace Denby.MarsRover.Core
{
    public class MarsRoverController : IRoverController
    {
        public event EventHandler<LocationHeadingEventArg> RaiseLocationReport;
        public int MaximumNumberOfCommands { get; private set; }
        public IRover Rover { get; private set; }
        private readonly Queue<ICommand> _commands = new Queue<ICommand>();

        public MarsRoverController(int maximumNumberOfCommands, IRover rover)
        {
            if (rover == null)
            {
                throw new ArgumentNullException("rover");
            }

            MaximumNumberOfCommands = maximumNumberOfCommands;
            Rover = rover;
        }

        private void ExecuteCommands()
        {
            while (_commands.Count > 0 ) 
            {
                ICommand command = _commands.Dequeue();
                command.Execute();
            }

            Rover.ResetStateToReady();

            OnRaiseLocationReport(new LocationHeadingEventArg(Rover.Location, Rover.Heading));
        }

        public void AddCommand(ICommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            _commands.Enqueue(command);

            if (_commands.Count >= MaximumNumberOfCommands)
            {
                ExecuteCommands();
            }
        }

        protected virtual void OnRaiseLocationReport(LocationHeadingEventArg e)
        {
            EventHandler<LocationHeadingEventArg> handler = RaiseLocationReport;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
