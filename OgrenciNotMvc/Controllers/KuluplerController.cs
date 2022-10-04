using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;

namespace OgrenciNotMvc.Controllers
{
    public class KuluplerController : Controller
    {

        DbMvcOkulEntities db = new DbMvcOkulEntities();
        // GET: Kuluper
        public ActionResult Index()
        {
            var kulupler = db.Tbl_Kulup.ToList();
            return View(kulupler);
        }

        [HttpGet]
        public ActionResult KulupEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KulupEkle(Tbl_Kulup k)
        {
            db.Tbl_Kulup.Add(k);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Sil(int id)
        {
            var kulupd = db.Tbl_Kulup.Find(id);
            db.Tbl_Kulup.Remove(kulupd);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var kulup = db.Tbl_Kulup.Find(id);
            return View("Guncelle",kulup);

        }

        [HttpPost]
        public ActionResult Guncelle(Tbl_Kulup p)
        {

            var kulup = db.Tbl_Kulup.Find(p.KulupId);
            kulup.KulupAdi = p.KulupAdi;
            kulup.KulupKontejan = p.KulupKontejan;
            db.SaveChanges();
            return RedirectToAction("Index", "Kulupler");

        }
    }
}