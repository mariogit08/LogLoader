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
using TReuters.LogLoader.Application.Interfaces;
using TReuters.LogLoader.Application.Models;
using TReuters.LogLoader.Infra.Data;

namespace TReuters.LogLoader.WebAPI.Controllers
{
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ILogAppService _logAppService;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, ILogAppService logAppService)
        {
            _logger = logger;
            _logAppService = logAppService;
        }

        [Route("api/logs")]
        [HttpGet]
        public async Task<ActionResult<List<LogViewModel>>> Get()
        {
            var rng = new Random();
            var log = new LogViewModel()
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
                UserAgents = new List<UserAgentViewModel>() { new UserAgentViewModel() { Id = 1, Product = "Chrome", ProductVersion = "1.1", SystemInformation = "(Windows 64x)" } },
                UserIdentifier = "Mario"
            };
            return Ok(new List<LogViewModel>() { log });
        }

        [Route("api/logs/{id}")]
        [HttpGet]
        public async Task<ActionResult<LogViewModel>> Get([FromQuery] int id)
        {
            var rng = new Random();
            var log = new LogViewModel()
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
                UserAgents = new List<UserAgentViewModel>() { new UserAgentViewModel() { Id = 1, Product = "Chrome", ProductVersion = "1.1", SystemInformation = "(Windows 64x)" } },
                UserIdentifier = "Mario"
            };
            return Ok(log);
        }

        [Route("api/logs")]
        [HttpPost]
        public async Task<ActionResult<LogViewModel>> Post([FromBody] LogViewModel log)
        {
            var asd = "";
            return Accepted(log);
        }


        [Route("api/logs/{id}")]
        [HttpPut]
        public async Task<ActionResult<LogViewModel>> Put(int id, [FromBody] LogViewModel log)
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
            var con = new PostgreConnectionFactory();
            con.CreateConnection("");
            var result = await _logAppService.InsertLogBatchFileAsync(file);
            if (result.Success)
                return Ok(true);
            else
                return Ok(false);
        }
    }
}

