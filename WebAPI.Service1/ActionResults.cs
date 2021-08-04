using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
#if NETFRAMEWORK
using System.Net.Http;
using System.Web.Http;
#else
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
#endif

namespace WebAPI.Service1
{
    public interface IHttpStatusCodeActionResult : IHttpActionResult
    {
        HttpStatusCode StatusCode { get; }
    }

#if NETFRAMEWORK
    public class AcrStatusCodeResult : IHttpStatusCodeActionResult
    {
        public AcrStatusCodeResult(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage(StatusCode));
        }
    }

    public class AcrObjectResult<TValue> : IHttpStatusCodeActionResult
    {
        public AcrObjectResult(HttpStatusCode statusCode, TValue value, HttpRequestMessage request)
        {
            StatusCode = statusCode;
            Value = value;
            Request = request;
        }

        public HttpStatusCode StatusCode { get; }

        public TValue Value { get; }

        public HttpRequestMessage Request { get; }

        public IDictionary<string, string> Headers { get; set; }

        public string ReasonPhrase { get; set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = Request.CreateResponse<TValue>(StatusCode, Value);
            if (Headers != null)
            {
                foreach (var header in Headers)
                {
                    response.Headers.Add(header.Key, header.Value);
                }
            }
            if (!string.IsNullOrEmpty(ReasonPhrase))
            {
                response.ReasonPhrase = ReasonPhrase;
            }
            return Task.FromResult(response);
        }
    }
#else
    public abstract class ApiController : ControllerBase { }

    public interface IHttpActionResult { }

    public class AcrStatusCodeResult : IHttpStatusCodeActionResult, IActionResult
    {
        public AcrStatusCodeResult(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }

        public Task ExecuteResultAsync(ActionContext context)
        {
            var statusResult = new StatusCodeResult((int)StatusCode);
            return statusResult.ExecuteResultAsync(context);
        }
    }

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

        public IDictionary<string, string> Headers { get; set; }

        public string ReasonPhrase { get; set; }

        public Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(Value)
            {
                DeclaredType = typeof(TValue),
                StatusCode = (int)StatusCode
            };

            if (Headers != null)
            {
                var response = context.HttpContext.Response;
                foreach (var header in Headers)
                {
                    response.Headers[header.Key] = header.Value;
                }
            }

            if (!string.IsNullOrEmpty(ReasonPhrase))
            {
                var responseFeature = context.HttpContext.Features.Get<IHttpResponseFeature>();
                if (responseFeature != null)
                {
                    responseFeature.ReasonPhrase = ReasonPhrase;
                }
            }

            return result.ExecuteResultAsync(context);
        }
    }
#endif
}
