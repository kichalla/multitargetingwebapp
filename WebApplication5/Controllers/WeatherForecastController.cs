﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Controllers
{
    [ApiController]
    public abstract class ApiController : ControllerBase { }

    [Route("api/[controller]")]
    public class WeatherForecastController : ApiController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [AcrSwaggerOperation(OperationId = "Registries_ImportImage", Tags = new[] { "Registries" })]
        public IActionResult Get()
        {
            var rng = new Random();
            var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            return new ObjetResultWithHeaders(data, new Dictionary<string, string>
            {
                ["foo"] = "bar",
                ["a"] = "b"
            });
        }
    }

    public class ObjetResultWithHeaders : ObjectResult
    {
        private readonly IDictionary<string, string> _headers;

        public ObjetResultWithHeaders(object value, IDictionary<string, string> headers = null) : base(value)
        {
            _headers = headers;
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            if (_headers != null)
            {
                var response = context.HttpContext.Response;
                foreach (var header in _headers)
                {
                    response.Headers[header.Key] = header.Value;
                }
            }

            return base.ExecuteResultAsync(context);
        }
    }

    public class AcrSwaggerOperationAttribute : SwaggerOperationAttribute { }

    public class HttpResponseMessage : IActionResult
    {
        public Task ExecuteResultAsync(ActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
