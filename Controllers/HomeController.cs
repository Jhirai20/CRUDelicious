using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CRUDelicious.Models;

using Microsoft.EntityFrameworkCore;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {

        //Dependency Injection of Context Model | Make sure there is only one dependency injection statement
        private MyContext _context;
        private readonly ILogger<HomeController> _logger;
        public HomeController(MyContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Dish> AllDishes =_context.Dishes.OrderByDescending(l =>l.DishId).ToList();
            return View("Index", AllDishes);
        }
        [HttpGet("new")]
        public IActionResult New()
        {
            return View("New");
        }
        [HttpPost("create")]
        public IActionResult CreateUser(Dish nDish)
        {
            if(ModelState.IsValid)
            {
                _context.Add(nDish);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("New"); 
        }
        [HttpGet("{dishId}")]
        public ViewResult Detail(int dishId)
        {
            Dish dish = _context.Dishes.SingleOrDefault(dish =>dish.DishId ==dishId);
            return View("Detail",dish);
        }
        [HttpGet("edit/{dishId}")]
        public IActionResult Edit(int dishId)
        {
            Dish dish = _context.Dishes.SingleOrDefault(dish=>dish.DishId==dishId);
            return View("Edit",dish);
        }
        [HttpPost("edit/{dishId}")]
        public IActionResult Update(int dishId,Dish updatedDish)
        {
            if(ModelState.IsValid)
            {
                Dish dish=_context.Dishes.SingleOrDefault(dish=> dish.DishId == dishId);
                dish.Name=updatedDish.Name;
                dish.Tastiness=updatedDish.Tastiness;
                dish.UpdatedAT=DateTime.Now;
                dish.Calories=updatedDish.Calories;
                dish.Chef=updatedDish.Chef;
                dish.Description=updatedDish.Description;
                _context.SaveChanges();
                return Detail(dishId);
            }
            return View("Edit", updatedDish);
        }
        [HttpGet("delete/{dishId}")]
        public IActionResult Delete(int dishId)
        {
            Dish dish=_context.Dishes.SingleOrDefault(d => d.DishId==dishId);
            _context.Dishes.Remove(dish);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
