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


namespace UnitTestTajra
{
    [TestClass]
    public class UnitTestTajra
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
        public void NumberOfDrinks_ReturnsCorrectCount()
        {
            // Arrange
            var drinks = new List<Drink>
            {
                new Drink { id = 1, name = "Coca Cola", price = 3 },
                new Drink { id = 2, name = "Pepsi", price = 5 },
                new Drink { id = 3, name = "Sprite", price = 7 }
            };

            // Act
            var result = controller.numberOfDrinks(drinks);

            // Assert
            Assert.AreEqual(drinks.Count, result);
        }


        [TestMethod]
        public void CheapestDrink_ReturnsCheapestDrink()
        {
            // Arrange
            var drinks = new List<Drink>
            {
                new Drink { id = 1, name = "Coca Cola", price = 2.5 },
                new Drink { id = 2, name = "Pepsi", price = 3.0 },
                new Drink { id = 3, name = "Sprite", price = 2.0 }
            };

            // Mock DbSet using the utility method
            var mockSet = MockDbSet(drinks);

            // Mock ApplicationDbContext
            mockDbContext.Setup(c => c.Drinks).Returns(mockSet);

            // Set up the controller with the mocked context
            controller = new AdminPanelController(mockDbContext.Object);

            // Act
            var result = controller.cheapestDrink(drinks);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2.0, result.price);
        }


    }
}