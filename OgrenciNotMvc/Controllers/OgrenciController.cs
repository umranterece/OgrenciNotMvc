using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;
namespace OgrenciNotMvc.Controllers
{
    public class OgrenciController : Controller
    {
        // GET: Ogrenci

        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index()
        {
            var ogrenci = db.Tbl_Ogrenci.ToList();
            return View(ogrenci);
        }

        [HttpGet]
        public ActionResult OgrenciEkle()
        {
            //List<SelectListItem> items = new List<SelectListItem>();
            //items.Add(new SelectListItem { Text = "Matematik", Value = "0" });
            //items.Add(new SelectListItem { Text = "Fen Bilgisi", Value = "1" });
            //items.Add(new SelectListItem { Text = "Ataturk Ilke ve Inkilaplari", Value = "2" });
            //items.Add(new SelectListItem { Text = "Cografya", Value = "3" });
            //ViewBag.DersAdlari = items;
            List<SelectListItem> degerler = (from i in db.Tbl_Kulup.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KulupAdi,
                                                 Value = i.KulupId.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult OgrenciEkle(Tbl_Ogrenci ogrenci)
        {
            var klp = db.Tbl_Kulup.Where(m => m.KulupId == ogrenci.Tbl_Kulup.KulupId).FirstOrDefault();
            ogrenci.Tbl_Kulup = klp;

            db.Tbl_Ogrenci.Add(ogrenci);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var ogrencis = db.Tbl_Ogrenci.Find(id);
            db.Tbl_Ogrenci.Remove(ogrencis);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var nots = db.Tbl_Ogrenci.Find(id);
            List<SelectListItem> degerler = (from i in db.Tbl_Kulup.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KulupAdi,
                                                 Value = i.KulupId.ToString()
                                             }).ToList();
            ViewBag.yenidgr = degerler;
            return View("Guncelle", nots);

        }

        [HttpPost]
        public ActionResult Guncelle(Tbl_Ogrenci  ogr)
        {
            var nots = db.Tbl_Ogrenci.Find(ogr.OgrId);
            nots.OgrAdi = ogr.OgrAdi;
            nots.OgrSoyadi=ogr.OgrSoyadi;
            nots.OgrFotograf = ogr.OgrFotograf;
            nots.OgrCinsiyet = ogr.OgrCinsiyet;

            var klp = db.Tbl_Kulup.Where(m => m.KulupId == ogr.Tbl_Kulup.KulupId).FirstOrDefault();
            nots.Tbl_Kulup = klp;



            db.SaveChanges();
            return RedirectToAction("Index","Ogrenci");
        }
    }
}