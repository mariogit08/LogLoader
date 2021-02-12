using System;
using System.Collections.Generic;
using System.Text;
using TReuters.LogLoader.Application.Models;
using TReuters.LogLoader.Domain.Entities;

namespace TReuters.LogLoader.Application.Adapters
{
    public static class UserAgentAdapter
    {
        public  static UserAgentViewModel ToViewModel(this UserAgent userAgent)
        {
            return new UserAgentViewModel(userAgent.UserAgentId, userAgent.Product, userAgent.ProductVersion, userAgent.SystemInformation);
        }

        public static UserAgent ToDomainModel(this UserAgentViewModel userAgent)
        {
            return new UserAgent(userAgent.Id, userAgent.Product, userAgent.ProductVersion, userAgent.SystemInformation);
        }
    }
}
