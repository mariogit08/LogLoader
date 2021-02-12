
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
        }

        public LogControllerTest()
        {

        }

        #region GetById

        [Test]
        public void GetByIdShouldReturnOkResult()
        {
            //Setup AppService Instance            
            var logAppServiceMock = new Mock<ILogAppService>();
            logAppServiceMock.Setup(_ => _.GetById(1)).Returns(Task.FromResult(Result.Ok(new LogViewModel())));

            _services.AddTransient((ser) => logAppServiceMock.Object);
            _serviceProvider = _services.BuildServiceProvider();

            var logController = _serviceProvider.GetService<LogController>();

            var result = logController.Get(1).Result;

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }


        [Test]
        public void Task_GetPostById_Return_NotFoundResult()
        {
            var logController = _serviceProvider.GetService<LogController>();

            const int nonExistentId = -1000;
            var result = logController.Get(nonExistentId).Result;

            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        //[Fact]
        //public async void Task_GetPostById_Return_BadRequestResult()
        //{
        //    //Arrange  
        //    var controller = new PostController(repository);
        //    int? postId = null;

        //    //Act  
        //    var data = await controller.GetPost(postId);

        //    //Assert  
        //    Assert.IsType<BadRequestResult>(data);
        //}

        //[Fact]
        //public async void Task_GetPostById_MatchResult()
        //{
        //    //Arrange  
        //    var controller = new PostController(repository);
        //    int? postId = 1;

        //    //Act  
        //    var data = await controller.GetPost(postId);

        //    //Assert  
        //    Assert.IsType<OkObjectResult>(data);

        //    var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
        //    var post = okResult.Value.Should().BeAssignableTo<PostViewModel>().Subject;

        //    Assert.Equal("Test Title 1", post.Title);
        //    Assert.Equal("Test Description 1", post.Description);
        //}

        #endregion



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
