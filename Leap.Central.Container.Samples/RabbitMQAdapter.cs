using System;
using Leap.Central.Container.Adapters;
using RabbitMQ.Client;

namespace Leap.Central.Container.Samples
{
    class RabbitMQAdapter<TEvent> : AdapterBase<TEvent>
    {
        protected readonly string Exchange;
        protected readonly string ExchangeType;
        protected readonly string QueueName;
        protected readonly string HostName;

        protected IModel Model;
        protected IConnection Connection;

        public RabbitMQAdapter(AdapterConfig settings)
            : base(settings)
        {
            Exchange = settings["Exchange"];
            ExchangeType = settings["ExchangeType"];
            HostName = settings["HostName"];
            QueueName = settings["QueueName"];
        }

        protected void Connect()
        {
            if (Connection != null && Connection.IsOpen)
            {
                return;
            }

            var connectionFactory = new ConnectionFactory { HostName = HostName };
            Connection = connectionFactory.CreateConnection();

            Model = Connection.CreateModel();

            var queueOk = Model.QueueDeclare(QueueName, true, false, false, null);

            if (!string.IsNullOrEmpty(Exchange))
            {
                Model.ExchangeDeclare(Exchange, ExchangeType, true);
                Model.QueueBind(queueOk, Exchange, string.Empty);
            }
        }

        public void Disconnect()
        {
            if (Connection != null && Connection.IsOpen)
            {
                Connection.Close();
                Connection.Dispose();
            }
            if (Model != null)
            {
                Model.Abort();
                Model.Dispose();
            }
        }

        public override void Dispose()
        {
            Disconnect();
            base.Dispose();
        }
    }
}
