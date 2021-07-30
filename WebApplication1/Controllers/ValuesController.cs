using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
    }

    public class CarsController : ApiController
    {
        [HttpGet]
        [Route("api/car")]
        public IHttpActionResult Get()
        {
            return new AcrObjectResult<Car>(HttpStatusCode.OK, new Car { Make = "Tesla" }, Request);
        }
    }

    public interface IAcrActionResult { }

    public interface IAcrStatusCodeActionResult : IAcrActionResult
    {
        HttpStatusCode StatusCode { get; }
    }

    public class AcrObjectResult<TValue> : IHttpActionResult, IAcrStatusCodeActionResult
    {
        public AcrObjectResult(HttpStatusCode statusCode, TValue value, HttpRequestMessage requestMessage)
        {
            StatusCode = statusCode;
            Value = value;
            RequestMessage = requestMessage;
        }

        public HttpStatusCode StatusCode { get; }

        public TValue Value { get; }

        public HttpRequestMessage RequestMessage { get; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = RequestMessage.CreateResponse<TValue>(StatusCode, Value);
            return Task.FromResult(response);
        }
    }
}
