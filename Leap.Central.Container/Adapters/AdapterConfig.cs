using System.Collections.Generic;

namespace Leap.Central.Container.Adapters
{
    public class AdapterConfig : Dictionary<string, string>
    {
        public string AdapterName { get; set; }
    }
}
