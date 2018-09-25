using Microsoft.AspNetCore.Mvc;
using NLog;
using OdeToFood.Models;
using OdeToFood.Services;
using OdeToFood.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;
        private IGreeter _greeter;

        public HomeController(IRestaurantData restaurantData,
                            IGreeter greeter)
        {
            _restaurantData = restaurantData;
            _greeter = greeter;
        }

        public IActionResult Index()
        {


            // using Nlog
            try
            {
                int zero = 0;
                int result = 5 / zero;
            }
            catch (DivideByZeroException ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.ErrorException("DUMMY EXCEPTION", ex);
            }

            //this.ControllerContext.ActionDescriptor.ActionName;
            //this.HttpContext.Response.OnStarting.ToString; // do not use this offen

            //return this.BadRequest();
            //return Content("Hello from HomeController!"); // this is where we decide what to return; Because of IActionResolut, the MVC framework will be the one to execute this and do return the result to the client

            //var model = new Restaurant { Id = 1, Name= "Pizzeria Tome"};
            //var model = _restaurantData.GetAll();
            var model = new HomeIndexViewModel();
            model.Restaurants = _restaurantData.GetAll();
            model.CurrentMessage = _greeter.GetMessageOfTheDay();

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _restaurantData.GetById(id);
            //return Content(id.ToString());

            if(model == null)
            {
                //return View("NotFound");
                return RedirectToAction(nameof(Index)); //RedirectToAction("Index");
                return NotFound(); // for APIs
                
            }

            return View(model);
        }


        // this will be on GET - add route contraints
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // this will be on POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RestaurantEditModel model)
        {

            // model state
            if (ModelState.IsValid)
            {
                var newRestaurant = new Restaurant();
                newRestaurant.Name = model.Name;
                newRestaurant.Cuisine = model.Cuisine;

                newRestaurant = _restaurantData.AddNewRestaurant(newRestaurant);

                //return View("Details", newRestaurant);
                return RedirectToAction(nameof(Details), new { id = newRestaurant.Id });
            } else
            {
                return View();
            }

        }

    }
}
