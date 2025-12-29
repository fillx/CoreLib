using CoreLib.DI;

namespace CoreLib.MVVM.Ui
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