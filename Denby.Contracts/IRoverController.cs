using System;
using Denby.Contracts.EventArguments;

namespace Denby.Contracts
{
    public interface IRoverController
    {
        event EventHandler<LocationHeadingEventArg> RaiseLocationReport;

        IRover Rover { get; }
        int MaximumNumberOfCommands { get; }
        void AddCommand(ICommand command);
    }
}
