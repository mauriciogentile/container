using System.Collections.Generic;

namespace Leap.Central.Container
{
    public interface IConfigurationService
    {
        IDictionary<string, string> GetSettings();
    }
}
