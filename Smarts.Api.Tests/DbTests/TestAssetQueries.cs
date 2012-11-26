using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smarts.Api.Db;
using Smarts.Api.Models;

namespace Smarts.Api.Tests.DbTests
{
    [TestClass]
    public class TestAssetQueries
    {
        [TestMethod]
        public void TestGettingAnAssetByIdFromDb()
        {
            // Prep
            Resource actual = null;
            Resource expected = PrepSpecificSingleAsset();
            int id = 1001;

            // Call
            var db = new SmartsDbContext();
            using (var queries = new Smarts.Api.Db.ResourceQueries(db))
            {
                actual = queries.Get(id);
            }

            // Test
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGettingAnAssetByQueryFromDb()
        {
            // Prep
            Resource actual = null;
            Resource expected = PrepSpecificSingleAsset();
            int id = 1001;

            // Call
            var db = new SmartsDbContext();
            using (var queries = new Smarts.Api.Db.ResourceQueries(db))
            {
                actual = queries.GetQuery().Where(a => a.Id == id).FirstOrDefault();
            }

            // Test
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCreatingAnAsset()
        {
            // Prep
            Guid newGuid = Guid.NewGuid();

            Resource actual = new Resource()
            {
                ResourceTypeId = 1,
                ContributorGuid = new Guid("38A52BE4-9352-453E-AF97-5C3B448652F0"),
                Cost = 0.00M,
                Created = DateTime.Parse("2012-07-15 20:05:17.640"),
                Description = "Test",
                Difficulty = 0,
                Importance = 0,
                IsActive = true,
                IsScoreable = false,
                IsTestRequired = false,
                PassingScore = null,
                Title = newGuid.ToString(),
                Uri = "http://" + newGuid.ToString()
            }; 

            Resource expected = new Resource()
            {
                ResourceTypeId = 1,
                ContributorGuid = new Guid("38A52BE4-9352-453E-AF97-5C3B448652F0"),
                Cost = 0.00M,
                Created = DateTime.Parse("2012-07-15 20:05:17.640"),
                Description = "Test",
                Difficulty = 0,
                Importance = 0,
                IsActive = true,
                IsScoreable = false,
                IsTestRequired = false,
                PassingScore = null,
                Title = newGuid.ToString(),
                Uri = "http://" + newGuid.ToString()
            };

            // Call
            var db = new SmartsDbContext();
            using (var queries = new Smarts.Api.Db.ResourceQueries(db))
            {
                queries.Save(ref actual);
            }

            // Test
            Assert.IsNotNull(actual);
            if (actual.Id <= 0)
            {
                Assert.Fail("Id was not populated on save.");
            }
            
            // Clear out Id after check and compare returned entity
            actual.Id = 0;
            Assert.AreEqual(expected, actual);
        }

        private Resource PrepSpecificSingleAsset()
        {
            int id = 1001;
            var expected = new Resource()
            {
                ResourceTypeId = 1,
                ContributorGuid = new Guid("38A52BE4-9352-453E-AF97-5C3B448652F0"),
                Cost = 0.00M,
                Created = DateTime.Parse("2012-07-15 20:05:17.640"),
                Description = "Test",
                Difficulty = 0,
                Id = id,
                Importance = 0,
                IsActive = true,
                IsScoreable = false,
                IsTestRequired = false,
                PassingScore = null,
                Title = "Test",
                Uri = "http://www.nicholasbarger.com"
            };

            return expected;
        }
    }
}
