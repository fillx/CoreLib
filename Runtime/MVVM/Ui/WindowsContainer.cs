using System.Collections.Generic;
using UnityEngine;

namespace CoreLib.MVVM.Ui
{
    public class WindowsContainer : MonoBehaviour
    {
        [SerializeField] private Transform _screenContainer;
        [SerializeField] private Transform _popupsContainer;

        private readonly Dictionary<WindowViewModel, IWindowBinder> _openedPopupBinders = new();
        private IWindowBinder _openedScreenBinder;

        public void OpenPopup(WindowViewModel viewModel)
        {
            var prefabPath = GetPrefabPath(viewModel);
            var prefab = Resources.Load<GameObject>(prefabPath);
            var instance = Instantiate(prefab, _popupsContainer);
            var binder = instance.GetComponent<IWindowBinder>();

            binder.Bind(viewModel);
            _openedPopupBinders.Add(viewModel, binder);
        }

        public void ClosePopup(WindowViewModel viewModel)
        {
            var binder = _openedPopupBinders[viewModel];

            binder?.Close();
            _openedPopupBinders.Remove(viewModel);
        }

        public void OpenScreen(WindowViewModel viewModel)
        {
            if (viewModel == null) return;

            _openedScreenBinder?.Close();

            var prefabPath = GetPrefabPath(viewModel);
            var prefab = Resources.Load<GameObject>(prefabPath);
            var instance = Instantiate(prefab, _screenContainer);
            var binder = instance.GetComponent<IWindowBinder>();

            binder.Bind(viewModel);
            _openedScreenBinder = binder;
        }

        private static string GetPrefabPath(WindowViewModel viewModel)
        {
            return $"Prefabs/Ui/{viewModel.Id}";
        }
    }
}