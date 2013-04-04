using System;
using System.Collections.Generic;
using Leap.Central.Container.Adapters;

namespace Leap.Central.Container
{
    public interface IContaner
    {
        IConfigurationService ConfigurationService { get; }
        IContainerRegistrationService ContainerRegistrationService { get; }
        IHearbeatService HearbeatService { get; }
        ILogger Logger { get; }

        void Initialize();
        void Reset();
        void TearDown();
        ContainerInfo GetContainerInfo();
        
    }
}
