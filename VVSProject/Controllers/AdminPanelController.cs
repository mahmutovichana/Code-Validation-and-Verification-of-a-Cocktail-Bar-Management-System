using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            foreach(Drink drink in drinks)
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
        private List<Ingredient> insertionSort(List<Ingredient> ingredients)
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
        private string optimalProfitMessage(double realProfit)
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
        private List<Drink> recommendationBasedOnIngredients(List<Ingredient> ingredients)
        {
            // needed lists
            List<DrinkIngredient> drinkIngredients = _context.DrinkIngredients.ToList();
            List<Drink> drinks = _context.Drinks.ToList();
            List<Drink> wantedDrinks = new List<Drink>();

            int num = 0;
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
                            if (wantedDrinks.Count == 5)
                            {
                                break;
                            }
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
        private int numberOfDrinks (List<Drink> drinks)
        {
            return drinks.Count;
        }

        //METHOD: Cheapest drink
        private Drink cheapestDrink(List<Drink> drinks)
        {
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
            foreach(var drink in drinks)
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
            // number of drinks
            ViewBag.NumberOfDrinks = numberOfDrinks(drinks);
            // cheapest drink
            ViewBag.CheapestDrink = cheapestDrink(drinks);
            // most expensive drink
            ViewBag.MostExpensiveDrink = MostExpensiveDrink(drinks);
            //prices with PDV
            ViewBag.PricesWithPDV = PriceWithPDV();

            return View(drinks);
        }

        // POST: AdminPanel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,price")] Drink drink)
        {
            if (id != drink.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (drink.price >= 2 && drink.price <= 50)
                    {
                        // Mark the entity as modified
                        _context.Entry(drink).State = EntityState.Modified;

                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Cijena mora biti između 2 i 50.";
                        return RedirectToAction(nameof(Index), new { id = drink.id });
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrinkExists(drink.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(drink);
        }

        private bool DrinkExists(int id)
        {
            return _context.Drinks.Any(e => e.id == id);
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
                .Where(di => di.Ingredient.name.Contains(searchTerm))
                .Select(di => di.Drink)
                .Distinct()
                .ToList();
            List<Drink> filteredDrinks = drinks;
            return filteredDrinks;
        }

        [HttpPost]
        public ActionResult calculateDailyProfit([FromBody] List<DrinkQuantityPair> selectedDrinks)
        {
            ViewBag.SelectedDrinks = selectedDrinks;
            Console.WriteLine(selectedDrinks.ToArray());
            var drinkIds = selectedDrinks.Select(dq => dq.DrinkId).ToList();
            var drinksFromDb = _context.Drinks.Where(d => drinkIds.Contains(d.id)).ToList();

            var dailyProfit = 0.0;
            foreach (var drinkQuantityPair in selectedDrinks)
            {
                var drink = drinksFromDb.FirstOrDefault(d => d.id == drinkQuantityPair.DrinkId);
                if (drink != null)
                {
                    dailyProfit += drink.price * drinkQuantityPair.Quantity;
                }
            }

            ViewBag.DailyProfit = dailyProfit;
            Console.WriteLine(dailyProfit.ToString());
            var message = optimalProfitMessage(dailyProfit); // Call the optimalProfitMessage function
            return Json(new { dailyProfit = dailyProfit, message = message });
        }

        public class DrinkQuantityPair
        {
            public int DrinkId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
