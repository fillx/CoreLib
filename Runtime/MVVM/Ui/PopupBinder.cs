using UnityEngine;
using UnityEngine.UI;

namespace Morpeh.Game.Core.MVVM.Ui
{
    public abstract class PopupBinder<T> : WindowBinder<T> where T : WindowViewModel
    {
        [SerializeField] private Button _btnClose;
        [SerializeField] private Button _btnCloseAll;

        protected virtual void Start()
        {
            _btnClose?.onClick.AddListener(OnCloseButtonClicked);
            _btnCloseAll?.onClick.AddListener(OnCloseButtonClicked);
        }

        protected virtual void OnDestroy()
        {
            _btnClose?.onClick.RemoveListener(OnCloseButtonClicked);
            _btnCloseAll?.onClick.RemoveListener(OnCloseButtonClicked);
        }

        protected virtual void OnCloseButtonClicked()
        {
            ViewModel.RequestClose();
        }
    }
}