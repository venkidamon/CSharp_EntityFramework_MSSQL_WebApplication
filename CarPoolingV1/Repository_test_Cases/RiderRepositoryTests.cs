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
    public class RiderRepositoryTests
    {
        Mock<RideDbContext> riderContextMock;
        Mock<DbSet<Rider>> riderDbSet;
        RiderRepository riderRepository;
        public IRiderRepository repo;
        [OneTimeSetUp]
        public void Setup()
        {
            var data = new List<Rider>
            {
             new Rider { RiderName="Amazon", Email="person1@cts.in",Password = "pwd123" },
             new Rider { RiderName="FlipCart",Email="person2@cts.in",Password = "pwd123" },
            }
                  .AsQueryable();
            var mockSet = new Mock<DbSet<Rider>>();
            mockSet.As<IQueryable<Rider>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Rider>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Rider>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Rider>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<RideDbContext>();
            mockContext.Setup(c => c.Riders).Returns(mockSet.Object);

            repo = new RiderRepository(mockContext.Object);
            riderDbSet = new Mock<DbSet<Rider>>();

            riderContextMock = new Mock<RideDbContext>();
        }
        [Test, Order(1)]
        public void CreateRider_ExpectedBehavior_Success()
        {
            riderDbSet.Setup(x => x.Add(It.IsAny<Rider>())).Returns((Rider u) => u);
            riderContextMock.Setup(x => x.SaveChanges()).Returns(1);
            riderContextMock.Setup(x => x.Riders).Returns(riderDbSet.Object);
            riderRepository = new RiderRepository(riderContextMock.Object);
            Rider rider = new Rider { RiderName = "SnapDeal", Email = "person1@cts.in", Password = "pwd123" };
            var actual = riderRepository.CreateRider(rider);
            Assert.AreEqual(1, actual);
        }
        [Test, Order(2)]
        public void ValidateRider_ExpectedBehaviour_Success()
        {
            var result = repo.Validate(new Rider { RiderName = "Amazon", Email = "person1@cts.in", Password = "pwd123" });
            Assert.AreEqual("Amazon", result);
        }
        [Test, Order(3)]
        public void ValidateUser_ExpectedBehaviour_Failure()
        {
            var result = repo.Validate(new Rider { RiderName = "Amazons", Email = "person1@cts.in", Password = "pwd123" });
            Assert.IsNull(result);
        }
    }
}
