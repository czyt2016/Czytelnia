using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.Models;
using System.Diagnostics;
using Repository.RBaza;
using Repository.RInterface;
using Microsoft.AspNet.Identity;
using PagedList;

//kontroler (interakcja z użytkownikami i obsługa zdarzeń) będzie tylko wywoływał metody
//zapytania LINQ i obiekt kontekstu przeniesione do osobnych metod
namespace Czytelnia.Controllers
{
    public class KsiazkaController : Controller
    {
        //do pola IKsiazkaRepo _repo zostanie wstrzyknięta instancja repozytorium
        private readonly IKsiazkaRepo _repo;
        public KsiazkaController(IKsiazkaRepo repo)
        {
            _repo = repo;
        }

        // GET: Ksiazka
        //akcja Index otrzymuje tylko gotowe dane do wyświetlenia
        //metoda PobierzKsiazki() jest w kontrolerze i obiekt kontekstu jest tworzony w kontrolerze
        public ActionResult Index(int? page, string sortOrder, string searchString)
        {
            int currentPage = page ?? 1;
            int naStronie = 5;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdSort = String.IsNullOrEmpty(sortOrder) ? "IdAsc" : "";
            ViewBag.DataDodaniaSort = sortOrder == "DataDodania" ? "DataDodaniaAsc" : "DataDodania";
            ViewBag.AutorSort = sortOrder == "AutorAsc" ? "Autor" : "AutorAsc";
            ViewBag.TytulSort = sortOrder == "TytulAsc" ? "Tytul" : "TytulAsc";
            ViewBag.GatunekSort = sortOrder == "GatunekAsc" ? "Gatunek" : "GatunekAsc";
            ViewBag.RokWydaniaSort = sortOrder == "RokWydaniaAsc" ? "RokWydania" : "RokWydaniaAsc";

            var ksiazki = _repo.PobierzKsiazki();

            ksiazki = from k in _repo.PobierzKsiazki()
                      select k;
            if (!String.IsNullOrEmpty(searchString))
            {
                ksiazki = ksiazki.Where(s => s.Autor.Contains(searchString)
                                       || s.Gatunek.Contains(searchString)
                                       || s.Tytul.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "DataDodania":
                    ksiazki = ksiazki.OrderByDescending(s => s.DataDodania);
                    break;
                case "DataDodaniaAsc":
                    ksiazki = ksiazki.OrderBy(s => s.DataDodania);
                    break;
                case "Tytul":
                    ksiazki = ksiazki.OrderByDescending(s => s.Tytul);
                    break;
                case "TytulAsc":
                    ksiazki = ksiazki.OrderBy(s => s.Tytul);
                    break;
                case "Autor":
                    ksiazki = ksiazki.OrderByDescending(s => s.Autor);
                    break;
                case "AutorAsc":
                    ksiazki = ksiazki.OrderBy(s => s.Autor);
                    break;
                case "Gatunek":
                    ksiazki = ksiazki.OrderByDescending(s => s.Gatunek);
                    break;
                case "GatunekAsc":
                    ksiazki = ksiazki.OrderBy(s => s.Gatunek);
                    break;
                case "RokWydania":
                    ksiazki = ksiazki.OrderByDescending(s => s.RokWydania);
                    break;
                case "RokWydaniaAsc":
                    ksiazki = ksiazki.OrderBy(s => s.RokWydania);
                    break;
                case "IdAsc":
                    ksiazki = ksiazki.OrderBy(s => s.Id);
                    break;
                default:    // id descending
                    ksiazki = ksiazki.OrderByDescending(s => s.Id);
                    break;
            }

            return View(ksiazki.ToPagedList<Ksiazka>(currentPage, naStronie));
        }

        [OutputCache(Duration = 1000)]
        public ActionResult MojeKsiazki(int? page)
        {
            int currentPage = page ?? 1;
            int naStronie = 5;
            string userId = User.Identity.GetUserId();
            var ksiazki = _repo.PobierzKsiazki();
            ksiazki = ksiazki.OrderByDescending(d => d.DataDodania)
                .Where(o => o.CzytelnikId == userId);
            return View(ksiazki.ToPagedList<Ksiazka>(currentPage, naStronie));
        }

        // GET: Ksiazka
        public ActionResult Partial(int? page)
        {
            int currentPage = page ?? 1;
            int naStronie = 5;
            var ksiazki = _repo.PobierzKsiazki();
            ksiazki = ksiazki.OrderByDescending(d => d.DataDodania);
            return PartialView("Index", ksiazki.ToPagedList<Ksiazka>(currentPage, naStronie));
        }
        
        // GET: Ksiazka/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ksiazka ksiazka = _repo.GetKsiazkaById((int)id);
            if (ksiazka == null)
            {
                return HttpNotFound();
            }
            return View(ksiazka);
        }

        // GET: Ksiazka/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ksiazka/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        //Metoda POST dla akcji Create
        [HttpPost]
        //zabezpieczenie witryny przed CSRF (Cross-Site Request Forgery) atrybut [ValidateAntiForgeryToken]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Autor,Tytul,Gatunek,RokWydania")] Ksiazka ksiazka)
        {
            if (ModelState.IsValid)
            {
                // using Microsoft.AspNet.Identity;
                //automatyczne przypisanie Id czytelnika dodającego książkę
                ksiazka.CzytelnikId = User.Identity.GetUserId();
                //automatyczne przypisanie aktualnej daty jako DataDodania
                ksiazka.DataDodania = DateTime.Now;
                //w razie wystąpienia błędu powrót do widoku dodawania
                try
                {
                    _repo.Dodaj(ksiazka);
                    _repo.SaveChanges();
                    return RedirectToAction("MojeKsiazki");
                }
                catch
                {
                    return View(ksiazka);
                }
            }
            return View(ksiazka);
        }

        [Authorize]
        // GET: Ksiazka/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ksiazka ksiazka = _repo.GetKsiazkaById((int)id);
            if (ksiazka == null)
            {
                return HttpNotFound();
            }
            else if (ksiazka.CzytelnikId != User.Identity.GetUserId() && !(User.IsInRole("Admin") || User.IsInRole("Pracownik")))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(ksiazka);
        }

        // POST: Ksiazka/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Autor,Tytul,Gatunek,RokWydania,DataDodania,CzytelnikId")] Ksiazka ksiazka)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Aktualizuj(ksiazka);
                    _repo.SaveChanges();
                }
                catch
                {
                    ViewBag.Blad = true;
                    return View(ksiazka);
                }
            }
            ViewBag.Blad = false;
            return View(ksiazka);
        }

        // GET: Ksiazka/Delete/5
        public ActionResult Delete(int? id, bool? blad)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ksiazka ksiazka = _repo.GetKsiazkaById((int)id);
            if (ksiazka == null)
            {
                return HttpNotFound();
            }
            else if (ksiazka.CzytelnikId != User.Identity.GetUserId() && !User.IsInRole("Admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (blad != null)
                //aby przekazać z kontrolera do widoku informację o tym, że wystąpił błąd
                ViewBag.Blad = true;
            return View(ksiazka);
        }

        // POST: Ksiazka/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.UsunKsiazka(id);
            try
            {
                _repo.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Delete", new { id = id, blad = true });
            }

            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
