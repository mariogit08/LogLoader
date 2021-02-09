using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TReuters.LogLoader.Infra.Crosscutting;

namespace TReuters.LogLoader.Application.Interfaces
{
    public interface ILogAppService
    {
        Task<Result> InsertLogBatchFileAsync(IFormFile file);
    }
}
