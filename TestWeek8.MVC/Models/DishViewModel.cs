using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestWeek8.Core.Models;

namespace TestWeek8.MVC.Models
{
    public class DishViewModel
    {
        [Required(ErrorMessage ="Id obbligatorio")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome obbligatorio")]
        [MaxLength(50,ErrorMessage ="Massimo 50 caratteri")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Descrizione obbligatoria")]
        [MaxLength(400, ErrorMessage = "Massimo 400 caratteri")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Tipo obbligatorio")]
        public Typology Type { get; set; }
        [Required(ErrorMessage = "Prezzo obbligatorio")]
        public decimal Price { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
    }
}
