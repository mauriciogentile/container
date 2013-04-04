using System;
using System.Collections.Generic;
using Leap.Central.Container.Adapters;

namespace Leap.Central.Container
{
    public abstract class ContainerBase : IContaner
    {
        public IConfigurationService ConfigurationService { get; set; }
        public IContainerRegistrationService ContainerRegistrationService { get; set; }
        public IHearbeatService HearbeatService { get; set; }
        public ILogger Logger { get; set; }

        protected ContainerBase()
        {
        }

        public virtual void Initialize()
        {
        }

        public void Reset()
        {
        }

        public void TearDown()
        {
        }

        public ContainerInfo GetContainerInfo()
        {
            throw new NotImplementedException();
        }
    }
}