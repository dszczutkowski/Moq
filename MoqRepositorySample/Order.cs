using System;
using System.Collections.Generic;

namespace MoqProjectRepository
{
    public class Order
    {
        public int OrderId { get; set; }

        public virtual List<Meal> Meals { get; set; }
        
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
