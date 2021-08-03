using System.Net;
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
}