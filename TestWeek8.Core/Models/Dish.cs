using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWeek8.Core.Models
{
    public enum Typology { First, Second, Side, Dessert}
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Typology Type { get; set; }
        public decimal Price { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
    }
}
