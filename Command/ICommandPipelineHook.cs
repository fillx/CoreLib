namespace Command
{
    public interface ICommandPipelineHook 
    {
        void OnCommandHandled<TCommand>(TCommand command, bool success) where TCommand : ICommand;
    }
}