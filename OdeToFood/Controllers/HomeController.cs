using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using OdeToFood.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;

        public HomeController(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }

        public IActionResult Index()
        {
            //this.ControllerContext.ActionDescriptor.ActionName;
            //this.HttpContext.Response.OnStarting.ToString; // do not use this offen

            //return this.BadRequest();
            //return Content("Hello from HomeController!"); // this is where we decide what to return; Because of IActionResolut, the MVC framework will be the one to execute this and do return the result to the client

            //var model = new Restaurant { Id = 1, Name= "Pizzeria Tome"};
            var model = _restaurantData.GetAll();

            return View(model);
        }
    }
}
