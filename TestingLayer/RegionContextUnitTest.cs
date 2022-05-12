using DataLayer;
using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace TestingLayer
{
    public class RegionContextUnitTest
    {
        private IzpitDBContext dbContext;
        private RegionContext regionContext;
        DbContextOptionsBuilder builder;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

        }

        [SetUp]

        public void Setup()
        {
            builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            dbContext = new IzpitDBContext(builder.Options);
            regionContext = new RegionContext(dbContext);
        }

        [Test]

        public void TestCreateRegion()
        {
            int regionsBefore = regionContext.ReadAll().Count();

            regionContext.Create(new Region("test"));

            int regionsAfter = regionContext.ReadAll().Count();

            Assert.IsTrue(regionsBefore != regionsAfter);
        }

        [Test]

        public void TestReadRegion()
        {
            regionContext.Create(new Region("test"));

            Region region = regionContext.Read(1);

            Assert.That(region != null, "There is no record with id 1");
        }

        [Test]

        public void TestUpdateRegion()
        {
            regionContext.Create(new Region("test"));

            Region region = regionContext.Read(1);

            region.Name = "test2";

            regionContext.Update(region);

            Region region2 = regionContext.Read(1);

            Assert.IsTrue(region.Name == "test2", "Region Update() failed");
        }

        [Test]

        public void TestDeleteRegion()
        {
            regionContext.Create(new Region("test"));

            int regionsBefore = regionContext.ReadAll().Count();

            regionContext.Delete(1);

            int regionsAfter = regionContext.ReadAll().Count();

            Assert.IsTrue(regionsBefore != regionsAfter);
        }
    }
}
