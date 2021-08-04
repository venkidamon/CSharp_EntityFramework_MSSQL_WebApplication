using CarPool.Models;
using CarPool.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolTest.Repository
{
    [TestFixture]
    public class UserRepositoryTests
    {
        Mock<RideDbContext> userContextMock;
        Mock<DbSet<User>> usersDbSet;
        UserRepository userRepository;
        public IUserRepository repo;
        [OneTimeSetUp]
        public void Setup()
        {
            var data = new List<User>
            {
             new User {Email="person1@cts.in",Password = "pwd123" },
             new User {Email="person2@cts.in",Password = "pwd123" },
            }
                  .AsQueryable();
            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<RideDbContext>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            repo = new UserRepository(mockContext.Object);
            usersDbSet = new Mock<DbSet<User>>();

            userContextMock = new Mock<RideDbContext>();
        }
        [Test]
        public void CreateUser_ExpectedBehavior_Success()
        {
            usersDbSet.Setup(x => x.Add(It.IsAny<User>())).Returns((User u) => u);
            userContextMock.Setup(x => x.SaveChanges()).Returns(1);
            userContextMock.Setup(x => x.Users).Returns(usersDbSet.Object);
            userRepository = new UserRepository(userContextMock.Object);
            User user = new User { Email = "person1@cts.in", Password = "pwd123" };
            var actual = userRepository.CreateUser(user);
            Assert.AreEqual(1, actual);
        }
        [Test]
        public void ValidateUser_ExpectedBehaviour_Success()
        {
            var result = repo.Validate(new User { Email = "person1@cts.in", Password = "pwd123" });
            Assert.IsTrue(result);
        }
        [Test]
        public void ValidateUser_ExpectedBehaviour_Failure()
        {
            var result = repo.Validate(new User { Email = "person1@cts.com", Password = "pwd123" });
            Assert.IsFalse(result);
        }
    }
}
