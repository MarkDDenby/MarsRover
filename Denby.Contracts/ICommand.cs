namespace Denby.Contracts
{
    public interface ICommand
    {
        void Execute();
        bool CanExecute();
    }
}
