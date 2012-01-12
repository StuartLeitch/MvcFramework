using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.Biz;

namespace Application.Tests.Biz
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