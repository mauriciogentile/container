using System;
using System.Collections.Generic;
using Leap.Central.Container.Events;

namespace Leap.Central.Container.Adapters
{
    public interface IObseravleAdapter<out TEvent> : IAdapter, IObservable<TEvent>
    {
    }

    public interface IAdapter : IService, IDisposable
    {
        string Name { get; set; }
        AdapterStatus Status { get; }
        AdapterConfig Settings { get; }
        void Configure();
    }
}
