using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class Rodzaj
    {
        public Rodzaj()
        {
            this.Ksiazka_Rodzaj = new HashSet<Ksiazka_Rodzaj>();
        }

        [Key]
        [Display(Name = "ID gatunku:")]
        public int Id { get; set; }

        [Display(Name = "Nazwa rodzaju")]
        [Required]
        public string Nazwa { get; set; }
        
        public ICollection<Ksiazka_Rodzaj> Ksiazka_Rodzaj { get; set; }
    }
}