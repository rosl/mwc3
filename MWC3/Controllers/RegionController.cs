using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MWC3.Models;

namespace MWC3.Controllers
{
    using System.Security.Cryptography.X509Certificates;

    public class RegionController : BaseController
    {
        // GET: /Region/
        public ActionResult Index()
        {
            var regions = this.Db.Regions.Include(r => r.Country);
            return View(regions.ToList());
        }

        // GET: /Region/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = this.Db.Regions.Find(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // GET: /Region/Create
        public ActionResult Create()
        {
            this.PopulateCountryList();
            return View();
        }

        // POST: /Region/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,AddedBy,Timestamp,CountryId")] Region region)
        {
            if (ModelState.IsValid)
            {
                this.Db.Regions.Add(region);
                this.Db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(this.Db.Countries, "Id", "Name", region.CountryId);
            return View(region);
        }

        // GET: /Region/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = this.Db.Regions.Find(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(this.Db.Countries, "Id", "Name", region.CountryId);
            return View(region);
        }

        // POST: /Region/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,AddedBy,Timestamp,CountryId")] Region region)
        {
            if (ModelState.IsValid)
            {
                this.Db.Entry(region).State = EntityState.Modified;
                this.Db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(this.Db.Countries, "Id", "Name", region.CountryId);
            return View(region);
        }

        // GET: /Region/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = this.Db.Regions.Find(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // POST: /Region/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Region region = this.Db.Regions.Find(id);
            this.Db.Regions.Remove(region);
            this.Db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetRegions(int countryId)
        {
            var list = this.Db.Regions.Where(r => r.CountryId == countryId).OrderBy(r => r.Name).ToList();
            list.Insert(0, new Region{Name = string.Empty});
            return this.Json(list.Select(l => new {Selected = false, Value = l.Id, Text = l.Name }), JsonRequestBehavior.AllowGet);
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
