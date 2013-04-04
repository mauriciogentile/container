using System;
using System.Collections.Generic;

namespace Leap.Central.Container
{
    public class ContainerInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IDictionary<string, string> HostInfo { get; set; }
    }
}
