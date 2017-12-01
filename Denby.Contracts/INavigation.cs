namespace Denby.Contracts
{
    public interface INavigation : INavigable, IStateAware, ILocation, IHeading
    {
        IPlanet Planet { get; }
    }
}
