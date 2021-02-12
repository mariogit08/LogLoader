using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TReuters.LogLoader.Infra.Crosscutting.Helpers
{
    public static class FileHelper
    {
        public static async Task<string> ReadAsStringAsync(this IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(await reader.ReadLineAsync());
            }
            return result.ToString();
        }

        public static string GetExtension(this IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName);
            return extension;
        }
    }
}
