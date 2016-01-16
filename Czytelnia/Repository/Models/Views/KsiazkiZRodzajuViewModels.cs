using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.Models.Views
{
    public class KsiazkiZRodzajuViewModels
    {
        public IList<Ksiazka> Ksiazki { get; set; }
        public string NazwaRodzaju { get; set; }
    }
}