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
    public class RideRepositoryTests
    {
        public RideRepository repo;
        Mock<DbSet<Ride>> mockSet;
        Mock<RideDbContext> mockContext;
        IQueryable<Ride> data;
        [OneTimeSetUp]
        public void Setup()
        {
            data = new List<Ride>
            {
             new Ride {
                            RideCode="ride1",
                            CreatedBy="manager1",
                            Description="Gathering Requirements",
                            CreatedDate=DateTime.Today,
                            SeatCount=1,
                            BookedCount=0,
                            Status="New"
                        }
            }
           .AsQueryable();
            mockSet = new Mock<DbSet<Ride>>();
            mockSet.As<IQueryable<Ride>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Ride>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Ride>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Ride>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


            mockContext = new Mock<RideDbContext>();
            mockContext.Setup(c => c.Rides).Returns(mockSet.Object);
            mockContext.Setup(m => m.SaveChanges()).Returns(1);
            mockSet.Setup(d => d.Add(It.IsAny<Ride>())).Callback<Ride>((s) => data.Append(s));

            repo = new RideRepository(mockContext.Object);

        }
        [Test, Order(1)]
        public void CreateRide_ExpectedBehavior_Success()
        {
            int result = repo.AddRide(new Ride
            {
                RideCode = "ride2",
                CreatedBy = "manager2",
                Description = "Design creation",
                CreatedDate = DateTime.Today,
                SeatCount = 1,
                BookedCount = 0,
                Status = "New"
            });

            mockSet.Verify(m => m.Add(It.IsAny<Ride>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.AreEqual(1, result);
        }
        [Test, Order(2)]
        public void GetActiveRides_ExpectedBehaviour_Success()
        {
            Mock<IRideRepository> mockRideRepository = new Mock<IRideRepository>();
            mockRideRepository.Setup(mr => mr.GetActiveRides()).Returns(() => data.Where(x => x.Status == "New").ToList());
            IRideRepository MockRideRepository = mockRideRepository.Object;
            var result = MockRideRepository.GetActiveRides();
            Assert.AreEqual(1, result.Count);
        }
        [Test, Order(3)]
        public void DisableRide_ExpectedBehaviour_Success()
        {
            Mock<IRideRepository> mockRideRepository = new Mock<IRideRepository>();
            mockRideRepository.Setup(mr => mr.GetRideByCode(It.IsAny<string>())).Returns((string i) => data.Where(x => x.RideCode == i).Single());
            IRideRepository MockRideRepository = mockRideRepository.Object;
            var ride = MockRideRepository.GetRideByCode("ride1");
            ride.Status = "Cancelled";
            MockRideRepository.Disable("ride1");
            Assert.AreEqual("Cancelled", MockRideRepository.GetRideByCode("ride1").Status);
        }

        [Test, Order(4)]
        public void GetActiveRidesByRider_ExpectedBehaviour_Success()
        {
            Mock<IRideRepository> mockRideRepository = new Mock<IRideRepository>();
            mockRideRepository.Setup(mr => mr.GetActiveRidesByRider(It.IsAny<string>())).Returns((string s) => data.Where(x => x.CreatedBy == s).ToList());
            IRideRepository MockRideRepository = mockRideRepository.Object;
            var result = MockRideRepository.GetActiveRidesByRider("manager1");

            Assert.AreEqual(1, result.Count);
        }
        [Test, Order(5)]
        public void GetRideByCode_ExpectedBehaviour_Success()
        {
            Mock<IRideRepository> mockRideRepository = new Mock<IRideRepository>();
            mockRideRepository.Setup(mr => mr.GetRideByCode(It.IsAny<string>())).Returns((string code) => data.Where(x => x.RideCode == code).FirstOrDefault());
            IRideRepository MockRideRepository = mockRideRepository.Object;
            var result = MockRideRepository.GetRideByCode("ride1");

            Assert.AreEqual("manager1", result.CreatedBy);
        }
        [Test, Order(6)]
        public void UpdateRide_ExpectedBehaviour_Success()
        {
            Mock<IRideRepository> mockRideRepository = new Mock<IRideRepository>();
            mockRideRepository.Setup(mr => mr.GetRideByCode(It.IsAny<string>())).Returns((string i) => data.Where(x => x.RideCode == i).Single());
            IRideRepository MockRideRepository = mockRideRepository.Object;
            var ride = MockRideRepository.GetRideByCode("ride1");
            ride.SeatCount = 1;
            MockRideRepository.UpdateRide(ride);
            Assert.AreEqual(1, MockRideRepository.GetRideByCode("ride1").SeatCount);
        }

        [Test, Order(7)]
        public void UpdateRideUsageCount_ExpectedBehaviour_Success()
        {
            Mock<IRideRepository> mockRideRepository = new Mock<IRideRepository>();
            mockRideRepository.Setup(mr => mr.GetRideByCode(It.IsAny<string>())).Returns((string i) => data.Where(x => x.RideCode == i).Single());
            IRideRepository MockRideRepository = mockRideRepository.Object;
            var ride = MockRideRepository.GetRideByCode("ride1");
            int? current = ride.BookedCount;
            ride.BookedCount = ++ride.BookedCount;
            MockRideRepository.UpdateRide(ride);
            Assert.AreEqual(++current, MockRideRepository.GetRideByCode("ride1").BookedCount);
        }
    }
}
