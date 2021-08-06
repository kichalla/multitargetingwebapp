using System.Net;
using System.Collections.Generic;
using System;
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

    public class CarsController : ApiControllerBase
    {
        [HttpGet]
        [Route("api/car")]
        public IHttpActionResult Get()
        {
            var clientRequestId = Request.GetClientRequestId();

            return CreateResult(
                HttpStatusCode.OK, 
                new Car { Make = "Tesla", Model = "Model S" },
                headers: new Dictionary<string, string> 
                { 
                    ["header1"] = "header1-value",
                    ["clientrequestid"] = clientRequestId
                },
                reasonPhrase: "custom phrase here");
        }
    }
}