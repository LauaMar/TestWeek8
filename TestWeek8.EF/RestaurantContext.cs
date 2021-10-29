using Microsoft.EntityFrameworkCore;
using System;
using TestWeek8.Core.Models;
using TestWeek8.EF.Configurations;

namespace TestWeek8.EF
{
    public class RestaurantContext : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<User> Users { get; set; }
        public RestaurantContext() { }

        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DishConfiguration());
            builder.ApplyConfiguration(new MenuConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
