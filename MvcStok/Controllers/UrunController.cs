using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Urunler()
        {
            var degerler = db.TBL_URUNLER.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> degerler = (from i in db.TBL_KATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORİAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(TBL_URUNLER p1)
        {
            var ktg = db.TBL_KATEGORILER.Where(m => m.KATEGORIID == p1.TBL_KATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBL_KATEGORILER = ktg;
            db.TBL_URUNLER.Add(p1);
            db.SaveChanges();

            return RedirectToAction("Urunler");
        }
        public ActionResult SIL(int id)
        {
            var urun = db.TBL_URUNLER.Find(id);
            db.TBL_URUNLER.Remove(urun);
            db.SaveChanges();

            return RedirectToAction("Urunler");
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBL_URUNLER.Find(id);

            List<SelectListItem> degerler = (from i in db.TBL_KATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORİAD,
                                                 Value = i.KATEGORIID.ToString(),
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("UrunGetir", urun);
        }
        public ActionResult Guncelle(TBL_URUNLER p)
        {
            var urun = db.TBL_URUNLER.Find(p.URUNID);
            urun.URUNAD = p.URUNAD;
            urun.MARKA = p.MARKA;
            urun.STOK = p.STOK;
            urun.FİYAT = p.FİYAT;
            //urun.URUNKATEGORİ = p.URUNKATEGORİ;
            var ktg = db.TBL_KATEGORILER.Where(m => m.KATEGORIID == p.TBL_KATEGORILER.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORİ = ktg.KATEGORIID;
            db.SaveChanges();

            return RedirectToAction("Urunler");
        }
    }
}