using System;
using Leap.Central.Container.Adapters;

namespace Leap.Central.Container.Samples
{
    public class ConsoleOutputAdapter<TEvent> : AdapterBase<TEvent>
    {
        public ConsoleOutputAdapter()
            : base(new AdapterConfig())
        {
        }

        public override void Start()
        {
            this.Subscribe(Write);
            base.Start();
        }

        void Write(TEvent @event)
        {
            Console.WriteLine(@event.ToString());
        }
    }
}
