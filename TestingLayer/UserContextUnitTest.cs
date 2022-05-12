using DataLayer;
using BusinessLayer;

using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingLayer
{
    public class UserContextUnitTest
    {
        private IzpitDBContext dbContext;

        private UserContext userContext;

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
            userContext = new UserContext(dbContext);
        }

        [Test]
        public void TestCreateUser()
        {
            int usersBefore = userContext.ReadAll().Count();
            
            User userCreate = new User("Test First Name", "Test Last Name", 32, "Test Username", "Test Email", "Test Password");

            userContext.Create(userCreate);

            int usersAfter = userContext.ReadAll().Count();

            Assert.IsTrue(usersBefore != usersAfter);
        }

        [Test]
        public void TestReadUser()
        {

            userContext.Create(new User("Test First Name", "Test Last Name", 32, "Test Username", "Test Email", "Test Password"));

            User user = userContext.Read(1);

            Assert.That(user != null, "There is no record with id 1");
        }

        [Test]
        public void TestUpdateUser()
        {
            userContext.Create(new User("Test First Name", "Test Last Name", 32, "Test Username", "Test Email", "Test Password"));

            User user = userContext.Read(1);

            user.FirstName = "Updated First Name";

            userContext.Update(user);

            User updatedUser = userContext.Read(1);

            Assert.IsTrue(updatedUser.FirstName == "Updated First Name", "The first name is not updated");
        }

        [Test]

        public void TestDeleteUser()
        {
            userContext.Create(new User("Test First Name", "Test Last Name", 32, "Test Username", "Test Email", "Test Password"));

            int usersBefore = userContext.ReadAll().Count();

            userContext.Delete(1);

            int usersAfter = userContext.ReadAll().Count();

            Assert.IsTrue(usersBefore != usersAfter);
        }
    }
}
