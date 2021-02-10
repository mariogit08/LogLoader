using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TReuters.LogLoader.Domain.Entities;
using TReuters.LogLoader.Infra.Crosscutting;

namespace TReuters.LogLoader.Infra.Service.Interfaces
{
    public interface IBatchFileLogExtractorService
    {
        Task<Result<List<Log>>> ExtractLogsFromFile(IFormFile file);
    }

}
