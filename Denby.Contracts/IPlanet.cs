namespace Denby.Contracts
{
    public interface IPlanet
    {
        IAxis XAxis { get; }
        IAxis YAxis { get; }

        bool IsLocationWithinPerimeter(ICoordinates location);
    }
}
