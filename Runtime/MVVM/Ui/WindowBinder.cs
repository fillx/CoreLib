using UnityEngine;

namespace Morpeh.Game.Core.MVVM.Ui
{
    public abstract class WindowBinder<T> : MonoBehaviour, IWindowBinder where T : WindowViewModel
    {
        protected T ViewModel;

        public void Bind(WindowViewModel viewModel)
        {
            ViewModel = (T)viewModel;
            OnBind(ViewModel);
        }

        public virtual void Close()
        {
            //Пока уничтожаем, потом можно делать анимации на закрытие
            Destroy(gameObject);
        }

        protected virtual void OnBind(T viewModel){}
      
    }
}