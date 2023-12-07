using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartCafe.Controllers;
using SmartCafe.Data;
using SmartCafe.Models;
using VVSProject.Interfaces;
using Moq;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static SmartCafe.Controllers.AdminPanelController;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Web.Helpers;

namespace UnitTestHana
{
    [TestClass]
    public class UnitTestHana
    {
        private AdminPanelController controller;
        private Mock<IApplicationDbContext> mockDbContext;
        private ApplicationDbContext mockContext;

        // method for creating a mock DbSet
        private static DbSet<T> MockDbSet<T>(List<T> data) where T : class
        {
            var queryable = data.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            return mockSet.Object;
        }

        [TestInitialize]
        public void SetUp()
        {
            controller = new AdminPanelController(null);
            mockDbContext = new Mock<IApplicationDbContext>();
        }

        [TestMethod]
        public void SearchDrinksByName_ReturnsCorrectResults()
        {
            // Arrange
            var searchTerm = "Kiwi";
            // setting up mock behavior for Drinks property
            var drinksMocked = new List<Drink>
            { 
                new Drink(1, "Grapefruit Spritz", 4.99), 
                new Drink(2, "Cucumber Cooler", 2.99),
                new Drink(3, "Orange Blossom", 2.99), 
                new Drink(4, "Kiwi Kiss", 6.99)
            };
            mockDbContext.Setup(m => m.Drinks).Returns(MockDbSet(drinksMocked));

            // injecting the mockDbContext into the controller
            var controller = new AdminPanelController(mockDbContext.Object);

            // Act
            var result = controller.searchDrinksByName(searchTerm) as ActionResult<IEnumerable<Drink>>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Value, typeof(List<Drink>));

            var drinks = (List<Drink>)result.Value;
            Assert.IsTrue(drinks.Count > 0);
        }

        [TestMethod]
        public void SearchDrinksByIngredient_ReturnsCorrectResults()
        {
            // Arrange
            var searchTerm = "Vodka";
            var drinksMocked = new List<Drink>
            {
                new Drink { id = 1, name = "Grapefruit Spritz", price = 4.99 },
                new Drink { id = 2, name = "Cucumber Cooler", price = 2.99 },
                new Drink { id = 3, name = "Orange Blossom", price = 2.99 },
                new Drink { id = 4, name = "Kiwi Kiss", price = 6.99 }
            };

            var ingredientsMocked = new List<Ingredient>
            {
                new Ingredient { id = 1, name = "Vodka", quantity = 100 },
                new Ingredient { id = 2, name = "Lime", quantity = 5 },
                new Ingredient { id = 3, name = "Soda", quantity = 200 }
            };

            var drinkIngredientsMocked = new List<DrinkIngredient>
            {
                new DrinkIngredient { id = 1, idDrink = 1, idIngredient = 1, Ingredient = ingredientsMocked[0], Drink = drinksMocked[0] },
                new DrinkIngredient { id = 2, idDrink = 2, idIngredient = 1, Ingredient = ingredientsMocked[0], Drink = drinksMocked[1] },
                new DrinkIngredient { id = 3, idDrink = 3, idIngredient = 2, Ingredient = ingredientsMocked[1], Drink = drinksMocked[2] },
                new DrinkIngredient { id = 4, idDrink = 4, idIngredient = 3, Ingredient = ingredientsMocked[2], Drink = drinksMocked[3] }
            };

            // Konfiguracija MockDbContext-a
            mockDbContext.Setup(m => m.Drinks).Returns(MockDbSet(drinksMocked));
            mockDbContext.Setup(m => m.Ingredients).Returns(MockDbSet(ingredientsMocked));
            mockDbContext.Setup(m => m.DrinkIngredients).Returns(MockDbSet(drinkIngredientsMocked));

            // injecting the mockDbContext into the controller
            var controller = new AdminPanelController(mockDbContext.Object);

            // Act
            var result = controller.searchDrinksByIngredient(searchTerm) as ActionResult<IEnumerable<Drink>>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Value, typeof(List<Drink>));

            var drinks = (List<Drink>)result.Value;
            Assert.IsTrue(drinks.Count > 0);

        }

        [TestMethod]
        public void CalculateDailyProfit_ReturnsCorrectResult()
        {
            // Arrange
            var selectedDrinks = new List<DrinkQuantityPair>
            {
                new DrinkQuantityPair { DrinkId = 1, Quantity = 2 },
                new DrinkQuantityPair { DrinkId = 2, Quantity = 3 },
                new DrinkQuantityPair { DrinkId = 3, Quantity = 1 }
            };

            var mockDrinks = new List<Drink>
            {
                new Drink { id = 1, name = "Drink1", price = 10 },
                new Drink { id = 2, name = "Drink2", price = 15 },
                new Drink { id = 3, name = "Drink3", price = 8 }
            };

            mockDbContext.Setup(c => c.Drinks).Returns(MockDbSet(mockDrinks));

            // injecting the mockDbContext into the controller
            var controller = new AdminPanelController(mockDbContext.Object);

            // Act
            var result = controller.CalculateDailyProfit(selectedDrinks);

            // Assert
            Assert.IsInstanceOfType(result, typeof(Tuple<double, string>));
            Assert.IsNotNull(result);

            double dailyProfit = result.Item1;
            string message = result.Item2;

            Assert.AreEqual(73.0, dailyProfit, 0.01);
            Assert.AreEqual("Your profit is above average", message);
        }


        [TestMethod]
        public void CalculateDailyProfit_DrinkNotFound_ReturnsJsonResultWithZeroProfit()
        {
            // Arrange
            var selectedDrinks = new List<DrinkQuantityPair>
            {
                new DrinkQuantityPair { DrinkId = 1, Quantity = 2 }
            };

            var mockDrinks = new List<Drink>
            {
                new Drink { id = 150, name = "Drink1", price = 10 }
            };

            mockDbContext.Setup(c => c.Drinks).Returns(MockDbSet(mockDrinks));

            // injecting the mockDbContext into the controller
            var controller = new AdminPanelController(mockDbContext.Object);

            // Act
            var result = controller.CalculateDailyProfit(selectedDrinks);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0.0, result.Item1);
        }


        [TestMethod]
        public void CalculateDailyProfit_NegativeQuantity_ReturnsJsonResultWithZeroProfit()
        {
            // Arrange
            var selectedDrinks = new List<DrinkQuantityPair>
            {
                new DrinkQuantityPair { DrinkId = 1, Quantity = -2 }
            };

            var mockDrinks = new List<Drink>
            {
                new Drink { id = 150, name = "Drink1", price = 10 }
            };

            mockDbContext.Setup(c => c.Drinks).Returns(MockDbSet(mockDrinks));

            // injecting the mockDbContext into the controller
            var controller = new AdminPanelController(mockDbContext.Object);

            // Act
            var result = controller.CalculateDailyProfit(selectedDrinks);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0.0, result.Item1);
        }
    }
}