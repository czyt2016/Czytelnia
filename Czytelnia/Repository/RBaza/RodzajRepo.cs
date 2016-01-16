using Repository.RInterface;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Repository.RBaza
{
    public class RodzajRepo : IRodzajRepo
    {
        private readonly ICzytelniaContext _db;
        public RodzajRepo(ICzytelniaContext db)
        {
            _db = db;
        }

        public IQueryable<Rodzaj> PobierzRodzaje()
        {
            _db.Database.Log = message => Trace.WriteLine(message);
            var rodzaje = _db.Rodzaje.AsNoTracking();
            return rodzaje;
        }

        public IQueryable<Ksiazka> PobierzKsiazkiZRodzaju(int id)
        {
            _db.Database.Log = message => Trace.WriteLine(message);
            var ksiazki =
                from o in _db.Ksiazki
                join k in _db.Ksiazka_Rodzaj on o.Id equals k.Id
                where k.RodzajId == id
                select o;

            return ksiazki;
        }
        public string NazwaDlaRodzaju(int id)
        {
            var nazwa = _db.Rodzaje.Find(id).Nazwa;
            return nazwa;
        }
    }
}