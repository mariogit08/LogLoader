
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;
using TReuters.LogLoader.Application.Interfaces;
using TReuters.LogLoader.Application.Models;
using TReuters.LogLoader.Domain.Entities;
using TReuters.LogLoader.Domain.Interfaces.DBContext;
using TReuters.LogLoader.Domain.Interfaces.Repositories;
using TReuters.LogLoader.Infra.Crosscutting;
using TReuters.LogLoader.Infra.IOC;
using TReuters.LogLoader.Infra.IOC.Modules;
using TReuters.LogLoader.WebAPI.Controllers;

namespace TReuters.LogLoader
{
    [TestFixture]
    public class LogControllerTest
    {
        private LogController _logController;
        private ServiceCollection _services;
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            string solution_dir = Path.GetDirectoryName(Path.GetDirectoryName(
            TestContext.CurrentContext.TestDirectory));

            var path = $@"{solution_dir}\Debug\netcoreapp3.1";

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json")
            .Build();

            _services = new ServiceCollection();
            _services.RegisterAllIOCModules();

            _services.AddTransient<IConfiguration>((ser) => configuration);
            _services.AddTransient<LogController, LogController>();
            
            _serviceProvider = _services.BuildServiceProvider();

            _logController = _serviceProvider.GetService<LogController>();
        }

        

        #region GetById

        [Test]
        public void GetLogByIdShouldReturnOkResultWhenInputExistentId()
        {
            //Setup AppService Instance            
            var logAppServiceMock = new Mock<ILogAppService>();
            logAppServiceMock.Setup(_ => _.GetById(1)).Returns(Task.FromResult(Result.Ok(new LogViewModel())));

            _services.AddTransient((ser) => logAppServiceMock.Object);
            _serviceProvider = _services.BuildServiceProvider();

            _logController = _serviceProvider.GetService<LogController>();

            var result = _logController.Get(1).Result;

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }


        [Test]
        public void GetLogByIdShoudReturnNotFoundResultWhenInputAnNonExistentId()
        {
            const int nonExistentId = -1000;
            var result = _logController.Get(nonExistentId).Result;

            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [Test]
        public void PutLogShoudReturnBadRequestWhenUrlIdAndLogObjectIdDontMatch()
        {
            const int anyId = 100;
            const int differentId = 200;
            var result = _logController.Put(anyId, new LogViewModel() { LogId = differentId }).Result;

            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }
        #endregion

        [TestCase("TReuters.pdf")]
        [TestCase("TReuters.jpg")]
        [TestCase("TReuters.html")]
        [TestCase("TReuters.bat")]
        public void ShouldReturnsBadRequestWhenIsNotTxtFile(string fileName)
        {
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            var result = _logController.PostBatch(fileMock.Object).Result;
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        [Test]
        public void ShouldReturnsBadRequestWhenFileIsNull()
        {
            var result = _logController.PostBatch(null).Result;
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

    }
}
