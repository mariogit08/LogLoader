using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TReuters.LogLoader.Application.Adapters;
using TReuters.LogLoader.Application.Interfaces;
using TReuters.LogLoader.Application.Models;
using TReuters.LogLoader.Domain.Interfaces.DBContext;
using TReuters.LogLoader.Domain.Interfaces.DomainServices;
using TReuters.LogLoader.Infra.Crosscutting;
using TReuters.LogLoader.Infra.Crosscutting.Helpers;
using TReuters.LogLoader.Infra.Service.Interfaces;

namespace TReuters.LogLoader.Application
{
    public class LogAppService : ILogAppService
    {
        private readonly IBatchFileLogExtractorService _batchFileLogExtractor;
        private readonly ILogDomainService _logDomainService;
        private readonly IDBContext _DBContext;

        public LogAppService(IBatchFileLogExtractorService batchFileLogExtractor, ILogDomainService logDomainService, IDBContext DBContext)
        {
            _batchFileLogExtractor = batchFileLogExtractor;
            _logDomainService = logDomainService;
            _DBContext = DBContext;
        }

        public async Task<Result> InsertLogBatchFileAsync(IFormFile file)
        {
            var logsExtractionResult = await _batchFileLogExtractor.ExtractLogsFromFile(file);

            async Task<bool> insertLogResult() => await _logDomainService.Insert(logsExtractionResult.Value);

            var processResult =
                          Result.Combine(logsExtractionResult)
                         .OnSuccess(insertLogResult);

            return Result.Fail("An error has ocurred, was not possible insert logs from batch file");
        }

        public async Task<Result> InsertLog(LogViewModel logViewModel)
        {
            var log = logViewModel.ToDomainModel();
            var result = await _logDomainService.Insert(log);
            if (result == true)
                return Result.Ok();
            else
                return Result.Fail("An error has ocurred, was not possible update log");
        }

        public async Task<Result> UpdateLog(LogViewModel logViewModel)
        {
            var log = logViewModel.ToDomainModel();
            var result = await _logDomainService.Update(log);
            if (result == true)
                return Result.Ok();
            else
                return Result.Fail("An error has ocurred, was not possible update log");
        }

        public async Task<Result> DeleteLog(int logId)
        {
            var log = await _DBContext.Log.GetById(logId);
            log = log.SetAvailable(false);
            var result = await _logDomainService.Update(log);
            if (result == true)
                return Result.Ok();
            else
                return Result.Fail("An error has ocurred, was not possible delete log");
        }

        public async Task<Result<IEnumerable<LogViewModel>>> GetAllLogs()
        {
            try
            {
                var viewModelLogs = (await _DBContext.Log.GetAll()).Select(a => a.ToViewModel()).Take(100);
                return Result.Ok(viewModelLogs);
            }
            catch (Exception e)
            {
                var asd = e.Message;
            }
            return Result.Fail<IEnumerable<LogViewModel>>("Error when try to get logs");
        }

        public async Task<Result<LogViewModel>> GetById(int logId)
        {
            Maybe<LogViewModel> viewModelLog = (await _DBContext.Log.GetById(logId)).ToViewModel();
            if (viewModelLog.HasNoValue)
                return Result.Fail<LogViewModel>($"Log with logId:{logId} was not found");
            else
                return Result.Ok(viewModelLog.Value); 

        }

        public async Task<Result<IEnumerable<LogViewModel>>> GetByFilter(string ip, string userAgentProduct, string fromHour, string fromMinute, string toHour, string toMinute)
        {
            var viewModelLogs = (await _DBContext.Log.GetByFilter
                                (ip,
                                userAgentProduct,
                                TryParseNullable(fromHour),
                                TryParseNullable(fromMinute),
                                TryParseNullable(toHour),
                                TryParseNullable(toMinute))).Select(a => a.ToViewModel());

            return Result.Ok(viewModelLogs);
        }

        public int? TryParseNullable(string val)
        {
            int outValue;
            return int.TryParse(val, out outValue) ? (int?)outValue : null;
        }
    }
}
