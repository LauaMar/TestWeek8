using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestWeek8.Core.Models;

namespace TestWeek8.MVC.Models
{
    public class MenuViewModel
    {
        [Required(ErrorMessage ="Id obbligatorio!")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome obbligatorio!")]
        [MaxLength(50, ErrorMessage="Massimo 50 caratteri!")]
        public string Name { get; set; }
        public IEnumerable<DishViewModel> Dishes { get; set; }
    }
}
