using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChefsAndDishes.Models
{
    public class Dish
    {
        // This sets the key for the SQL database
        [Key]
        public int DishId { get; set; }
        [Display(Name="Dish's Name")]
        [Required(ErrorMessage="Dish Name is required!")]
        public string Name { get; set ;}
        [Display(Name="Dish's Calories")]
        [Required(ErrorMessage="Dish Calories are required!")]
        [Range(1,5000,ErrorMessage="Dish caloreis must be a valid number between 1 and 5000!")]
        public int? Calories { get; set; }
        [Display(Name="Dish's Tastiness")]
        [RegularExpression(@"^[1-9]|5*$", ErrorMessage = "What are you doing?! Tastiness must be a number 1-5!")]
        public int? Tastiness { get; set; }
        [Display(Name="Dishes Description")]
        [Required(ErrorMessage = "Description is required!")]
        public int ChefId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Chef Creator { get; set; }

    }
}