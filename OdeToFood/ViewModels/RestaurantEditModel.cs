using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.ViewModels
{
    public class RestaurantEditModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }
        // model binding - the process of moving data into my input model from the request; the framework produces a model state that can be checked
        //

        public CuisineType Cuisine { get; set; }
    }
}
