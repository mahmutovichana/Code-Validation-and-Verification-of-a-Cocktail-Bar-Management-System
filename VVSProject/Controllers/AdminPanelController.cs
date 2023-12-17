using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmartCafe.Data;
using SmartCafe.Models;
using VVSProject.Interfaces;

namespace SmartCafe.Controllers
{

    public class AdminPanelController : Controller
    {
        private readonly IApplicationDbContext _context;

        public AdminPanelController(IApplicationDbContext context)
        {
            _context = context;
        }

        //sort method (newly added)

        public List<Drink> BubbleSort(List<Drink> drinks)
        {
            foreach (Drink drink in drinks)
            {
                if (drink.price <= 0) throw new ArgumentException("Price of a drink must be greater than zero!");
            }
            // Bubble sort 
            int n = drinks.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (drinks[j].price > drinks[j + 1].price)
                    {
                        var temp = drinks[j];
                        drinks[j] = drinks[j + 1];
                        drinks[j + 1] = temp;
                    }
                }
            }

            return drinks;

        }
        public List<Ingredient> insertionSort(List<Ingredient> ingredients)
        {
            int n = ingredients.Count, j;
            for (int i = 1; i < n; i++)
            {
                Ingredient key = ingredients[i];
                j = i - 1;

                while (j >= 0 && ingredients[j].quantity > key.quantity)
                {
                    ingredients[j + 1] = ingredients[j];
                    j = j - 1;
                }
                ingredients[j + 1] = key;
            }

            return ingredients;
        }
        public string optimalProfitMessage(double realProfit)
        {
            var EPSILON = 0.0001;
            var drinks = _context.Drinks.ToList();
            double optimalProfit = 0;

            for (int i = 0; i < drinks.Count; i++)
            {
                optimalProfit += drinks[i].price;
            }
            if (optimalProfit - realProfit > EPSILON) //realProfit < optimalProfit
            {
                return "You are below optimal profit";
            }
            else if (Math.Abs(realProfit - optimalProfit) < EPSILON) //realProfit == optimalProfit
            {
                return "Your profit is optimal";
            }
            return "Your profit is above average";
        }

        // METHOD: recommended drinks based on ingredients
        public List<Drink> recommendationBasedOnIngredients(List<Ingredient> ingredients)
        {
            // needed lists
            List<DrinkIngredient> drinkIngredients = _context.DrinkIngredients.ToList();
            List<Drink> drinks = _context.Drinks.ToList();
            List<Drink> wantedDrinks = new List<Drink>();

            int num = 1;
            while (wantedDrinks.Count != 5)
            {
                Ingredient ingredient = ingredients[ingredients.Count - num];
                Console.WriteLine("sastojak " + ingredient.name);
                List<int> drinkIds = new List<int>();
                Console.WriteLine(drinkIngredients.Count);
                for (int i = 0; i < drinkIngredients.Count; i++)
                {
                    if (drinkIngredients[i].idIngredient == ingredient.id)
                    {
                        Console.WriteLine(" - id drinka: " + drinkIngredients[i].idDrink);
                        drinkIds.Add(drinkIngredients[i].idDrink);
                    }
                }

                for (int i = 0; i < drinkIds.Count; i++)
                {
                    for (int j = 0; j < drinks.Count; j++)
                    {
                        if (drinkIds[i] == drinks[j].id && !wantedDrinks.Contains(drinks[j]))
                        {
                            Console.WriteLine("ulazi: " + drinkIds[i]);
                            wantedDrinks.Add(drinks[j]);
                        }
                    }
                    if (wantedDrinks.Count == 5)
                    {
                        break;
                    }
                }
                num++;
            }
            return wantedDrinks;
        }

        //METHOD: Number of drinks
        public int numberOfDrinks(List<Drink> drinks)
        {
            return drinks.Count;
        }

        //METHOD: Cheapest drink
        public Drink cheapestDrink(List<Drink> drinks)
        {
            for (int i = 0; i < drinks.Count; i++)
            {
                if (drinks[i].price < 0)
                {
                    throw new ArgumentException("Price of a drink must be greater than zero!");
                }
            }
            Drink cheapestDrink = drinks[0];
            for (int i = 1; i < drinks.Count; i++)
            {
                if (drinks[i].price < cheapestDrink.price)
                {
                    cheapestDrink = drinks[i];
                }
            }
            return cheapestDrink;
        }

        //METHOD: Most expensive drink
        public Drink MostExpensiveDrink(List<Drink> drinks)
        {
            foreach (Drink drink in drinks)
            {
                if (drink.price <= 0) throw new ArgumentException("Price of a drink must be greater than zero!");
            }
            Drink expensiveDrink = drinks[0];
            for (int i = 1; i < drinks.Count; i++)
            {
                if (drinks[i].price > expensiveDrink.price)
                {
                    expensiveDrink = drinks[i];
                }
            }
            return expensiveDrink;
        }

        //METHOD: Price with PDV

        public List<Double> PriceWithPDV()
        {
            var PDVPrices = new List<Double>();
            var drinks = BubbleSort(_context.Drinks.ToList());
            foreach (var drink in drinks)
            {
                PDVPrices.Add(Math.Round(drink.price + 0.17 * drink.price, 2));
            }
            return PDVPrices;
        }
        // GET: AdminPanel
        public IActionResult Index()
        {
            var ingredients = _context.Ingredients.ToList();
            insertionSort(ingredients);
            ViewBag.SortedIngredients = ingredients;
            List<DrinkQuantityPair> selectedDrinks = ViewBag.SelectedDrinks;
            // Recommended drinks on view
            List<Drink> wantedDrinks = recommendationBasedOnIngredients(ingredients);
            ViewBag.WantedDrinks = wantedDrinks;
            // Retrieve the list of drinks from the database
            var drinks = _context.Drinks.ToList();
            //sortiranje
            BubbleSort(drinks);

            DateTime dateTime = DateTime.Now;

            drinks = ApplyHappyHourDiscount(drinks, dateTime);


            // number of drinks
            ViewBag.NumberOfDrinks = numberOfDrinks(drinks);
            // cheapest drink
            ViewBag.CheapestDrink = cheapestDrink(drinks);
            // most expensive drink
            ViewBag.MostExpensiveDrink = MostExpensiveDrink(drinks);
            //prices with PDV
            ViewBag.PricesWithPDV = PriceWithPDV();
            //discount for happy hour
            ViewBag.IsHappyHour = CheckHappyHourStatus(dateTime);
            return View(drinks);
        }

        // POST: AdminPanel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,price")] Drink updatedDrink)
        {
            if (id != updatedDrink.id)
            {
                return NotFound();
            }

            var existingDrink = await _context.Drinks.FindAsync(id);

            if (existingDrink == null)
            {
                Console.WriteLine(9);
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (updatedDrink.price >= 2 && updatedDrink.price <= 50)
                {
                    // Update properties manually
                    existingDrink.name = updatedDrink.name;
                    existingDrink.price = updatedDrink.price;
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return RedirectToAction(nameof(Index));

                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Index), new { id = updatedDrink.id });
                }
            }

            return View(updatedDrink);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Drink>> searchDrinksByName(string searchTerm)
        {
            var drinks = _context.Drinks
                .Where(d => d.name.Contains(searchTerm))
                .ToList();
            return drinks;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Drink>> searchDrinksByIngredient(string searchTerm)
        {
            var drinks = _context.DrinkIngredients
                .Where(di => di.Ingredient != null && di.Ingredient.name.Contains(searchTerm))
                .Select(di => di.Drink)
                .Distinct()
                .ToList();
            return drinks;
        }

        public class DrinkQuantityPair
        {
            public int DrinkId { get; set; }
            public int Quantity { get; set; }
        }
        [HttpPost]
        public Tuple<double, string> CalculateDailyProfit([FromBody] List<DrinkQuantityPair> selectedDrinks)
        {
            ViewBag.SelectedDrinks = selectedDrinks;
            Console.WriteLine(selectedDrinks.ToArray());
            var drinkIds = selectedDrinks.Select(dq => dq.DrinkId).ToList();
            Console.WriteLine(drinkIds.Count);
            var drinksFromDb = _context.Drinks.Where(d => drinkIds.Contains(d.id)).ToList();
            Console.WriteLine(drinksFromDb.Count);
            var dailyProfit = 0.0;
            foreach (var drinkQuantityPair in selectedDrinks)
            {
                var drink = drinksFromDb.FirstOrDefault(d => d.id == drinkQuantityPair.DrinkId);
                if (drink != null)
                {
                    dailyProfit += drink.price * drinkQuantityPair.Quantity;
                }
            }

            var message = optimalProfitMessage(dailyProfit);
            Console.WriteLine(Tuple.Create(dailyProfit, message));
            return Tuple.Create(dailyProfit, message);
        }
        //////////////////

        public bool CheckHappyHourStatus(DateTime dateTime)
        {
            var happyHourStartTime = new TimeSpan(20, 0, 0); // 8 PM
            var happyHourEndTime = new TimeSpan(22, 0, 0);   // 10 PM

            return dateTime.TimeOfDay >= happyHourStartTime && dateTime.TimeOfDay <= happyHourEndTime;
        }


        public List<Drink> ApplyHappyHourDiscount(List<Drink> drinks, DateTime dateTime)
        {
            if (CheckHappyHourStatus(dateTime))
            {
                foreach (var drink in drinks)
                {
                    drink.price = Math.Round(drink.price * 0.8, 1); // 20% popusta
                }
            }

            return drinks;
        }

        //////////////   Alcoholic drink   /////////////
        public bool isAlcoholic(Drink drink)
        {
            List<int> alcoholicIngredientId = new List<int> { 21, 27, 29, 33, 42, 54 };

            var drinkIngredients = _context.DrinkIngredients
                .Where(di => di.idDrink == drink.id && alcoholicIngredientId.Contains(di.idIngredient))
                .ToList();

            return drinkIngredients.Any();
        }

        public bool AreAllDrinksAlcoholic(List<Drink> drinks)
        {
            foreach (var drink in drinks)
            {
                if (!isAlcoholic(drink))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
