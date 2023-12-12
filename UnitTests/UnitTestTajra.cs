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
using System.IO;
using System.Xml.Linq;



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
                new Drink { id = 1, name = "Watermelon Wave", price = 3.99 },
                new Drink { id = 2, name = "Minty Glacier", price = 5.99 },
                new Drink { id = 3, name = "Rosy Chill", price = 7.99 }
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
                new Drink(1, "Watermelon Wave", 7.99),
                new Drink(2, "Blue Lemonade", 5.99),
                new Drink(3, "Strawberry Mojito", 11.99)
            };

            // Mock DbSet using the utility method
            var mockSet = MockDbSet(drinks);
            mockDbContext.Setup(c => c.Drinks).Returns(mockSet);
            controller = new AdminPanelController(mockDbContext.Object);
            var result = controller.cheapestDrink(drinks);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(5.99, result.price);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheapestDrink_DrinkWithNegativePrice_ThrowsArgumentException()
        {
            var drinks = new List<Drink> {
                new Drink(1, "Berrylicious", 7.99), 
                new Drink(2, "Cherry Bomb", -9.99), 
                new Drink(3, "Watermelon Wave", 11.99)
            };
            controller.cheapestDrink(drinks);
        }


        [TestMethod]
        public void RecommendationBasedOnIngredients_ReturnsExpectedDrinks()
        {
            var drinks = new List<Drink> {
                new Drink(1, "Berrylicious", 7.99),
                new Drink(2, "Cherry Bomb", 9.99),
                new Drink(3, "Watermelon Wave", 11.99),
                new Drink(4, "Blue Lemonade", 5.99),
                new Drink(5, "Rosy Chill", 7.99),
                new Drink(6, "Orange Blossom", 4.99),
                new Drink(7, "Kiwi Kiss", 3.99)
            };

            var ingredients = new List<Ingredient>
            {
                new Ingredient(1, "Berry", 39),
                new Ingredient(3, "Kiwi", 75),
                new Ingredient(2, "Watermelon", 99)
            };

            var drinkIngredients = new List<DrinkIngredient>()
            {
                new DrinkIngredient(1, 1, 1),
                new DrinkIngredient(2, 2, 1),
                new DrinkIngredient(3, 3, 2),
                new DrinkIngredient(4, 4, 2),
                new DrinkIngredient(5, 5, 1),
                new DrinkIngredient(6, 6, 2),
                new DrinkIngredient(7, 7, 3)
            };

            // Mock DbSet koristeći pomoćnu metodu
            var mockIngredients = MockDbSet(ingredients);
            var mockDrinkIngredients = MockDbSet(drinkIngredients);
            var mockDrinks = MockDbSet(drinks);

            mockDbContext.Setup(c => c.Ingredients).Returns(mockIngredients);
            mockDbContext.Setup(c => c.DrinkIngredients).Returns(mockDrinkIngredients);
            mockDbContext.Setup(c => c.Drinks).Returns(mockDrinks);

            // Set up the controller with the mocked context
            controller = new AdminPanelController(mockDbContext.Object);

            var result = controller.recommendationBasedOnIngredients(ingredients);

            // Right order of drink ids: 3, 4, 6, 7, 1
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result[0].id);
            Assert.AreEqual(4, result[1].id);
            Assert.AreEqual(6, result[2].id);
            Assert.AreEqual(7, result[3].id);
            Assert.AreEqual(1, result[4].id);
        }




        [TestMethod]
        public void RecommendationBasedOnIngredients_RightNumberOfDrinksInList()
        {
            var drinks = new List<Drink> {
                new Drink(1, "Berrylicious", 7.99),
                new Drink(2, "Cherry Bomb", 9.99),
                new Drink(3, "Watermelon Wave", 11.99),
                new Drink(4, "Blue Lemonade", 5.99),
                new Drink(5, "Rosy Chill", 7.99),
                new Drink(6, "Orange Blossom", 4.99),
                new Drink(7, "Kiwi Kiss", 3.99)
            };

            var ingredients = new List<Ingredient>
            {
                new Ingredient(1, "Berry", 39),
                new Ingredient(3, "Kiwi", 75),
                new Ingredient(2, "Watermelon", 99)
            };

            var drinkIngredients = new List<DrinkIngredient>()
            {
                new DrinkIngredient(1, 1, 1),
                new DrinkIngredient(2, 2, 1),
                new DrinkIngredient(3, 3, 2),
                new DrinkIngredient(4, 4, 2),
                new DrinkIngredient(5, 5, 1),
                new DrinkIngredient(6, 6, 2),
                new DrinkIngredient(7, 7, 3)
            };

            // Mock DbSet koristeći pomoćnu metodu
            var mockIngredients = MockDbSet(ingredients);
            var mockDrinkIngredients = MockDbSet(drinkIngredients);
            var mockDrinks = MockDbSet(drinks);

            mockDbContext.Setup(c => c.Ingredients).Returns(mockIngredients);
            mockDbContext.Setup(c => c.DrinkIngredients).Returns(mockDrinkIngredients);
            mockDbContext.Setup(c => c.Drinks).Returns(mockDrinks);

            // Set up the controller with the mocked context
            controller = new AdminPanelController(mockDbContext.Object);

            var result = controller.recommendationBasedOnIngredients(ingredients);
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count);
        }


        public static IEnumerable<object[]> CheapestDrinkXmlData()
        {
            var xmlTestData = @"
            <TestData>
                <TestEntry>
                    <DrinkList>
                        <Drink id='1' name='Watermelon Wave' price='3.99' />
                        <Drink id='2' name='Minty Glacier' price='5.99' />
                        <Drink id='3' name='Rosy Chill' price='7.99' />
                    </DrinkList>
                    <ExpectedDrink>
                        <Id>1</Id>
                        <Name>Watermelon Wave</Name>
                        <Price>3.99</Price>
                    </ExpectedDrink>
                </TestEntry>
            </TestData>";

            var xmlDoc = XDocument.Parse(xmlTestData);
            var testEntries = xmlDoc.Descendants("TestEntry");

            foreach (var entry in testEntries)
            {
                var expectedDrinkElement = entry.Element("ExpectedDrink");
                var expectedId = int.Parse(expectedDrinkElement.Element("Id").Value);
                var expectedName = expectedDrinkElement.Element("Name").Value;
                var expectedPrice = double.Parse(expectedDrinkElement.Element("Price").Value);

                var drinksElement = entry.Element("DrinkList");
                var drinks = new List<Drink>();
                foreach (var drinkElement in drinksElement.Elements("Drink"))
                {
                    var id = int.Parse(drinkElement.Attribute("id").Value);
                    var name = drinkElement.Attribute("name").Value;
                    var price = double.Parse(drinkElement.Attribute("price").Value);
                    drinks.Add(new Drink { id = id, name = name, price = price });
                }

                yield return new object[] { expectedId, expectedName, expectedPrice, drinks };
            }
        }

        [TestMethod]
        [DynamicData(nameof(CheapestDrinkXmlData), DynamicDataSourceType.Method)]
        public void CheapestDrink_ReturnsCheapestDrinkUsingXMLData(int expectedId, string expectedName, double expectedPrice, List<Drink> drinks)
        {
            // Act
            var result = controller.cheapestDrink(drinks);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedId, result.id);
            Assert.AreEqual(expectedName, result.name);
            Assert.AreEqual(expectedPrice, result.price);
        }

        public static List<object[]> GetCheapesDrinkCsvTestData()
        {
            var csvTestData = @"1,Watermelon Wave,3.99,2,Minty Glacier,5.99,3,Rosy Chill,7.99,";

            var lines = csvTestData.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var testDataList = new List<object[]>();

            foreach (var line in lines)
            {
                try
                {
                    var values = line.Split(',');
                    int drink1Id = int.Parse(values[0]);
                    string drink1Name = values[1];
                    double drink1Price = double.Parse(values[2]);
                    int drink2Id = int.Parse(values[3]);
                    string drink2Name = values[4];
                    double drink2Price = double.Parse(values[5]);
                    int drink3Id = int.Parse(values[6]);
                    string drink3Name = values[7];
                    double drink3Price = double.Parse(values[8]);

                    testDataList.Add(new object[] {
                        drink1Id, drink1Name, drink1Price,
                        drink2Id, drink2Name, drink2Price,
                        drink3Id, drink3Name, drink3Price
                    });

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing line: {line}. Error details: {ex.Message}");
                    throw;
                }
            }

            return testDataList;
        }

        /////////////////
        [TestMethod]
        [DynamicData(nameof(GetCheapesDrinkCsvTestData), DynamicDataSourceType.Method)]
        public void CheapesDrink_CsvData_ReturnsCheapestDrink(
            int drink1Id, string drink1Name, double drink1Price,
            int drink2Id, string drink2Name, double drink2Price,
            int drink3Id, string drink3Name, double drink3Price)
        {
            var mockDrinks = new List<Drink>
            {
                new Drink { id = drink1Id, name = drink1Name, price = drink1Price },
                new Drink { id = drink2Id, name = drink2Name, price = drink2Price },
                new Drink { id = drink3Id, name = drink3Name, price = drink3Price }
            };

            mockDbContext.Setup(c => c.Drinks).Returns(MockDbSet(mockDrinks));

            // injecting the mockDbContext into the controller
            var controller = new AdminPanelController(mockDbContext.Object);

            var result = controller.cheapestDrink(mockDrinks);

            Assert.AreEqual(1, result.id);
        }

        [TestMethod]
        public void AreAllDrinksAlcoholic_AllDrinksDoNotContainAlcoholicIngredients_ReturnsFalse()
        {
            // Arrange
            var drinkIngredients = new List<DrinkIngredient>
            {
                new DrinkIngredient(1, 1, 21),
                new DrinkIngredient(2, 1, 23),
                new DrinkIngredient(3, 1, 29),
            };

            var mockDbContext = new Mock<IApplicationDbContext>();
            mockDbContext.Setup(c => c.DrinkIngredients).Returns(MockDbSet(drinkIngredients));

            controller = new AdminPanelController(mockDbContext.Object);

            var drinks = new List<Drink>
            {
                new Drink { id = 1, name = "AlcoholicDrink1", price = 8.99 },
                new Drink { id = 2, name = "AlcoholicDrink2", price = 7.99 },
            };

            // Act
            var result = controller.AreAllDrinksAlcoholic(drinks);

            // Assert
            Assert.IsFalse(result);
        }


    }
}