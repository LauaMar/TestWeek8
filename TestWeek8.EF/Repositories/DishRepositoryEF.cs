using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWeek8.Core.Interfaces;
using TestWeek8.Core.Models;

namespace TestWeek8.EF.Repositories
{
    public class DishRepositoryEF : IDishRepository
    {

        private readonly RestaurantContext ctx;
        public DishRepositoryEF(RestaurantContext context)
        {
            ctx = context;
        }

        public bool AddItem(Dish item)
        {
            if (item == null)
                return false;

            ctx.Dishes.Add(item);
            ctx.SaveChanges();
            return true;
        }

        public bool DeleteItemById(int id)
        {
            if (id <= 0)
                return false;

            var itemToDelete = ctx.Dishes.Find(id);
            if (itemToDelete == null)
                return false;

            ctx.Dishes.Remove(itemToDelete);
            ctx.SaveChanges();
            return true;
        }

        public IEnumerable<Dish> Fetch(Func<Dish, bool> filter = null)
        {
            if (filter != null)
                return ctx.Dishes.Where(filter);

            return ctx.Dishes;
        }
    

        public Dish GetById(int id)
        {
        if (id <= 0)
            return null;

        return ctx.Dishes.Find(id);
        }

        public bool UpdateItem(Dish updatedItem)
        {
            if (updatedItem == null)
                return false;

            ctx.Entry(updatedItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            ctx.SaveChanges();
            return true;
        }
    }
}
