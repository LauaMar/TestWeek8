using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWeek8.Core.Models;
using TestWeek8.MVC.Models;

namespace TestWeek8.MVC.Helpers
{
    public static class MappingExtensions
    {
        public static MenuViewModel ToViewModel(this Menu menu)
        {

            var dishesVM = menu.Dishes.ToListOfDishViewModel();
            return new MenuViewModel
            {
                Id = menu.Id,
                Name = menu.Name,
                Dishes= dishesVM,

            };
        }

        public static IEnumerable<MenuViewModel> ToListViewModel(this IEnumerable<Menu> menus)
        {
            return menus.Select(m => m.ToViewModel());
        }

        public static Menu ToMenu (this MenuViewModel mvm)
        {
            return new Menu
            {
                Id=mvm.Id,
                Name=mvm.Name
            };
        }

        public static IEnumerable<SelectListItem> FromEnumToSelectList<T>() where T : struct
        {
            return (Enum.GetValues(typeof(T))).Cast<T>().Select(
                    e => new SelectListItem() { Text = e.ToString(), Value = e.ToString() }).ToList();
        }

        public static Dish ToDish (this DishViewModel dvm)
        {
            return new Dish
            {
                Id = dvm.Id,
                Name = dvm.Name,
                Description = dvm.Description,
                Type = dvm.Type,
                Price = dvm.Price,
                MenuId = dvm.MenuId
            };
        }

        public static DishViewModel ToDishViewModel(this Dish dish)
        {
            return new DishViewModel 
            {
                Id=dish.Id,
                Name=dish.Name,
                Description=dish.Description,
                Type=dish.Type,
                Price=dish.Price,
                MenuId=dish.MenuId
            };
        }
        public static IEnumerable<DishViewModel> ToListOfDishViewModel(this IEnumerable<Dish> dishes)
        {
            return dishes.Select(d => d.ToDishViewModel());
        }
    }
}
