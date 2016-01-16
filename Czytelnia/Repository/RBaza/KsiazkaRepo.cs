using Repository.RInterface;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Repository.RBaza
{
    public class KsiazkaRepo : IKsiazkaRepo
    {
        private readonly ICzytelniaContext _db;
        public KsiazkaRepo(ICzytelniaContext db)
        {
            _db = db;
        }

        public IQueryable<Ksiazka> PobierzKsiazki()
        {
            _db.Database.Log = message => Trace.WriteLine(message);
            var ksiazki = _db.Ksiazki.AsNoTracking();
            return ksiazki;
        }

        public Ksiazka GetKsiazkaById(int id)
        {
            Ksiazka ksiazka = _db.Ksiazki.Find(id);
            return ksiazka;
        }

        public void Dodaj(Ksiazka ksiazka)
        {
            _db.Ksiazki.Add(ksiazka);
        }

        public void Aktualizuj(Ksiazka ksiazka)
        {
            _db.Entry(ksiazka).State = EntityState.Modified;
        }


        public void UsunKsiazka(int id)
        {
            UsunPowiazanieKsiazkaRodzaj(id);
            Ksiazka ksiazka = _db.Ksiazki.Find(id);
            _db.Ksiazki.Remove(ksiazka);

        }

        //Aby usunięcie książki było możliwe, konieczne jest wcześniejsze usunięcie 
        //wpisów z tabeli ksiazka_gatunek
        private void UsunPowiazanieKsiazkaRodzaj(int idKsiazki)
        {
            var list = _db.Ksiazka_Rodzaj.Where(o => o.KsiazkaId == idKsiazki);
            foreach (var el in list)
            {
                _db.Ksiazka_Rodzaj.Remove(el);
            }

        }

        public IQueryable<Ksiazka> PobierzStrone(int? page = 1, int? pageSize = 10)
        {
            var ksiazki = _db.Ksiazki
                .OrderByDescending(o => o.DataDodania)
                .Skip((page.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);

            return ksiazki;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}