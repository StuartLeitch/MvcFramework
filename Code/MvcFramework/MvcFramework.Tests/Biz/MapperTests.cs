using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcFramework.Biz;

namespace MvcFramework.Tests.Biz
{
    [TestClass]
    public class MapperTests
    {
        [TestMethod]
        public void AutoMapper_Mappings_Validate()
        {
            MapperMaps.Initialize();
            MapperMaps.AssertConfigurationIsValid();
        }
    }
}