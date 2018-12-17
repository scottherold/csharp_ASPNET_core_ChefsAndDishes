// Created to have multiple modles display on the same view.
// Use properties as accessors to the actual model classes

using Microsoft.EntityFrameworkCore;

namespace ChefsAndDishes.Models
{
    // Estbalishes DB connection
    public class ChefDishContext : DbContext
    {
        public ChefDishContext(DbContextOptions<ChefDishContext> options) : base(options) { }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Dish> Dishes { get; set; }
    }
}