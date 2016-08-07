using System;

namespace MoqProjectRepository
{
    public class Meal
    {
        public int MealId { get; set; }

        public Order Order { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
