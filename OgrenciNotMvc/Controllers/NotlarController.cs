using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;
using OgrenciNotMvc.Models;
using System.Security.Cryptography;

namespace OgrenciNotMvc.Controllers
{
    public class NotlarController : Controller
    {
        // GET: Notlar

        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index()
        {
            var notlar = db.Tbl_Not.ToList();
            return View(notlar);

        }

        [HttpGet]
        public ActionResult NotEkle()
        {
            List<SelectListItem> degerler = (from i in db.Tbl_Dersler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.DersAdi,
                                                 Value = i.DersId.ToString()
                                             }).ToList();
            ViewBag.gelenders = degerler;

            List<SelectListItem> degerler2 = (from item in db.Tbl_Ogrenci.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = item.OgrAdi + " " + item.OgrSoyadi,
                                                  Value = item.OgrId.ToString()
                                              }).ToList();
            ViewBag.gelenogrenci = degerler2;
            return View();
        }

        [HttpPost]
        public ActionResult NotEkle(Tbl_Not not)
        {

            var drs = db.Tbl_Dersler.Where(m => m.DersId == not.Tbl_Dersler.DersId).FirstOrDefault();
            not.Tbl_Dersler = drs;

            var ogr = db.Tbl_Ogrenci.Where(m => m.OgrId == not.Tbl_Ogrenci.OgrId).FirstOrDefault();
            not.Tbl_Ogrenci = ogr;


            db.Tbl_Not.Add(not);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var nots = db.Tbl_Not.Find(id);
            return View("Guncelle", nots);

        }

        [HttpPost]
        public ActionResult Guncelle(Class1 model, Tbl_Not p, int Sinav1=0, int Sinav2= 0, int Sinav3= 0, int Proje = 0) 
        {
            if (model.Islem == "Hesapla")
            {
                //Islem1
                int Ortalama = (Sinav1 + Sinav2 + Sinav3 + Proje) / 4;
                ViewBag.ortalama = Ortalama.ToString();

            }

            if (model.Islem == "NotGuncelle")
            {
                //Islem2
                var snv = db.Tbl_Not.Find(p.NotId);
                snv.Sinav1 = p.Sinav1;
                snv.Sinav2 = p.Sinav2;
                snv.Sinav3 = p.Sinav3;
                snv.Proje = p.Proje;
                snv.Ortalama = p.Ortalama;
                db.SaveChanges();
                return RedirectToAction("Index", "Notlar");
            }
                return View();


        }
    }
}