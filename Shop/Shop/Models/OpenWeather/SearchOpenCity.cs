using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models.OpenWeather
{
    public class SearchOpenCity
    {
        [Required(ErrorMessage = "City not found!")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Only text allowed")]
        [Display(Name = "Please enter the name of a city")]
        public string OpenCityName { get; set; }
    }
}
