using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MWC3.Models;
using MWC3.DAL;

namespace MWC3.Controllers
{
    public class WineTestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /WineTest/
        public ActionResult Index()
        {
            var wines = db.Wines.Include(w => w.Business).Include(w => w.Country).Include(w => w.Qualification).Include(w => w.Region).Include(w => w.WineColor);
            return View(wines.ToList());
        }

        // GET: /WineTest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wine wine = db.Wines.Find(id);
            if (wine == null)
            {
                return HttpNotFound();
            }
            return View(wine);
        }

        // GET: /WineTest/Create
        public ActionResult Create()
        {
            ViewBag.BusinessId = new SelectList(db.Businesses, "Id", "Name");
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            ViewBag.QualificationId = new SelectList(db.Qualifications, "Id", "Name");
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name");
            ViewBag.WineColorId = new SelectList(db.WineColors, "Id", "Name");
            return View();
        }

        // POST: /WineTest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,RegionId,WineColorId,CountryId,QualificationId,BusinessId,IsSparkling,IsFortified,AddedBy,TimeStamp")] Wine wine)
        {
            if (ModelState.IsValid)
            {
                db.Wines.Add(wine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusinessId = new SelectList(db.Businesses, "Id", "Name", wine.BusinessId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", wine.CountryId);
            ViewBag.QualificationId = new SelectList(db.Qualifications, "Id", "Name", wine.QualificationId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", wine.RegionId);
            ViewBag.WineColorId = new SelectList(db.WineColors, "Id", "Name", wine.WineColorId);
            return View(wine);
        }

        // GET: /WineTest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wine wine = db.Wines.Find(id);
            if (wine == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessId = new SelectList(db.Businesses, "Id", "Name", wine.BusinessId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", wine.CountryId);
            ViewBag.QualificationId = new SelectList(db.Qualifications, "Id", "Name", wine.QualificationId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", wine.RegionId);
            ViewBag.WineColorId = new SelectList(db.WineColors, "Id", "Name", wine.WineColorId);
            return View(wine);
        }

        // POST: /WineTest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,RegionId,WineColorId,CountryId,QualificationId,BusinessId,IsSparkling,IsFortified,AddedBy,TimeStamp")] Wine wine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusinessId = new SelectList(db.Businesses, "Id", "Name", wine.BusinessId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", wine.CountryId);
            ViewBag.QualificationId = new SelectList(db.Qualifications, "Id", "Name", wine.QualificationId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", wine.RegionId);
            ViewBag.WineColorId = new SelectList(db.WineColors, "Id", "Name", wine.WineColorId);
            return View(wine);
        }

        // GET: /WineTest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wine wine = db.Wines.Find(id);
            if (wine == null)
            {
                return HttpNotFound();
            }
            return View(wine);
        }

        // POST: /WineTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wine wine = db.Wines.Find(id);
            db.Wines.Remove(wine);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
