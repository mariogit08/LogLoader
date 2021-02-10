using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace TReuters.LogLoader.Infra.Service.BatchFileLogServices
{
    internal static class RequestDateExtractor
    {
        public static DateTime ExtractAndConvertRequestDate(this Match generalMatch)
        {
            DateTime.TryParseExact(generalMatch.Groups["requestDate"].Value, "dd/MMM/yyyy:HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime requestDate);
            return requestDate;
        }
    }
}
