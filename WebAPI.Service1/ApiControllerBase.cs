using System.Collections.Generic;
using System.Net;
#if NETFRAMEWORK
using System.Web.Http;
#else
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
#endif

namespace WebAPI.Service1
{
    public class ApiControllerBase : ApiController
    {
        public IHttpActionResult CreateResult<TValue>(
            HttpStatusCode httpStatusCode,
            TValue value,
            IDictionary<string, string> headers = null,
            string reasonPhrase = null)
        {
            return new AcrObjectResult<TValue>(httpStatusCode, value, Request)
            {
                Headers = headers,
                ReasonPhrase = reasonPhrase
            };
        }
    }
}
