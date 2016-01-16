using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.RInterface
{
    public interface IRodzajRepo
    {
        IQueryable<Rodzaj> PobierzRodzaje();

        IQueryable<Ksiazka> PobierzKsiazkiZRodzaju(int id);
        string NazwaDlaRodzaju(int id);
    }
}