using UnityEngine;
using UnityEngine.Events;

namespace MVVM.Binders.UnityEventBinders
{
    public class IntToStringUnityEventBinder : ObservableBinder<int, string>
    {
        [SerializeField] private UnityEvent<string> _event;

        protected override string HandleValue(int value)
        {
            var result = value.ToString();
            _event.Invoke(result);
            return result;
        }
    }
}