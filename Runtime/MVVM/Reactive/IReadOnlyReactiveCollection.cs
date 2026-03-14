using System;
using System.Collections.Generic;

namespace MVVM.Reactive
{
    public interface IReadOnlyReactiveCollection<out T> : IReadOnlyCollection<T>, IReadOnlyList<T>
    {
        IObservable<T> Added { get; }
        IObservable<T> Removed { get; }
    }
}