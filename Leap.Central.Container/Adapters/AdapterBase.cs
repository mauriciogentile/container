using System;
using Leap.Central.Container.Events;

namespace Leap.Central.Container.Adapters
{
    public abstract class AdapterBase<TEvent> : IObseravleAdapter<TEvent>
    {
        private string _name;

        protected AdapterBase(AdapterConfig settings)
        {
            Settings = settings;
            Status = AdapterStatus.NotStarted;
            Name = settings.AdapterName;
        }

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                {
                    return Guid.NewGuid().ToString();
                }
                return _name;
            }
            set { _name = value; }
        }

        public AdapterStatus Status { get; private set; }
        public AdapterConfig Settings { get; private set; }

        public virtual void Configure()
        {
        }

        public virtual void Start()
        {
        }

        public virtual void Stop()
        {
        }

        public virtual void Resume()
        {
        }

        public virtual void Pause()
        {
        }

        public virtual void Dispose()
        {
        }

        public IDisposable Subscribe(IObserver<TEvent> observer)
        {
            return EventAgregator.Current.GetEvent<TEvent>().Subscribe(observer);
        }
    }
}