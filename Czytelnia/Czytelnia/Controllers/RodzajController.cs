using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.Models;
using Repository.RInterface;
using Repository.Models.Views;

namespace Czytelnia.Controllers
{
    public class RodzajController : Controller
    {
        private readonly IRodzajRepo _repo;

        public RodzajController(IRodzajRepo repo)
        {
            _repo = repo;
        }

        // GET: Rodzaj
        public ActionResult Index()
        {
            var rodzaje = _repo.PobierzRodzaje().AsNoTracking();
            return View(rodzaje);
        }

        public ActionResult PokazKsiazki(int id)
        {
            var ksiazki = _repo.PobierzKsiazkiZRodzaju(id);
            KsiazkiZRodzajuViewModels model = new KsiazkiZRodzajuViewModels();
            model.Ksiazki = ksiazki.ToList();
            model.NazwaRodzaju = _repo.NazwaDlaRodzaju(id);
            return View(model);
        }

        [Route("JSON")]
        public ActionResult RodzajeWJson()
        {
            var rodzaje = _repo.PobierzRodzaje().AsNoTracking();
            return Json(rodzaje, JsonRequestBehavior.AllowGet);
        }
    }
}
