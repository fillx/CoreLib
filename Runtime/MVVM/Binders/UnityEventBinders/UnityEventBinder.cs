using UnityEngine;
using UnityEngine.Events;

namespace MVVM.Binders.UnityEventBinders
{
    public abstract class UnityEventBinder<T> : ObservableBinder<T>
    {
        [SerializeField] private UnityEvent<T> _event;
        
        protected override T HandleValue(T value)
        {
            _event.Invoke(value);
            
            return value;
        }
    }
}
