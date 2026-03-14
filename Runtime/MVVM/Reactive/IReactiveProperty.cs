using System;

namespace MVVM.Reactive
{
    public interface IReactiveProperty<out T> : IObservable<T>
    {
        T Value { get; }
        bool HasValue { get; }

        void Unsubscribe(IObserver<T> observer);
    }
}