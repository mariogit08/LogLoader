using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TReuters.LogLoader.WebAPI.Controllers
{
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [Route("api/logs")]
        [HttpGet]
        public async Task<ActionResult<List<Log>>> Get()
        {
            var rng = new Random();
            var log = new Log()
            {
                Id = 1,
                IP = "IP 192.198",
                Method = "Get",
                OriginUrl = "www.",
                Port = 8080,
                Protocol = "Http",
                ProtocolVersion = "1.1",
                RequestDate = DateTime.Now,
                StatusCodeResponse = 200,
                Timezone = "GTM",
                RequestURL = "www.",
                UserAgents = new List<UserAgent>() { new UserAgent() { Id = 1, Product = "Chrome", ProductVersion = "1.1", SystemInformation = "(Windows 64x)"} },
                UserIdentifier = "Mario"
            };
            return Ok(new List<Log>() { log });
        }

        [Route("api/logs/{id}")]
        [HttpGet]
        public async Task<ActionResult<Log>> Get([FromQuery] int id)
        {
            var rng = new Random();
            var log = new Log()
            {
                Id = 1,
                IP = "IP 192.198",
                Method = "Get",
                OriginUrl = "www.",
                Port = 8080,
                Protocol = "Http",
                ProtocolVersion = "1.1",
                RequestDate = DateTime.Now,
                StatusCodeResponse = 200,
                Timezone = "GMT",
                RequestURL = "www.",
                UserAgents = new List<UserAgent>() { new UserAgent() { Id = 1, Product = "Chrome", ProductVersion = "1.1", SystemInformation = "(Windows 64x)" } },
                UserIdentifier = "Mario"
            };
            return Ok(log);
        }

        [Route("api/logs")]
        [HttpPost]
        public async Task<ActionResult<Log>> Post([FromBody] Log log)
        {
            var asd = "";
            return Accepted(log);
        }


        [Route("api/logs/{id}")]
        [HttpPut]
        public async Task<ActionResult<Log>> Put(int id, [FromBody] Log log)
        {
            if (id != log.Id)
            {
                return BadRequest();
            }

            var asd = "";
            return Accepted(log);
        }


        [Route("api/logs/{id}")]
        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var asd = "";
            return Ok(true);
        }


        [Route("api/logs/batch")]
        [HttpPost]
        public async Task<ActionResult<bool>> Batch([FromForm] IFormFile file)
        {
            string lines = await ReadAsStringAsync(file);
            return Ok(true);
        }


        private async Task<string> ReadAsStringAsync(IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(await reader.ReadLineAsync());
            }
            return result.ToString();
        }
    }



    public class Log
    {
        public int Id { get; set; }
        public string IP { get; set; }
        public string UserIdentifier { get; set; }
        public DateTime RequestDate { get; set; }
        public string Timezone { get; set; }
        public string Method { get; set; }
        public string RequestURL { get; set; }
        public string Protocol { get; set; }
        public string ProtocolVersion { get; set; }
        public int StatusCodeResponse { get; set; }
        public int Port { get; set; }
        public string OriginUrl { get; set; }
        public IEnumerable<UserAgent> UserAgents { get; set; }
    }

    public class UserAgent
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public string ProductVersion { get; set; }
        public string SystemInformation { get; set; }
    }
}

