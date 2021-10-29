using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWeek8.Core.Interfaces;
using TestWeek8.Core.Models;
using TestWeek8.MVC.Helpers;
using TestWeek8.MVC.Models;

namespace TestWeek8.MVC.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
       private readonly IMainBusinessLayer mainBL;
        public MenuController(IMainBusinessLayer bl)
        {
            mainBL = bl;
        }

        public IActionResult Index()
        {
            var result = mainBL.FetchMenus();
            var resultMapping = result.ToListViewModel();
            return View(resultMapping);
        }

        [Authorize(Policy = "UtenteRistoratore")]
        public IActionResult Create()
        {
            return View(new MenuViewModel());
        }


        [HttpPost]
        public IActionResult Create(MenuViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model == null)
                return View("Error", new ResultBL(false, "Error!"));

            Menu newMenu = model.ToMenu();
            var result = mainBL.CreateMenu(newMenu);
            if (!result.Success)
                return View("ExceptionError", result);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            if (id<=0)
                return View("NotFound");
            var menu = this.mainBL.GetMenuById(id);
            if (menu == null)
                return View("NotFound");
            var menuMapped = menu.ToViewModel();
            //var provaPiatti = menuMapped.Dishes;
            return View(menuMapped);
        }

    }
}
