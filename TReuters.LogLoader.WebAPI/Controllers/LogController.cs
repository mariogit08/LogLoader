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

            var result = _logAppService.UpdateLog(log);
            return Accepted(result);
        }


        [Route("api/logs/{id}")]
        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = _logAppService.DeleteLog(id);
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

