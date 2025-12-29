using Morpeh.Game.Core.DI;

namespace Morpeh.Game.Core.MVVM.Ui
{
    public abstract class UiManager
    {
        protected readonly DiContainer Container;
        protected UiManager(DiContainer container)
        {
            Container = container;
        }
    }
}