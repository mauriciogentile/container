using System;

namespace Leap.Central.Container
{
    public interface IContainerRegistrationService
    {
        void Register(ContainerInfo info);
        void Unregister(Guid containerId);
    }
}