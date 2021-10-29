using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class DishController : Controller
    {
        private readonly IMainBusinessLayer mainBL;

        public DishController(IMainBusinessLayer bl)
        {
            mainBL = bl;
        }
        [Authorize(Policy = "UtenteRistoratore")]
        public IActionResult Create()
        {
            LoadViewBag();
            return View(new DishViewModel());
        }

        [HttpPost]
        public IActionResult Create(DishViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model == null)
            {
                return View("Error", new ResultBL(false, "Error!"));
            }
            //Mappatura da Employee View Model a Employee
            Dish newDish = model.ToDish();
            var result = mainBL.CreateDish(newDish);
            if (!result.Success)
            {
                return View("ExceptionError", result);
            }
            //return Redirect($"/Menu/Details/{model.MenuId}");
            return Redirect("/Menu/Index");
        }

        [Authorize(Policy = "UtenteRistoratore")]
        public IActionResult Edit(int id)
        {
            if (id <= 0)
                return View("NotFound");
            var dishToEdit = mainBL.GetDishById(id);
            if (dishToEdit == null)
                return View("NotFound");
            var prodViewModel = dishToEdit.ToDishViewModel();
            LoadViewBag();
            return View(prodViewModel);
        }

        [HttpPost]
        public IActionResult Edit(DishViewModel dvm)
        {
            if (!ModelState.IsValid)
            {
                return View(dvm);
            }
            if (dvm == null)
                return View("Error", new ResultBL(false, "Something wrong!"));

            var dishToEdit = dvm.ToDish();
            var result = mainBL.EditDish(dishToEdit);
            if (!result.Success)
            {
                return View("Error", result);
            }
            return Redirect($"/Menu/Details/{dvm.MenuId}");
        }

        [Authorize(Policy = "UtenteRistoratore")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return View();

            // chiamata a BL ...
            Dish dishToDelete = mainBL.GetDishById(id);
            DishViewModel dishVM = dishToDelete.ToDishViewModel();

            return View(dishVM);
        }

        [HttpPost]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            if (id <= 0)
                return View();
            Dish dishToDelete = mainBL.GetDishById(id);

            var result = mainBL.DeleteDishById(id);

            if (!result.Success)
                return View("Error", null);


            return Redirect($"/Menu/Details/{dishToDelete.MenuId}");
        }

        private void LoadViewBag()
        {

            ViewBag.Typologies = MappingExtensions.FromEnumToSelectList<Typology>();
        }
    }
}
