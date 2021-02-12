using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TReuters.LogLoader.Application.Models;
using TReuters.LogLoader.Infra.Crosscutting;

namespace TReuters.LogLoader.Application.Interfaces
{
    public interface ILogAppService
    {
        Task<Result<IEnumerable<LogViewModel>>> GetAllLogs();
        Task<Result<IEnumerable<LogViewModel>>> GetByFilter(string ip, string userAgentProduct, int? fromHour, int? fromMinute, int? toHour, int? toMinute);
        Task<Result> InsertLogBatchFileAsync(IFormFile file);
        Task<Result> UpdateLog(LogViewModel logViewModel);
        Task<Result<LogViewModel>> GetById(int logId);
        Task<Result> InsertLog(LogViewModel logViewModel);
        Task<Result> DeleteLog(int logId);
    }
}
