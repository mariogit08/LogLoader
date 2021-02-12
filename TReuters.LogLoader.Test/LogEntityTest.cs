using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TReuters.LogLoader.Domain.Entities;

namespace TReuters.LogLoader
{
    [TestFixture]
    public class LogEntityTest
    {
        [TestCase("asd")]
        [TestCase("13211.5p")]
        [TestCase("8888")]
        [TestCase("21444123")]
        public void ShouldThrowsArgumentExceptionWhenIPIsNotValid(string ip)
        {
            Assert.Throws<ArgumentException>(() => { var log = new Log().SetIP(ip); });
        }

        [TestCase("192.168.0.1")]
        [TestCase("132.10.11.5")]
        [TestCase("8.8.8.8")]
        [TestCase("214.44.55.123")]

        public void ShouldNotThrowsAnyExceptionWhenInputValidIP(string ip)
        {
            Assert.DoesNotThrow(() => { var log = new Log().SetIP(ip); });
        }

        public void ShouldThrowsArgumentExceptionWhenRequestDateIsLowerThanDatabaseMinDate(string ip)
        {
            DateTime.TryParse("1970-01-01 08:00:00", out DateTime requestDateOut);
            Assert.Throws<ArgumentException>(() => { var log = new Log().SetRequestDate(requestDateOut); });
        }
    }
}
