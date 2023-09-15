using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities db = new MvcDbStokEntities();

        public ActionResult Kategoriler(int sayfa = 1)
        {
            //var degerler = db.TBL_KATEGORILER.ToList();
            var degerler = db.TBL_KATEGORILER.ToList().ToPagedList(sayfa, 4);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKategori(TBL_KATEGORILER p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            db.TBL_KATEGORILER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Kategoriler");
        }
        public ActionResult SIL(int id)
        {
            var kategori = db.TBL_KATEGORILER.Find(id);
            db.TBL_KATEGORILER.Remove(kategori);
            db.SaveChanges();

            return RedirectToAction("Kategoriler");
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TBL_KATEGORILER.Find(id);
            return View("KategoriGetir", ktgr);
        }
        public ActionResult Guncelle(TBL_KATEGORILER p1)
        {
            var ktg = db.TBL_KATEGORILER.Find(p1.KATEGORIID);
            ktg.KATEGORİAD = p1.KATEGORİAD;
            db.SaveChanges();

            return RedirectToAction("Kategoriler");
        }
    }
}