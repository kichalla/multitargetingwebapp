#if !NETFRAMEWORK
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Service1
{
    public static class HttpRequestExtensions
    {
        const string IFX_OPERATION = "IFX_OPERATION";
        public const string CLIENT_REQUEST_ID = "x-ms-client-request-id";
        
        public static string GetClientRequestId(this HttpRequest request) =>
            request?.GetHeader(CLIENT_REQUEST_ID);

        public static void SetIFXOperation(this HttpRequest request, object operation)
        {
            request.HttpContext.Items[IFX_OPERATION] = operation;
        }

        public static string GetHeader(this HttpRequest request, string headerName)
        {
            if (!request.Headers.TryGetValue(headerName, out var value))
            {
                return null;
            }
            return value;
        }
    }
}
#endif