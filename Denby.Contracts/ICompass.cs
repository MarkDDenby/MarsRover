namespace Denby.Contracts
{
    public interface ICompass
    {
        CardinalHeading Bearing { get; set; }
        void Rotate(Rotate direction);
    }
}
