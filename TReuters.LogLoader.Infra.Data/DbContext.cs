using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using TReuters.LogLoader.Domain.Interfaces;
using TReuters.LogLoader.Domain.Interfaces.DBContext;
using TReuters.LogLoader.Domain.Interfaces.Repositories;
using TReuters.LogLoader.Infra.Data.Repositories;

namespace TReuters.LogLoader.Infra.Data
{
    public class DBContext : IDBContext
    {
        private IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IConfiguration configuration;
        private IUnitOfWork unitOfWork;

        private ILogRepository log;
        private IUserAgentRepository userAgent;

        public DBContext(IUnitOfWorkFactory unitOfWorkFactory, IConfiguration configuration)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.configuration = configuration;
        }

        public ILogRepository Log =>
            log ?? (log = new LogRepository(UnitOfWork));

        public IUserAgentRepository UserAgent =>
            userAgent ?? (userAgent = new UserAgentRepository(UnitOfWork));

        protected IUnitOfWork UnitOfWork =>
            unitOfWork ?? (unitOfWork = unitOfWorkFactory.Create(configuration.GetSection($"ConnectionStrings:{ConnectionName.LogLoaderDB}").Value));

        public void Commit()
        {
            try
            {
                UnitOfWork.Commit();
            }
            finally
            {
                Reset();
            }
        }

        public void Rollback()
        {
            try
            {
                UnitOfWork.Rollback();
            }
            finally
            {
                Reset();
            }
        }

        private void Reset()
        {
            unitOfWork = null;
            log = null;
        }
    }
}


