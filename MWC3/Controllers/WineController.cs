namespace MWC3.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using MWC3.Models;

    public class WineController : BaseController
    {
        // GET: /Wine/
        public ActionResult Index()
        {
            var wines = this.Db.Wines.Include(w => w.Business).Include(w => w.Country).Include(w => w.Qualification).Include(w => w.Region).Include(w => w.WineColor);
            return View(wines.ToList());
        }

        // GET: /Wine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wine wine = this.Db.Wines.Find(id);
            if (wine == null)
            {
                return HttpNotFound();
            }
            return View(wine);
        }

        // GET: /Wine/Create
        public ActionResult Create()
        {
            var emptyList = new List<string>();

            ViewData["Regions"] = new SelectList(emptyList, "Id", "Name");
            ViewData["Qualifications"] = new SelectList(emptyList, "Id", "Name");

            this.PopulateCountryList();
            this.PopulateGrapeList();
            this.PopulateWineColorList();
            this.PopulateBusinessList();

            return View();
        }

        // POST: /Wine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,RegionId,WineColorId,CountryId,QualificationId,IsSparkling,IsFortified,BusinessId,GrapeIds")] Wine wine)
        {
            if (ModelState.IsValid)
            {
                wine.AddedBy = this.GetUserName();
                wine.TimeStamp = DateTime.Now;

                if (wine.GrapeIds.Count() > 0)
                {
                   wine.Grapes = new Collection<Grape>();
                   foreach (var grapeId in wine.GrapeIds)
                   {
                       var grape = Db.Grapes.Find(grapeId);
                       wine.Grapes.Add(grape);
                   }
                }
                
                this.Db.Wines.Add(wine);
                this.Db.SaveChanges();
                return RedirectToAction("Index");
            }

            this.PopulateCountryList();
            this.PopulateGrapeList();
            this.PopulateWineColorList();
            this.PopulateBusinessList();
            this.PopulateQualificationList(wine.CountryId);
            this.PopulateRegionList(wine.CountryId);

            return View(wine);
        }

        // GET: /Wine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wine wine = this.Db.Wines.Find(id);
            if (wine == null)
            {
                return HttpNotFound();
            }

            if (wine.Grapes.Count > 0)
            {
                wine.GrapeIds = new int[wine.Grapes.Count];
                var counter = 0;
                foreach (Grape grape in wine.Grapes)
                {
                    wine.GrapeIds[counter] = grape.Id;
                    counter++;
                }
            }

            this.PopulateCountryList();
            this.PopulateGrapeList();
            this.PopulateWineColorList();
            this.PopulateBusinessList();
            this.PopulateQualificationList(wine.CountryId);
            this.PopulateRegionList(wine.CountryId);

            return View(wine);
        }

        // POST: /Wine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,RegionId,WineColorId,CountryId,QualificationId,IsSparkling,IsFortified,BusinessId,GrapeIds")] Wine wine)
        {
            if (ModelState.IsValid)
            {
                var myWine = this.Db.Wines.Find(wine.Id);
                
                wine.AddedBy = this.GetUserName();
                wine.TimeStamp = DateTime.Now;

                if (wine.GrapeIds.Count() > 0)
                {
                    myWine.Grapes = new Collection<Grape>();
                    foreach (var grapeId in wine.GrapeIds)
                    {
                        // find grape
                        var grape = Db.Grapes.Find(grapeId);
                        // attach found grape to current context
                        Db.Grapes.Attach(grape);
                        // add grape to found wine
                        myWine.Grapes.Add(grape);
                    }
                }

                // copy modelview wine values to found wine from db
                this.Db.Entry(myWine).CurrentValues.SetValues(wine);
                this.Db.Entry(myWine).State = EntityState.Modified;
                this.Db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusinessId = new SelectList(this.Db.Businesses, "Id", "Name", wine.BusinessId);
            ViewBag.CountryId = new SelectList(this.Db.Countries, "Id", "Name", wine.CountryId);
            ViewBag.QualificationId = new SelectList(this.Db.Qualifications, "Id", "Name", wine.QualificationId);
            ViewBag.RegionId = new SelectList(this.Db.Regions, "Id", "Name", wine.RegionId);
            ViewBag.WineColorId = new SelectList(this.Db.WineColors, "Id", "Name", wine.WineColorId);
            return View(wine);
        }

        // GET: /Wine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wine wine = this.Db.Wines.Find(id);
            if (wine == null)
            {
                return HttpNotFound();
            }
            return View(wine);
        }

        // POST: /Wine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wine wine = this.Db.Wines.Find(id);
            this.Db.Wines.Remove(wine);
            this.Db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
