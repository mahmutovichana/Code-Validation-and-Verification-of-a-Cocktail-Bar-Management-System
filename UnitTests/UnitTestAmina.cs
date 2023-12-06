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


namespace UnitTestAmina
{
    [TestClass]
    public class UnitTestAmina
    {
        private AdminPanelController controller;
        private Mock<IApplicationDbContext>  mockDbContext;
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
        public void BubbleSort_AlreadyOrdered_ReturnsSameList()
        {
            var drinks= new List<Drink> {
            new Drink(1, "Kiwi Kiss", 2.99), new Drink(2, "Orange Blossom", 3.99), new Drink(3, "Cucumber Cooler", 4.99),
            new Drink(4, "Grapefruit Spritz", 5.99)
            };
            var drinksOrdered = controller.BubbleSort(drinks);
            CollectionAssert.AreEquivalent(drinks, drinksOrdered);
        }

        [TestMethod]
        public void BubbleSort_InverseOrder_ReturnsOrderedList()
        {
            var drinks = new List<Drink> {
            new Drink(1, "Grapefruit Spritz", 5.99), new Drink(2, "Cucumber Cooler", 4.99), new Drink(3, "Orange Blossom", 3.99),
            new Drink(4, "Kiwi Kiss", 2.99)
            };
            var drinksOrdered = controller.BubbleSort(drinks);
            Assert.AreEqual("Kiwi Kiss", drinksOrdered[0].name);
            Assert.AreEqual("Orange Blossom", drinksOrdered[1].name);
            Assert.AreEqual("Cucumber Cooler", drinksOrdered[2].name);
            Assert.AreEqual("Grapefruit Spritz", drinksOrdered[3].name);
        }

        [TestMethod]
        public void BubbleSort_RandomOrder_ReturnsOrderedList()
        {
            var drinks = new List<Drink> {
            new Drink(1, "Grapefruit Spritz", 4.99), new Drink(2, "Cucumber Cooler", 2.99), new Drink(3, "Orange Blossom", 2.99),
            new Drink(4, "Kiwi Kiss", 6.99)
            };
            var drinksOrdered = controller.BubbleSort(drinks);
            /*
            var drinksExpected = new List<Drink> {
            new Drink(2, "Cucumber Cooler", 2.99),
            new Drink(3, "Orange Blossom", 2.99),
            new Drink(1, "Grapefruit Spritz", 4.99),
            new Drink(4, "Kiwi Kiss", 6.99)
            };*/
            //CollectionAssert.AreEquivalent(drinksExpected, drinksOrdered);
            Assert.AreEqual("Cucumber Cooler", drinksOrdered[0].name);
            Assert.AreEqual("Orange Blossom", drinksOrdered[1].name);
            Assert.AreEqual("Grapefruit Spritz", drinksOrdered[2].name);
            Assert.AreEqual("Kiwi Kiss", drinksOrdered[3].name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BubbleSort_DrinkWithNegativePrice_ThrowsArgumentException()
        {
            var drinks = new List<Drink> {
            new Drink(1, "Grapefruit Spritz", 4.99), new Drink(2, "Cucumber Cooler", -2.99), new Drink(3, "Orange Blossom", 2.99),
            new Drink(4, "Kiwi Kiss", 6.99)
            };
            controller.BubbleSort(drinks);
        }

        [TestMethod]
        public void MostExpensiveDrink_RandomOrder_ReturnsMostExpensiveDrink()
        {
           
            var drinks = new List<Drink> {
            new Drink(1, "Grapefruit Spritz", 4.99), new Drink(2, "Cucumber Cooler", 2.99), new Drink(3, "Orange Blossom", 2.99),
            new Drink(4, "Kiwi Kiss", 6.99)
            };
            var mostExpensiveDrink = controller.MostExpensiveDrink(drinks);
          
            Assert.AreEqual("Kiwi Kiss", mostExpensiveDrink.name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MostExpensiveDrink_DrinkWithNegativePrice_ThrowsArgumentException()
        {
            var drinks = new List<Drink> {
            new Drink(1, "Grapefruit Spritz", 4.99), new Drink(2, "Cucumber Cooler", -2.99), new Drink(3, "Orange Blossom", 2.99),
            new Drink(4, "Kiwi Kiss", 6.99)
            };
            controller.MostExpensiveDrink(drinks);
        }


        [TestMethod]
        public void PriceWithPDV_RandomOrder_ReturnsPricesWithPDV()
        {
            // setting up mock behavior for Drinks property
            var drinksMocked = new List<Drink>{ new Drink(1, "Grapefruit Spritz", 4.99), new Drink(2, "Cucumber Cooler", 2.99), 
            new Drink(3, "Orange Blossom", 2.99), new Drink(4, "Kiwi Kiss", 6.99)
            };
            mockDbContext.Setup(m => m.Drinks).Returns(MockDbSet(drinksMocked));

            // injecting the mockDbContext into the controller
            var controller = new AdminPanelController(mockDbContext.Object);
            var pricesWithPDV = controller.PriceWithPDV();

            Assert.AreEqual(Math.Round(2.99 + 0.17 * 2.99, 2), pricesWithPDV[0]);
            Assert.AreEqual(Math.Round(2.99 + 0.17 * 2.99, 2), pricesWithPDV[1]);
            Assert.AreEqual(Math.Round(4.99 + 0.17 * 4.99, 2), pricesWithPDV[2]);
            Assert.AreEqual(Math.Round(6.99 + 0.17 * 6.99, 2), pricesWithPDV[3]);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PriceWithPDV_DrinkWithNegativePrice__ThrowsArgumentException()
        {
            // setting up mock behavior for Drinks property
            var drinksMocked = new List<Drink>{ new Drink(1, "Grapefruit Spritz", 4.99), new Drink(2, "Cucumber Cooler", -2.99),
            new Drink(3, "Orange Blossom", 2.99), new Drink(4, "Kiwi Kiss", 6.99)
            };
            mockDbContext.Setup(m => m.Drinks).Returns(MockDbSet(drinksMocked));

            // injecting the mockDbContext into the controller
            var controller = new AdminPanelController(mockDbContext.Object);
            var pricesWithPDV = controller.PriceWithPDV();
        }

        


    }
}