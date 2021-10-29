using System;
using System.Collections.Generic;
using TestWeek8.Core.Interfaces;
using TestWeek8.Core.Models;

namespace TestWeek8.Core
{
    public class MainBusinessLayer : IMainBusinessLayer
    {
        private readonly IDishRepository dishRepo;
        private readonly IMenuRepository menuRepo;
        private readonly IUserRepository userRepo;

        public MainBusinessLayer(IDishRepository repoDish, IMenuRepository repoMenu, IUserRepository repoUser)
        {
            dishRepo = repoDish;
            menuRepo = repoMenu;
            userRepo = repoUser;
        }

        #region Menu

        public IEnumerable<Menu> FetchMenus(Func<Menu, bool> filter = null)
        {
            if (filter != null)
                return menuRepo.Fetch(filter);

            return menuRepo.Fetch();
        }

        public ResultBL CreateMenu(Menu newMenu)
        {
            if (newMenu == null)
                return new ResultBL(false, "Invalid Menu");

            var output = menuRepo.AddItem(newMenu);
            if (!output)
                return new ResultBL(output, "Something went wrong...");

            return new ResultBL(output, "OK!");
        }

        public Menu GetMenuById(int id)
        {
            if (id <= 0)
                return null;

            return menuRepo.GetById(id);
        }

        #endregion

        #region User
        public User GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;
            return userRepo.GetByEmail(email);
        }
        #endregion

        #region Dish
        public ResultBL CreateDish(Dish newDish)
        {
            if (newDish == null)
                return new ResultBL(false, "Dati inseriti non validi");
            var result = dishRepo.AddItem(newDish);

            return new ResultBL(result, result ? "Ok!" : "Impossibile creare il piatto");
        }

        public ResultBL DeleteDishById(int id)
        {

            if (id <= 0)
                return new ResultBL(false, "Id inserito non valido");

            var result = dishRepo.DeleteItemById(id);

            return new ResultBL(result, result ? "" : "Impossibile eliminare il piatto");
        }

        public ResultBL EditDish(Dish modifiedDish)
        {
            if (modifiedDish == null)
                return new ResultBL(false, "Dati inseriti non validi");
            var result = dishRepo.UpdateItem(modifiedDish);

            return new ResultBL(result, result ? "Ok!" : "Impossibile modificare il piatto");
        }

        public IEnumerable<Dish> FetchDishes(Func<Dish, bool> filter = null)
        {
            if (filter != null)
                return dishRepo.Fetch(filter);

            return dishRepo.Fetch();
        }

        public Dish GetDishById(int id)
        {
            if (id <= 0)
                return null;

            return dishRepo.GetById(id);
        }
        #endregion


    }
}
