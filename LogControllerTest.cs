
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;
using TReuters.LogLoader.Infra.IOC;
using TReuters.LogLoader.WebAPI.Controllers;

namespace TReuters.LogLoader
{
    [TestFixture]
    public class LogControllerTest
    {
        private LogController _logController;

        //Levantar execeção quando arquivo for invalido para leitura ou formato incorreto
        
        public LogControllerTest()
        {
            string solution_dir = Path.GetDirectoryName(Path.GetDirectoryName(
    TestContext.CurrentContext.TestDirectory));
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(@"./TReuters.LogLoader.WebAPI")
            .AddJsonFile("appsettings.json")
            .Build();

            

            var services = new ServiceCollection();
            IOCBootstrapper.RegisterInstances(services);

            services.AddTransient<IConfiguration>((ser) => configuration);
            services.AddTransient<LogController, LogController>();
            
            var serviceProvider = services.BuildServiceProvider();
            _logController = serviceProvider.GetService<LogController>();
        }

        [Test]
        public void ShouldReturnsBadRequestWhenIsNotTxtFile()
        {
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(_ => _.FileName).Returns("TReuters.img");
            var result = _logController.PostBatch(fileMock.Object).Result;
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        [Test]
        public void ShouldReturnsBadRequestWhenFileIsNull()
        {
            var fileMock = new Mock<IFormFile>();            
            var result = _logController.PostBatch(null).Result;
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        public static IFormFile AsMockIFormFile(FileInfo physicalFile)
        {
            var fileMock = new Mock<IFormFile>();
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(physicalFile.OpenRead());
            writer.Flush();
            ms.Position = 0;
            var fileName = physicalFile.Name;
            //Setup mock file using info from physical file
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            fileMock.Setup(m => m.OpenReadStream()).Returns(ms);
            fileMock.Setup(m => m.ContentDisposition).Returns(string.Format("inline; filename={0}", fileName));
            //...setup other members (code removed for brevity)


            return fileMock.Object;
        }

        [Test]
        public async Task Controller_Should_Upload_FormFile()
        {
            //// Arrange.
            //var fileMock = new Mock<IFormFile>();
            //var physicalFile = new FileInfo("filePath");
            //var ms = new MemoryStream();
            //var writer = new StreamWriter(ms);
            //writer.Write(physicalFile.OpenRead());
            //writer.Flush();
            //ms.Position = 0;
            //var fileName = physicalFile.Name;
            ////Setup mock file using info from physical file
            //fileMock.Setup(_ => _.FileName).Returns(fileName);
            //fileMock.Setup(_ => _.Length).Returns(ms.Length);
            //fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            //fileMock.Setup(_ => _.ContentDisposition).Returns(string.Format("inline; filename={0}", fileName));
            ////...setup other members as needed.

            //var file = fileMock.Object;

            //// Act.
            //var result = await _logController.Upload(file);

            ////Assert.
            //Assert.IsInstanceOf(result, typeof(IActionResult));
        }
    }
}
