using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IntegratedFlghtDynamicSystem.Models;

namespace IntegratedFlghtDynamicSystem.Tests.Fake
{
    public class FakeHttpRequestMessage : HttpRequestMessage
    {
        public HttpResponseMessage CreateResponse(HttpStatusCode statusCode, SpacecraftInitialData initData)
        {
            return  new HttpResponseMessage(statusCode);
        }
    }
}
