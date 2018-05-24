using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Restaurant> GetRestaurants { get; set; }
        public string CurrentMessage { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; internal set; }
    }
}
