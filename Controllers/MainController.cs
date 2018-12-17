using System;
using Microsoft.AspNetCore.Mvc;
using ChefsAndDishes.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ChefsAndDishes
{
    public class MainController : Controller
    {
        // Sets up dbContext for queries
        private ChefDishContext dbContext;

        // Creates DB context on accessing the controller
        public MainController(ChefDishContext context)
        {
            dbContext = context;
        }

        // Route for the main/default View
        [HttpGet("")]
        public IActionResult Index()
        {
            List<Chef> DisplayChefs = dbContext.Chefs.Include(c => c.CreatedDishes).ToList();
            return View("Index", DisplayChefs);
        }

        // Route to add a new chef
        [HttpGet("newChef")]
        public IActionResult AddChef()
        {
            return View("AddChef");
        }

        // Route for all dishes
        [HttpGet("dishes")]
        public IActionResult ViewDishes()
        {
            // list of dishes, including the "creator" field to access chef object
            List<Dish> allDishes = dbContext.Dishes.Include(dish => dish.Creator).ToList();
            return View("Dishes", allDishes);
        }

        // Route to add a new dish
        [HttpGet("newDish")]
        public IActionResult AddDish()
        {
            // Need to utilize the bundle to access both chef and dish models
            var chefDishBundle = new ChefDishBundle();
            chefDishBundle.AllChefs=dbContext.Chefs.ToList();
            return View("AddDish", chefDishBundle);
        }

        // route to process the creation of a new chef in the database
        [HttpPost("createChef")]
        public IActionResult CreateChef(Chef chef)
        {
            // Checks validations
            if(ModelState.IsValid)
            {
                // If age is greater than or equal to 18 (see Chef.cs)
                if(chef.Age>=18)
                {
                    // updates DateTime values
                    chef.CreatedAt = DateTime.Now;
                    chef.UpdatedAt = DateTime.Now;
                    dbContext.Add(chef);
                    dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                // Throws Age registration error
                else
                {
                    ModelState.AddModelError("Birthday", "A Chef must be 18 years or older to be registered!");
                    return View("AddChef");
                }
            }
            // Throws ModelState errors
            else
            {
                return View("AddChef");
            }
        }

        // route to create a new dish in the database
        [HttpPost("createDish")]
        public IActionResult CreateDish(ChefDishBundle chefDishBundle)
        {
            // creates a dish object from the bundled model
            var newDish = chefDishBundle.dish;
            if(ModelState.IsValid)
            {
                // updates DateTime values
                newDish.CreatedAt = DateTime.Now;
                newDish.UpdatedAt = DateTime.Now;
                // queries to add the dish object to the DB
                dbContext.Dishes.Add(newDish);
                dbContext.SaveChanges();
                return RedirectToAction("ViewDishes");
            }
            else
            {
                // if model state is invalid, uses the created bundle
                // to query the db, for the chef list on the add
                // dish view.
                chefDishBundle.AllChefs=dbContext.Chefs.ToList();
                return View("AddDish", chefDishBundle);
            }
        }
    }
}