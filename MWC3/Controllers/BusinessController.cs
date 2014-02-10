namespace MWC3.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using MWC3.Models;
    public class BusinessController : BaseController
    {
        // GET: /Business/
        public ActionResult Index()
        {
            var businesses = this.Db.Businesses.Include(b => b.Country).OrderBy(b => b.Name);
            return View(businesses.ToList());
        }

        // GET: /Business/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = this.Db.Businesses.Include(r => r.Country).FirstOrDefault(x => x.Id == id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // GET: /Business/Create
        public ActionResult Create()
        {
            // ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            this.PopulateCountryList();
            return View();
        }

        // POST: /Business/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Url,Phone,IsDistributor,IsProducer,Address,PostalCode,City,CountryId,Owner")] Business business)
        {
            if (ModelState.IsValid)
            {
                business.AddedBy = this.GetUserName();
                business.Timestamp = DateTime.Now;

                this.Db.Businesses.Add(business);
                this.Db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(this.Db.Countries, "Id", "Name", business.CountryId);
            return View(business);
        }

        // GET: /Business/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = this.Db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            this.PopulateCountryList();
            return View(business);
        }

        // POST: /Business/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Url,Phone,IsDistributor,IsProducer,Address,PostalCode,City,CountryId,Owner")] Business business)
        {
            if (ModelState.IsValid)
            {
                business.AddedBy = this.GetUserName();
                business.Timestamp = DateTime.Now;

                this.Db.Entry(business).State = EntityState.Modified;
                this.Db.SaveChanges();
                return RedirectToAction("Index");
            }
            this.PopulateCountryList();
            return View(business);
        }

        // GET: /Business/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = this.Db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // POST: /Business/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Business business = this.Db.Businesses.Find(id);
            this.Db.Businesses.Remove(business);
            this.Db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetProducers(int countryId)
        {
            var list = this.Db.Businesses.Where(b => b.CountryId == countryId && b.IsProducer).ToList();
            var selectList = list.Select(l => new { Selected = false, Value = l.Id, Text = l.Name }).ToList();
            selectList.Add(new { Selected = false, Value = 0, Text = "" });
            return this.Json(selectList.OrderBy(x => x.Text), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDistributors(int countryId)
        {
            var list = this.Db.Businesses.Where(b => b.CountryId == countryId && b.IsDistributor);
            return this.Json(list.Select(l => new { Selected = false, Value = l.Id, Text = l.Name + " - " + l.City }), JsonRequestBehavior.AllowGet);
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
