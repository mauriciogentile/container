using System.Collections.Generic;
using Leap.Central.Container.Adapters;

namespace Leap.Central.Container
{
    public class AdaptableContainer : ContainerBase, IService
    {
        public List<IAdapter> Adapters { get; private set; }

        public AdaptableContainer()
        {
            Adapters = new List<IAdapter>();
        }

        public void AddAdapter(IAdapter adapter)
        {
            Adapters.Add(adapter);
        }

        public void Start()
        {
            Adapters.ForEach(x => x.Start());
        }

        public void Stop()
        {
            Adapters.ForEach(x => x.Stop());
        }

        public void Resume()
        {
            throw new System.NotImplementedException();
        }

        public void Pause()
        {
            throw new System.NotImplementedException();
        }
    }
}
