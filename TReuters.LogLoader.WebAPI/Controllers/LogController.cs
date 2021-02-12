using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TReuters.LogLoader.Application.Interfaces;
using TReuters.LogLoader.Application.Models;
using TReuters.LogLoader.Infra.Crosscutting;
using TReuters.LogLoader.Infra.Crosscutting.Helpers;
using TReuters.LogLoader.WebAPI.Models;

namespace TReuters.LogLoader.WebAPI.Controllers
{
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;
        private readonly ILogAppService _logAppService;

        public LogController(ILogAppService logAppService)
        {

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
            if (result.Failure)
                return NotFound(result);

            return Ok(result);
        }

        [Route("api/logs/filter")]
        [HttpGet]
        public async Task<ActionResult<LogViewModel>> Get([FromQuery] LogFilterParameters lf)
        {
            var result = await _logAppService.GetByFilter(lf.ip, lf.userAgentProduct, lf.fromHour, lf.fromMinute, lf.toHour, lf.toMinute);
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
            var result = await _logAppService.UpdateLog(log);
            return Accepted(result);
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
        public async Task<ActionResult<bool>> PostBatch([FromForm] IFormFile file)
        {
            if (file == null || (file.GetExtension() != ".log" && file.GetExtension() != ".txt"))
                return BadRequest();


            var result = await _logAppService.InsertLogBatchFileAsync(file);
            return Ok(result);
        }
    }
}

