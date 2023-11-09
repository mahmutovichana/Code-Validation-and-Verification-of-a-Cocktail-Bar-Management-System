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

namespace SmartCafe.Controllers
{

    public class AdminPanelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminPanelController(ApplicationDbContext context)
        {
            _context = context;
        }

        //sort method (newly added)
        
        private List<Drink> bubbleSort(List<Drink> drinks)
        {
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
            int n = ingredients.Count;
            for (int i = 1; i < n; i++)
            {
                Ingredient key = ingredients[i];
                int j = i - 1;

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
            var drinks = _context.Drinks.ToList();
            double optimalProfit = 0;
            for (int i = 0; i < drinks.Count; i++)
            {
                optimalProfit += drinks[i].price;
            }
            if (realProfit < optimalProfit)
            {
                return "You are below optimal profit";
            }
            else if (realProfit == optimalProfit)
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
            bubbleSort(drinks);
            // number of drinks
            ViewBag.NumberOfDrinks = numberOfDrinks(drinks);
            // cheapest drink
            ViewBag.CheapestDrink = cheapestDrink(drinks);


            return View(drinks);
        }

        // GET: AdminPanel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks
                .FirstOrDefaultAsync(m => m.id == id);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        // GET: AdminPanel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminPanel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,price")] Drink drink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(drink);
        }

        // GET: AdminPanel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks.FindAsync(id);
            if (drink == null)
            {
                return NotFound();
            }
            return View(drink);
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
                        _context.Update(drink);
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


        // GET: AdminPanel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks
                .FirstOrDefaultAsync(m => m.id == id);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        // POST: AdminPanel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drink = await _context.Drinks.FindAsync(id);
            _context.Drinks.Remove(drink);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrinkExists(int id)
        {
            return _context.Drinks.Any(e => e.id == id);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Drink>> SearchDrinksByName(string searchTerm)
        {
            var drinks = _context.Drinks
                .Where(d => d.name.Contains(searchTerm))
                .ToList();
            return drinks;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Drink>> SearchDrinksByIngredient(string searchTerm)
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
        public ActionResult CalculateDailyProfit([FromBody] List<DrinkQuantityPair> selectedDrinks)
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
