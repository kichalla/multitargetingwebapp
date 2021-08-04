using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
#if NETFRAMEWORK
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
#else
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
#endif

namespace WebAPI.Service1
{
    public static class RequestExtensions
    {
        //public static IHttpActionResult CreateResult(this HttpRequestMessage request, HttpStatusCode statusCode)
        //{
        //    return new StatusCodeResult(statusCode, request);
        //}

        //public static IHttpActionResult CreateResult(this HttpRequestMessage request, HttpStatusCode statusCode)
        //{
        //    return new StatusCodeResult(statusCode, request);
        //}
#if NETFRAMEWORK
        public static string GetHeader(this HttpRequestMessage request, string headerName)
        {
            throw new NotImplementedException();
        }
#else
        public static string GetHeader(this HttpRequest request, string headerName)
        {
            throw new NotImplementedException();
        }
#endif
    }
}
