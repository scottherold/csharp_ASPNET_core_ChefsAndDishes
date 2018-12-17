using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ChefsAndDishes.Models
{
    public class Chef
    {
        // This sets the key for the SQL database
        [Key]
        public int ChefId { get; set; }
        [Display(Name="Chef's First Name")]
        [Required(ErrorMessage="The Chef's First Name is required!")]
        public string FirstName { get; set; }
        [Display(Name="Chef's Last Name")]
        [Required(ErrorMessage="The Chef's Last Name is required!")]
        public string LastName { get; set; }
        [Display(Name="Chef's Birthday")]
        [Required(ErrorMessage="The Chef's Birthday is required!")]
        public DateTime Birthday { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Dish> CreatedDishes { get; set; }

        // Age is created at time of creation to validate if the
        // chef is over 18.
        // Age is only established with a Getter so it will not push to the DB
        // this can be used for validation, or printing purposes without
        // adding extra information into the DB.
        public int Age 
        { 
            get
            {
                // Grabs current local DateTime -- n = now
                DateTime n = DateTime.Now;

                // Grabs the age by taking the year from n, and subtracing the year from
                // birthday
                int age = n.Year - Birthday.Year;

                // Leap year handler
                if (n.Month < Birthday.Month || (n.Month == Birthday.Month && n.Day < Birthday.Day))
                {
                    age--;
                }
                return age;
            } 
        }
    }
}