namespace Leap.Central.Container
{
    public interface IService
    {
        void Start();
        void Stop();
        void Resume();
        void Pause();
    }
}
