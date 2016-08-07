using System;
using System.Collections.Generic;

namespace MoqProjectRepository
{
    class OrderRepository : IOrderRepository
    {
        public IList<Order> FindAll()
        {
            throw new NotImplementedException();
        }

        public Order FindById(int orderId)
        {
            throw new NotImplementedException();
        }

        public bool Save(Order target)
        {
            throw new NotImplementedException();
        }
    }
}
