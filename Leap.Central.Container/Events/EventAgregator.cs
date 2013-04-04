using System;
using System.Collections.Concurrent;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Leap.Central.Container.Events
{
    public class EventAgregator : IEventAgregator
    {
        private readonly ConcurrentDictionary<Type, object> _subjects = new ConcurrentDictionary<Type, object>();
        private static EventAgregator _singleton;
        private static readonly object _lock = new object();

        private EventAgregator()
        {
        }

        public static EventAgregator Current
        {
            get
            {
                if (_singleton == null)
                {
                    lock (_lock)
                    {
                        if (_singleton == null)
                        {
                            _singleton = new EventAgregator();
                        }
                    }
                }
                return _singleton;
            }
        }

        public IObservable<TEvent> GetEvent<TEvent>()
        {
            var subject = (ISubject<TEvent>)_subjects.GetOrAdd(typeof(TEvent), t => new Subject<TEvent>());
            return subject.AsObservable();
        }

        public void Publish<TEvent>(TEvent eventData)
        {
            object subject;
            if (_subjects.TryGetValue(typeof(TEvent), out subject))
            {
                var observer = (ISubject<TEvent>)subject;
                observer.OnNext(eventData);
            }
        }

        public Task PublishAsync<TEvent>(TEvent @event)
        {
            return Task.Factory.StartNew(() => Publish(@event));
        }
    }
}
