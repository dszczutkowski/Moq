using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using MoqProjectRepository;

namespace TestMoqProject
{
    [TestClass]
    public class OrderTest
    {
        public OrderTest()
        {

            IList<Order> orders = new List<Order>
            {
                new Order { OrderId = 1 },
                new Order { OrderId = 2 },
                new Order { OrderId = 3 }
            };

            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();

            mockOrderRepository.Setup(mr => mr.FindAll()).Returns(orders);

            mockOrderRepository.Setup(mr => mr.FindById(It.IsAny<int>())).Returns((int i) => orders.Where(x => x.OrderId == i).Single());
            
            mockOrderRepository.Setup(mr => mr.Save(It.IsAny<Order>())).Returns(
            (Order target) =>
            {
                DateTime now = DateTime.Now;

                if (target.OrderId.Equals(default(int)))
                {
                    target.DateCreated = now;
                    target.DateModified = now;
                    target.OrderId = orders.Count() + 1;
                    orders.Add(target);
                }
                else
                {
                    var original = orders.Where(q => q.OrderId == target.OrderId).Single();

                    if (original == null)
                    {
                        return false;
                    }

                    original.DateModified = now;
                }

                return true;
            });

            this.MockOrderRepository = mockOrderRepository.Object;
        }
        
        public TestContext TestContext { get; set; }
        
        public readonly IOrderRepository MockOrderRepository;
        
        [TestMethod]
        public void TestReturnOrderById()
        {

            Order testOrder = this.MockOrderRepository.FindById(2);

            Assert.IsNotNull(testOrder);
            Assert.IsInstanceOfType(testOrder, typeof(Order));
            Assert.AreEqual(2, testOrder.OrderId);
        }
        
        [TestMethod]
        public void TestReturnAllorders()
        {

            IList<Order> testOrder = this.MockOrderRepository.FindAll();

            Assert.IsNotNull(testOrder);
            Assert.AreEqual(3, testOrder.Count);
        }
    }
}
