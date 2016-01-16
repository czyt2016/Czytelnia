using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class Ksiazka_Rodzaj
    {
        public Ksiazka_Rodzaj()
        {

        }

        public int Id { get; set; }
        public int RodzajId { get; set; }
        public int KsiazkaId { get; set; }
        public virtual Rodzaj Rodzaj { get; set; }
        public virtual Ksiazka Ksiazka { get; set; }
    }
}