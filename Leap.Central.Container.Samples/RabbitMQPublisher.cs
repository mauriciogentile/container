using System;
using Leap.Central.Container.Adapters;
using Leap.Central.Container.Events;

namespace Leap.Central.Container.Samples
{
    class RabbitMQPublisher<TEvent> : RabbitMQAdapter<TEvent>
    {
        private IDisposable susbcription;

        public RabbitMQPublisher(AdapterConfig settings)
            : base(settings)
        {
        }

        public override void Start()
        {
            susbcription = this.Subscribe(SendMessage);

            base.Start();
        }

        void SendMessage(TEvent message)
        {
            Connect();
            var basicProperties = Model.CreateBasicProperties();
            basicProperties.SetPersistent(true);
            Model.BasicPublish(Exchange, string.Empty, basicProperties, message.ToBytes());
        }

        public override void Stop()
        {
            susbcription.Dispose();

            Dispose();

            base.Stop();
        }
    }
}