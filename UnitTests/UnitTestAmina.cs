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
using System.Xml.Linq;


namespace UnitTestAmina
{
    [TestClass]
    public class UnitTestAmina
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
        public void BubbleSort_AlreadyOrdered_ReturnsSameList()
        {
            var drinks = new List<Drink> {
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


        [TestMethod]
        [DynamicData(nameof(MostExpensiveDrinkXmlData), DynamicDataSourceType.Method)]
        public void MostExpensiveDrink_ReturnsMostExpensiveDrinkUsingXMLData(List<Drink> drinks, int expectedId, string expectedName, double expectedPrice)
        {
            // Mock DbSet koriste?i pomo?nu metodu
            var mockSet = MockDbSet(drinks);
            mockDbContext.Setup(c => c.Drinks).Returns(mockSet);
            controller = new AdminPanelController(mockDbContext.Object);

            // Act
            var result = controller.MostExpensiveDrink(drinks);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedId, result.id);
            Assert.AreEqual(expectedName, result.name);
            Assert.AreEqual(expectedPrice, result.price);
        }

        public static IEnumerable<object[]> MostExpensiveDrinkXmlData()
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
                        <Id>3</Id>
                        <Name>Rosy Chill</Name>
                        <Price>7.99</Price>
                    </ExpectedDrink>
                </TestEntry>
                <!-- Dodajte dodatne TestEntry elemente prema potrebi -->
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

                yield return new object[] { drinks, expectedId, expectedName, expectedPrice };
            }
        }


        /////////////////
        public static List<object[]> GetMostExpensiveDrinkCsvTestData()
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
        [DynamicData(nameof(GetMostExpensiveDrinkCsvTestData), DynamicDataSourceType.Method)]
        public void MostExpensiveDrink_CsvData_ReturnsMostExpensiveDrink(
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

            var result = controller.MostExpensiveDrink(mockDrinks);

            Assert.AreEqual("Rosy Chill", result.name);
        }

        ////////Test Driven Development///////////
        [TestMethod]
        public void CheckHappyHourStatus_StartOfHappyHour_True()
        {
            DateTime dateTime = new DateTime(2023, 12, 31, 20, 0, 0); //31.12.2023. u 20:00
            var result = controller.CheckHappyHourStatus(dateTime);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckHappyHourStatus_MiddleOfHappyHour_True()
        {
            DateTime dateTime = new DateTime(2023, 12, 31, 21, 30, 0); //31.12.2023. u 21:30
            var result = controller.CheckHappyHourStatus(dateTime);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckHappyHourStatus_EndOfHappyHour_True()
        {
            DateTime dateTime = new DateTime(2023, 12, 31, 22, 0, 0); //31.12.2023. u 22:00
            var result = controller.CheckHappyHourStatus(dateTime);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckHappyHourStatus_BeforeHappyHour_False()
        {
            DateTime dateTime = new DateTime(2023, 12, 31, 19, 0, 0); //31.12.2023. u 19:00
            var result = controller.CheckHappyHourStatus(dateTime);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CheckHappyHourStatus_AfterHappyHour_False()
        {
            DateTime dateTime = new DateTime(2023, 12, 31, 23, 0, 0); //31.12.2023. u 19:00
            var result = controller.CheckHappyHourStatus(dateTime);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ApplyHappyHourDiscount_HappyHour_ReturnsDiscountedDrinks()
        {
            DateTime dateTime = new DateTime(2023, 12, 31, 21, 0, 0); //31.12.2023. u 21:00
            var drinks = new List<Drink>{ new Drink(1, "Grapefruit Spritz", 4.99),
            new Drink(2, "Orange Blossom", 2.99), new Drink(3, "Kiwi Kiss", 6.99)};
            var drinksExpected = new List<Drink>{ new Drink(1, "Grapefruit Spritz", Math.Round(4.99 * 0.8, 1)),
            new Drink(2, "Orange Blossom", Math.Round(2.99 * 0.8, 1)), new Drink(3, "Kiwi Kiss", Math.Round(6.99 * 0.8, 1))};
            var result = controller.ApplyHappyHourDiscount(drinks, dateTime);

            Assert.AreEqual(drinksExpected[0].price, result[0].price);
            Assert.AreEqual(drinksExpected[1].price, result[1].price);
            Assert.AreEqual(drinksExpected[2].price, result[2].price);

        }

        [TestMethod]
        public void ApplyHappyHourDiscount_HappyHour_ReturnsFullPriceDrinks()
        {
            DateTime dateTime = new DateTime(2023, 12, 31, 10, 0, 0); //31.12.2023. u 10:00
            var drinks = new List<Drink>{ new Drink(1, "Grapefruit Spritz", 4.99),
            new Drink(2, "Orange Blossom", 2.99), new Drink(3, "Kiwi Kiss", 6.99)};
            var result = controller.ApplyHappyHourDiscount(drinks, dateTime);

            Assert.AreEqual(drinks[0].price, result[0].price);
            Assert.AreEqual(drinks[1].price, result[1].price);
            Assert.AreEqual(drinks[2].price, result[2].price);
        }

        //////////dodatni testovi za pokrivenost////////////////
        [TestMethod]
        public void BubbleSort_EmptyList_ReturnsEmptyList()
        {
            var drinks = new List<Drink> { };
            var drinksOrdered = controller.BubbleSort(drinks);
            CollectionAssert.AreEquivalent(drinks, drinksOrdered);
        }
        ////
        [TestMethod]
        public void BubbleSort_TwoElementsList_ReturnsOrderedList()
        {
            var drinks = new List<Drink> { new Drink(1, "Kiwi Kiss", 2.99), new Drink(2, "Orange Blossom", 3.99) };
            var drinksOrdered = controller.BubbleSort(drinks);
            CollectionAssert.AreEquivalent(drinks, drinksOrdered);
        }

        [TestMethod]
        public void BubbleSort_ThreeElementsList_ReturnsOrderedList()
        {
            var drinks = new List<Drink> { new Drink(1, "Kiwi Kiss", 2.99), new Drink(2, "Orange Blossom", 3.99), 
            new Drink(3, "Cucumber Cooler", 4.99)};
            var drinksOrdered = controller.BubbleSort(drinks);
            CollectionAssert.AreEquivalent(drinks, drinksOrdered);
        }

    }
}