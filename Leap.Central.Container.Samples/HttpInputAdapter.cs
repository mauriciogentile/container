using System;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Leap.Central.Container.Adapters;
using Leap.Central.Container.Events;

namespace Leap.Central.Container.Samples
{
    public class HttpInputAdapter : AdapterBase<StringEvent>
    {
        private readonly HttpSelfHostServer _server;
        private readonly HttpSelfHostConfiguration _config;

        public HttpInputAdapter(AdapterConfig config)
            : base(config)
        {
            _config = new HttpSelfHostConfiguration(Settings["BaseAddress"]);
            _config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            _server = new HttpSelfHostServer(_config);
        }

        public override void Start()
        {
            base.Start();
            _server.OpenAsync();
        }

        public override void Stop()
        {
            base.Stop();
            _server.CloseAsync();
        }
    }

    public class AccountController : ApiController
    {
        [HttpGet]
        public string Get(int id)
        {
            EventAgregator.Current.PublishAsync(new StringEvent { Payload = string.Format("Account ID is '{0}'", id) });
            return Guid.NewGuid().ToString();
        }
    }
}
