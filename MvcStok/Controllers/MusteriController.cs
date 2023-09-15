using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Musteriler(string p)
        {
            var degerler = from d in db.TBL_MUSTERİLER select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MUSTERİAD.Contains(p));
            }
            return View(degerler.ToList());
            //var degerler = db.TBL_MUSTERİLER.ToList();
            //return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(TBL_MUSTERİLER p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.TBL_MUSTERİLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Musteriler");
        }
        public ActionResult SIL(int id)
        {
            var musteriler = db.TBL_MUSTERİLER.Find(id);
            db.TBL_MUSTERİLER.Remove(musteriler);
            db.SaveChanges();

            return RedirectToAction("Musteriler");
        }
        public ActionResult MusteriGetir(int id)
        {
            var mus = db.TBL_MUSTERİLER.Find(id);
            return View("MusteriGetir", mus);
        }
        public ActionResult Guncelle(TBL_MUSTERİLER p1)
        {
            var musteri = db.TBL_MUSTERİLER.Find(p1.MUSTERİID);
            musteri.MUSTERİAD = p1.MUSTERİAD;
            musteri.MUSTERİSOYAD = p1.MUSTERİSOYAD;
            db.SaveChanges();
            return RedirectToAction("Musteriler");

        }

    }
}