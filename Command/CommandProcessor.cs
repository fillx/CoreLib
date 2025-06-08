using System;
using System.Collections.Generic;

namespace Command
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly Dictionary<Type, object> _handlersMap = new();
        private readonly ICommandPipelineHook _pipelineHook;

        public CommandProcessor(ICommandPipelineHook pipelineHook)
        {
            _pipelineHook = pipelineHook;
        }
        
        public void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand
        {
            _handlersMap[typeof(TCommand)] = handler;
        }

        public bool Process<TCommand>(TCommand command) where TCommand : ICommand
        {
            bool success = false;

            if (_handlersMap.TryGetValue(typeof(TCommand), out var handler))
            {
                var typedHandler = (ICommandHandler<TCommand>)handler;
                success = typedHandler.Handle(command);
            }

            _pipelineHook.OnCommandHandled(command, success);
            return success;
        }
    }
}