using System.Collections.Generic;
using System.Linq;
using Game.State.cmd;

namespace Command
{
    public class CompositeCommandHook : ICommandPipelineHook
    {
        private readonly List<ICommandPipelineHook> _hooks;

        public CompositeCommandHook(IEnumerable<ICommandPipelineHook> hooks)
        {
            _hooks = hooks.ToList();
        }

        public void OnCommandHandled<TCommand>(TCommand command, bool success) where TCommand : ICommand
        {
            foreach (var hook in _hooks)
                hook.OnCommandHandled(command, success);
        }
    }
}