using System;
using System.Threading.Tasks;

namespace Leap.Central.Container.Events
{
    public interface IEventAgregator : IEventPublisher, IEventSubscriber
    {
    }

    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent @event);
        Task PublishAsync<TEvent>(TEvent @event);
    }

    public interface IEventSubscriber
    {
        IObservable<TEvent> GetEvent<TEvent>();
    }
}
