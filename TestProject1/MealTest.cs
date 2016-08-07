Musing System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using MoqProjectRepository;

namespace TestMoqProject
{
    [TestClass]
    public class MealTest
    {
        public MealTest()
        {

            IList<Meal> meals = new List<Meal>
            {
                new Meal { MealId = 1, Name = "Salmon", Description = "Tasty Salmon", Price = 16.99 },
                new Meal { MealId = 2, Name = "Roast Beef", Description = "Tasty Beef", Price = 16.99 },
                new Meal { MealId = 3, Name = "Tuna Salad", Description = "Tasty Salad", Price = 9.99 }
            };


            Mock<IMealRepository> mockMealRepository = new Mock<IMealRepository>();

            mockMealRepository.Setup(mr => mr.FindAll()).Returns(meals);

            mockMealRepository.Setup(mr => mr.FindById(It.IsAny<int>())).Returns((int i) => meals.Where(x => x.MealId == i).Single());

            mockMealRepository.Setup(mr => mr.FindByName(It.IsAny<string>())).Returns((string s) => meals.Where(x => x.Name == s).Single());

            mockMealRepository.Setup(mr => mr.Save(It.IsAny<Meal>())).Returns(
            (Meal target) =>
            {
                DateTime now = DateTime.Now;

                if (target.MealId.Equals(default(int)))
                {
                    target.DateCreated = now;
                    target.DateModified = now;
                    target.MealId = meals.Count() + 1;
                    meals.Add(target);
                }
                else
                {
                    var original = meals.Where(q => q.MealId == target.MealId).Single();

                    if (original == null)
                    {
                        return false;
                    }

                    original.Name = target.Name;
                    original.Price = target.Price;
                    original.Description = target.Description;
                    original.DateModified = now;
                }

                return true;
            });

            this.MockMealRepository = mockMealRepository.Object;
        }
        
        public TestContext TestContext { get; set; }
        
        public readonly IMealRepository MockMealRepository;
        
        [TestMethod]
        public void TestReturnMealById()
        {

            Meal testMeal = this.MockMealRepository.FindById(2);

            Assert.IsNotNull(testMeal);
            Assert.IsInstanceOfType(testMeal, typeof(Meal));
            Assert.AreEqual("Roast Beef", testMeal.Name);
        }
        
        [TestMethod]
        public void TestReturnMealByName()
        {

            Meal testMeal = this.MockMealRepository.FindByName("Tuna Salad");

            Assert.IsNotNull(testMeal);
            Assert.IsInstanceOfType(testMeal, typeof(Meal));
            Assert.AreEqual(3, testMeal.MealId);
        }
        
        [TestMethod]
        public void TestReturnAllMeals()
        {
            IList<Meal> testMeal = this.MockMealRepository.FindAll();

            Assert.IsNotNull(testMeal);
            Assert.AreEqual(3, testMeal.Count);
        }
        
        [TestMethod]
        public void TestInsertMeal()
        {
            Meal newMeal = new Meal
            { Name = "Pork", Description = "Pork Chop", Price = 12.99 };

            int productCount = this.MockMealRepository.FindAll().Count;
            Assert.AreEqual(3, productCount);
            
            this.MockMealRepository.Save(newMeal);
            
            productCount = this.MockMealRepository.FindAll().Count;
            Assert.AreEqual(4, productCount);
            
            Meal testMeal = this.MockMealRepository.FindByName("Pork");
            Assert.IsNotNull(testMeal);
            Assert.IsInstanceOfType(testMeal, typeof(Meal));
            Assert.AreEqual(4, testMeal.MealId);
        }
        
        [TestMethod]
        public void TestUpdateMeal()
        {
            Meal testMeal = this.MockMealRepository.FindById(1);
            
            testMeal.Name = "Grilled Salmon";
            
            this.MockMealRepository.Save(testMeal);
            
            Assert.AreEqual("Grilled Salmon", this.MockMealRepository.FindById(1).Name);
        }
    }
}
