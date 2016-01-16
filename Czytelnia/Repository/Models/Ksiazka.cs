using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class Ksiazka
    {
        public Ksiazka()
        {
            this.Ksiazka_Rodzaj = new HashSet<Ksiazka_Rodzaj>();
        }
        [Display(Name = "Id:")] // using System.ComponentModel.DataAnnotations;
        public int Id { get; set; }

        [Required(ErrorMessage = "Wymagane jest nazwisko autora")]
        [Display(Name = "Autor książki:")]
        [MinLength(2, ErrorMessage = "Nazwisko autora musi mieć co najmniej dwa znaki")]
        [MaxLength(100)]
        public string Autor { get; set; }

        [Required(ErrorMessage = "Wymagany jest tytuł książki")]
        [Display(Name = "Tytuł książki:")]
        [MaxLength(100)]
        public string Tytul { get; set; }

        [Required(ErrorMessage = "Wymagany jest gatunek książki")]
        [Display(Name = "Gatunek książki:")]
        [MaxLength(30, ErrorMessage = "Nazwa gatunku może mieć najwyżej 30 znaków")]
        [MinLength(3, ErrorMessage = "Nazwa gatunku musi mieć co najmniej 3 znaki")]
        public string Gatunek { get; set; }

        [Display(Name = "Rok wydania")]
        [DataType(DataType.Date, ErrorMessage = "Wymagany format: rrrr-mm-dd")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime RokWydania { get; set; }

        [Display(Name = "Data dodania:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime DataDodania { get; set; }

        public string CzytelnikId { get; set; }

        public virtual ICollection<Ksiazka_Rodzaj> Ksiazka_Rodzaj { get; set; }
        public virtual Czytelnik Czytelnik { get; set; }
    }
}