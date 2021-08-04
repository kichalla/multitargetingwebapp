#if NETFRAMEWORK
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace WebAPI.Service1
{
    public static class HttpRequestMessageExtensions
    {
        const string IFX_OPERATION = "IFX_OPERATION";
        public const string CLIENT_REQUEST_ID = "x-ms-client-request-id";
        
        public static string GetClientRequestId(this HttpRequestMessage request) =>
            request?.GetHeader(CLIENT_REQUEST_ID);

        public static void SetIFXOperation(this HttpRequestMessage request, object operation)
        {
            request.Properties[IFX_OPERATION] = operation;
        }

        public static string GetHeader(this HttpRequestMessage request, string headerName)
        {
            IEnumerable<string> values;
            if (!request.Headers.TryGetValues(headerName, out values))
            {
                return null;
            }
            var x = values.ToArray();

            if (x.Length > 1)
            {
                throw new InvalidOperationException("Request should only have one " + headerName + " header.");
            }
            return x[0];
        }
    }
}
#endif