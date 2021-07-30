using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
#if NETFRAMEWORK
using System.Web.Http;
#else
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
#endif

namespace WebAPI.Service1
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

    public interface IHttpStatusCodeActionResult : IHttpActionResult
    {
        HttpStatusCode StatusCode { get; }
    }

#if !NETFRAMEWORK
    public abstract class ApiController : ControllerBase { }

    public interface IHttpActionResult { }

    public class AcrObjectResult<TValue> : IHttpStatusCodeActionResult, IActionResult
    {
        public AcrObjectResult(HttpStatusCode statusCode, TValue value, HttpRequest request)
        {
            StatusCode = statusCode;
            Value = value;
            RequestMessage = request;
        }

        public HttpStatusCode StatusCode { get; }

        public TValue Value { get; }

        public HttpRequest RequestMessage { get; }

        public Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(Value)
            {
                DeclaredType = typeof(TValue),
                StatusCode = (int)StatusCode
            };
            return result.ExecuteResultAsync(context);
        }
    }
#else
    public class AcrObjectResult<TValue> : IHttpStatusCodeActionResult
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
#endif
}