using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mvc5WebApiApp.Controllers
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
            return Ok<Car>(new Car { Make = "Tesla" });
        }
    }
}
