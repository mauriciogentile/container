using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Leap.Central.Container.Events
{
    [Serializable]
    public abstract class Event
    {
        protected Event()
        {
            Id = Guid.NewGuid();
            Timestamp = DateTimeOffset.UtcNow;
            Metadata = new Dictionary<string, object>();
        }

        public Guid Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public IDictionary<string, object> Metadata { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Timestamp: {1}", Id, Timestamp);
        }
    }

    [Serializable]
    public class Event<T> : Event
    {
        public T Payload { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, Payload: {1}", base.ToString(), Payload);
        }
    }

    public static class EventExtensions
    {
        public static byte[] ToBytes(this object @event)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, @event);
                return stream.ToArray();
            }
        }

        public static object FromBytes<T>(this object @event, byte[] bytes)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream(bytes))
            {
                return formatter.Deserialize(stream);
            }
        }
    }
}
