using System;
using Leap.Central.Container.Adapters;
using RabbitMQ.Client;

namespace Leap.Central.Container.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            var httpInputAdapterConfig = new AdapterConfig();
            httpInputAdapterConfig.Add("BaseAddress", "http://localhost:8182");

            var rabbitConfig = new AdapterConfig();
            rabbitConfig.Add("QueueName", "ContainerSampleQueue");
            rabbitConfig.Add("HostName", "localhost");
            rabbitConfig.Add("Exchange", "ContainerSampleExchange");
            rabbitConfig.Add("ExchangeType", ExchangeType.Fanout);

            var container = new AdaptableContainer();

            container.AddAdapter(new HttpInputAdapter(httpInputAdapterConfig));
            container.AddAdapter(new ConsoleOutputAdapter<StringEvent>());
            container.AddAdapter(new RabbitMQSubscriber<StringEvent>(rabbitConfig));
            container.AddAdapter(new RabbitMQPublisher<StringEvent>(rabbitConfig));

            container.Initialize();
            container.Start();

            Console.Read();
        }
    }
}
