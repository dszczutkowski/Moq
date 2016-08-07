using System.Collections.Generic;

namespace MoqProjectRepository
{
    public interface IOrderRepository
    {
        IList<Order> FindAll();
        
        Order FindById(int orderId);

        bool Save(Order target);
    }
}
