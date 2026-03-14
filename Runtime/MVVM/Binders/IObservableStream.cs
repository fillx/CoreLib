using System;

namespace MVVM.Binders
{
    public interface IObservableStream<out T>
    {
        IObservable<T> OutputStream { get; }
    }
}