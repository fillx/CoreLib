using DI;

namespace MVVM.Ui
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