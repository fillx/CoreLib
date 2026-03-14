using UnityEngine;
using UnityEngine.UI;

namespace MVVM.Binders.Commands
{
    [RequireComponent(typeof(Button))]
    public class ButtonToCommandBinder : CommandBinder
    {
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(ExecuteCommand);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ExecuteCommand);
        }

#if UNITY_EDITOR
        private void Reset()
        {
            _button = GetComponent<Button>();
        }
#endif
    }
}