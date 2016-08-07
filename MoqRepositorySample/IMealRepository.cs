using System.Collections.Generic;

namespace MoqProjectRepository
{
    public interface IMealRepository
    {
        IList<Meal> FindAll();

        Meal FindByName(string mealName);

        Meal FindById(int mealId);

        bool Save(Meal target);
    }
}
