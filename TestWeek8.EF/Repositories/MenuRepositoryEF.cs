using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWeek8.Core.Interfaces;
using TestWeek8.Core.Models;

namespace TestWeek8.EF.Repositories
{
    public class MenuRepositoryEF : IMenuRepository
    {

        private readonly RestaurantContext ctx;
        public MenuRepositoryEF(RestaurantContext context)
        {
            ctx = context;
        }
        public bool AddItem(Menu item)
        {
            if (item == null)
                return false;

            ctx.Menus.Add(item);
            ctx.SaveChanges();
            return true;
        }

        public bool DeleteItemById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Menu> Fetch(Func<Menu, bool> filter = null)
        {
            if (filter != null)
                return ctx.Menus.Include(menu => menu.Dishes).Where(filter);

            return ctx.Menus.Include(menu => menu.Dishes);
        }

        public Menu GetById(int id)
        {
            if (id <= 0)
                return null;

            var menu = ctx.Menus.Include(menu => menu.Dishes).SingleOrDefault(m=>m.Id==id);
            return menu;
        }
        
        public bool UpdateItem(Menu updatedItem)
        {
            throw new NotImplementedException();
        }
    }
}
