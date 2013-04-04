using System;
using System.Threading;
using System.Threading.Tasks;
using Leap.Central.Container.Adapters;
using Leap.Central.Container.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using RabbitMQ.Client.MessagePatterns;

namespace Leap.Central.Container.Samples
{
    class RabbitMQSubscriber<TEvent> : RabbitMQAdapter<TEvent>
    {
        private readonly CancellationTokenSource _cancellationTokenSource;

        public RabbitMQSubscriber(AdapterConfig settings)
            : base(settings)
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public override void Start()
        {
            Connect();

            Task.Factory.StartNew(() =>
            {
                var susbcription = new Subscription(Model, QueueName);

                while (!_cancellationTokenSource.IsCancellationRequested)
                {
                    try
                    {
                        var e = susbcription.Next();
                        EventAgregator.Current.PublishAsync(e.FromBytes<TEvent>(e.Body));
                        susbcription.Ack();
                    }
                    catch (OperationInterruptedException ex)
                    {
                        break;
                    }
                }
            });

            base.Start();
        }

        public override void Stop()
        {
            _cancellationTokenSource.Cancel();

            Disconnect();

            base.Stop();
        }
    }
}