using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TReuters.LogLoader.Application.Interfaces;
using TReuters.LogLoader.Application.Models;

namespace TReuters.LogLoader.WebAPI.Controllers
{
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;
        private readonly ILogAppService _logAppService;


        public LogController(ILogger<LogController> logger, ILogAppService logAppService)
        {
            _logger = logger;
            _logAppService = logAppService;
        }

        [Route("api/logs")]
        [HttpGet]
        public async Task<ActionResult<List<LogViewModel>>> Get()
        {
            var result = await _logAppService.GetAllLogs();
            return Ok(result);
        }

        [Route("api/logs/{id}")]
        [HttpGet]
        public async Task<ActionResult<LogViewModel>> Get(int id)
        {
            var result = await _logAppService.GetById(id);
            return Ok(result);
        }

        [Route("api/logs/filter")]
        [HttpGet]
        public async Task<ActionResult<LogViewModel>> Get([FromQuery] LogFilterParameters lf)
        {
            var result = await _logAppService.GetByFilter(lf.ip, lf.userAgentProduct, lf.fromHour, lf.fromMinute, lf.toHour, lf.toMinute);
            return Ok(result);
        }

        public class LogFilterParameters
        {
            public string ip { get; set; }
            public string userAgentProduct { get; set; }
            public string fromHour { get; set; }
            public string fromMinute { get; set; }
            public string toHour { get; set; }
            public string toMinute { get; set; }

        }

        [Route("api/logs")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LogViewModel log)
        {
            var result = await _logAppService.InsertLog(log);
            return Accepted(result);
        }


        [Route("api/logs/{id}")]
        [HttpPut]
        public async Task<ActionResult<LogViewModel>> Put(int id, [FromBody] LogViewModel log)
        {
            if (id != log.LogId)
            {
                return BadRequest();
            }

            try
            {
                var result = _logAppService.UpdateLog(log);
            }
            catch (Exception e)
            {

                throw;
            }
            return Accepted(new { });
        }


        [Route("api/logs/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _logAppService.DeleteLog(id);
            return Ok(result);
        }


        [Route("api/logs/batch")]
        [HttpPost]
        public async Task<ActionResult<bool>> Batch([FromForm] IFormFile file)
        {
            var result = await _logAppService.InsertLogBatchFileAsync(file);
            return Ok(result);
        }
    }
}

