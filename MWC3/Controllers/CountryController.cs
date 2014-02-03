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
    public class CountryController : BaseController
    {
        // GET: /Country/
        public ActionResult Index()
        {
            return View(this.Db.Countries.ToList());
        }

        // GET: /Country/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = this.Db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }

            if (country.ParentId > 0)
            {
                country.ParentCountry = this.Db.Countries.Find(country.ParentId);
            }

            return View(country);
        }

        // GET: /Country/Create
        public ActionResult Create()
        {
            this.PopulateParentCountryList();
            this.PopulateEnabledLanguages();
            return View();
        }

        // POST: /Country/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,LanguageCode,ParentId,AddedBy,Timestamp")] Country country)
        {
            if (ModelState.IsValid)
            {
                country.Timestamp = DateTime.Now;
                country.AddedBy = this.GetUserName();
                this.Db.Countries.Add(country);
                this.Db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(country);
        }

        // GET: /Country/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = this.Db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }

            this.PopulateParentCountryList();
            this.PopulateEnabledLanguages();

            return View(country);
        }

        // POST: /Country/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,LanguageCode,ParentId,AddedBy,Timestamp")] Country country)
        {
            if (ModelState.IsValid)
            {
                country.Timestamp = DateTime.Now;
                country.AddedBy = this.GetUserName();
                this.Db.Entry(country).State = EntityState.Modified;
                this.Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(country);
        }

        // GET: /Country/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = this.Db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }

            if (country.ParentId > 0)
            {
                country.ParentCountry = this.Db.Countries.Find(country.ParentId);
            }

            return View(country);
        }

        // POST: /Country/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // check if child countries exist
            if (this.Db.Countries.Any(g => g.ParentId == id))
            {
                return HttpNotFound();
            }

            // check if region is coupled
            if (this.Db.Regions.Any(g => g.CountryId == id))
            {
                return HttpNotFound();
            }

            Country country = this.Db.Countries.Find(id);
            this.Db.Countries.Remove(country);
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
