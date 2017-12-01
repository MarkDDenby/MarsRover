namespace Denby.Contracts
{
    public interface INavigable
    {
        void Move(int distance);
        void Rotate(Rotate rotation);
    }
}
