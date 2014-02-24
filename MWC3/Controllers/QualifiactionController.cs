namespace MWC3.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using MWC3.Models;

    public class QualifiactionController : BaseController
    {
        // GET: /Qualifiaction/
        [Route("Qualifiaction")]
        [Route("Qualifiaction/Index")]
        public ActionResult Index()
        {
            this.PopulateCountryList();
            var qualifications = this.Db.Qualifications;
            return View(qualifications.ToList());
        }
        [Route("Qualifiaction/{id}")]
        public ActionResult Index(int id)
        {
            // var qualifications = db.Qualifications.Include(q => q.Country).Include(q => q.Region);
            this.PopulateCountryList(id);
            var qualifications = this.Db.Qualifications.Where(x=>x.CountryId == id);
            return View(qualifications.ToList());
        }

        // GET: /Qualifiaction/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Qualification qualification = this.Db.Qualifications.Find(id);
            if (qualification == null)
            {
                return HttpNotFound();
            }
            return View(qualification);
        }

        // GET: /Qualifiaction/Create
        [HttpGet]
        [Route("Qualifiaction/Create")]
        public ActionResult Create()
        {
            var qualification = new Qualification { AddedBy = this.GetUserName(), Timestamp = DateTime.Now };
            ViewBag.RegionId = new SelectList(new List<string>());
            this.PopulateCountryList();
            return View(qualification);
        }

        // POST: /Qualifiaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Qualifiaction/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ShortName,CountryId,RegionId")] Qualification qualification)
        {
            if (ModelState.IsValid)
            {
                qualification.AddedBy = this.GetUserName();
                qualification.Timestamp = DateTime.Now;

                this.Db.Qualifications.Add(qualification);
                this.Db.SaveChanges();
                return RedirectToAction(qualification.CountryId.ToString(CultureInfo.InvariantCulture));
            }

            ViewBag.CountryId = new SelectList(this.Db.Countries, "Id", "Name", qualification.CountryId);
            ViewBag.RegionId = new SelectList(this.Db.Regions, "Id", "Name", qualification.RegionId);
            return View(qualification);
        }

        // GET: /Qualifiaction/Edit/5
        [HttpGet]
        [Route("Qualifiaction/Edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Qualification qualification = this.Db.Qualifications.Find(id);
            if (qualification == null)
            {
                return HttpNotFound();
            }
            // ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", qualification.CountryId);
            this.PopulateCountryList();
            ViewBag.RegionId = new SelectList(this.Db.Regions.Where(r => r.CountryId == qualification.CountryId).OrderBy(r => r.Name), "Id", "Name", qualification.RegionId);
            return View(qualification);
        }

        // POST: /Qualifiaction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Qualifiaction/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ShortName,CountryId,RegionId")] Qualification qualification)
        {
            if (ModelState.IsValid)
            {
                qualification.AddedBy = this.GetUserName();
                qualification.Timestamp = DateTime.Now;

                this.Db.Entry(qualification).State = EntityState.Modified;
                this.Db.SaveChanges();
                return RedirectToAction(qualification.CountryId.ToString(CultureInfo.InvariantCulture));
            }
            ViewBag.CountryId = new SelectList(this.Db.Countries, "Id", "Name", qualification.CountryId);
            ViewBag.RegionId = new SelectList(this.Db.Regions, "Id", "Name", qualification.RegionId);
            return View(qualification);
        }

        // GET: /Qualifiaction/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Qualification qualification = this.Db.Qualifications.Find(id);
            if (qualification == null)
            {
                return HttpNotFound();
            }
            return View(qualification);
        }

        // POST: /Qualifiaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Qualification qualification = this.Db.Qualifications.Find(id);
            this.Db.Qualifications.Remove(qualification);
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

        //[Route("Qualifiaction/GetQualifications")]
        //public JsonResult GetQualifications(int countryId, int? regionId)
        //{
        //    List<Qualification> list;

        //    if (regionId == null || regionId == 0)
        //    {
        //        list = this.Db.Qualifications.Where(r => r.CountryId == countryId).OrderBy(r => r.ShortName).ToList();
        //    }
        //    else
        //    {
        //        var list1 = this.Db.Qualifications.Where(r => r.CountryId == countryId && r.RegionId == regionId).OrderBy(r => r.ShortName).ToList();
        //        var list2 = this.Db.Qualifications.Where(d => d.CountryId == countryId && d.RegionId == 0).OrderBy(d => d.ShortName).ToList();
        //        var list3 = this.Db.Qualifications.Where(d => d.CountryId == countryId && d.RegionId == null).OrderBy(d => d.ShortName).ToList();
        //        list = list1.Union(list2).Union(list3).OrderBy(r=>r.ShortName).ToList();
        //    }

        //    return this.Json(list.Select(l => new { Selected = false, Value = l.Id, Text = l.Name }), JsonRequestBehavior.AllowGet);
        //}
    }
}
