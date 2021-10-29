using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWeek8.Core.Models;

namespace TestWeek8.Core.Interfaces
{
    public interface IMainBusinessLayer
    {
        #region Menu
        IEnumerable<Menu> FetchMenus(Func<Menu, bool> filter = null);
        ResultBL CreateMenu(Menu newMenu);
        Menu GetMenuById(int id);
        #endregion

        #region User
        User GetUserByEmail(string email);
        #endregion

        #region Dish
        IEnumerable<Dish> FetchDishes(Func<Dish, bool> filter = null);
        Dish GetDishById(int id);
        ResultBL CreateDish(Dish newDish);
        ResultBL EditDish(Dish modifiedDish);
        ResultBL DeleteDishById(int id);
        #endregion
    }
}
