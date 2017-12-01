using Denby.Contracts.Enums;

namespace Denby.Contracts
{
    public interface IStateAware
    {
        NavigationState State { get; }
        void ResetStateToReady();
    }
}
