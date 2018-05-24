using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        public InMemoryRestaurantData()
        {
            _restaurnats = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "Restaurant 1"},
                new Restaurant { Id = 2, Name = "Restaurant 2"},
                new Restaurant { Id = 3, Name = "Restaurant 3"},
                new Restaurant { Id = 4, Name = "Restaurant 4"},
                new Restaurant { Id = 5, Name = "Restaurant 5"}
            };
        }

        

        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurnats.OrderBy(r => r.Name);
        }

        public Restaurant GetById(int id)
        {
            return _restaurnats.FirstOrDefault(r => r.Id == id);
        }

        public Restaurant AddNewRestaurant(Restaurant restaurant)
        {
            restaurant.Id = _restaurnats.Max(r => r.Id) + 1;
            _restaurnats.Add(restaurant);

            return restaurant; 
        }

        // declaration of private fields at the bottom
        List<Restaurant> _restaurnats;

    }
}

