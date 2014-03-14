namespace MWC3.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using MWC3.Models;
    public class BottleTypeController : BaseController
    {

        // GET: /BottleType/
        public ActionResult Index()
        {
            return View(this.Db.BottleTypes.ToList());
        }

        // GET: /BottleType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BottleType bottletype = this.Db.BottleTypes.Find(id);
            if (bottletype == null)
            {
                return HttpNotFound();
            }
            return View(bottletype);
        }

        // GET: /BottleType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /BottleType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,Capacity")] BottleType bottletype)
        {
            if (ModelState.IsValid)
            {
                bottletype.AddedBy = this.GetUserName();
                bottletype.Timestamp = DateTime.Now;
                this.Db.BottleTypes.Add(bottletype);
                this.Db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bottletype);
        }

        // GET: /BottleType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BottleType bottletype = this.Db.BottleTypes.Find(id);
            if (bottletype == null)
            {
                return HttpNotFound();
            }
            return View(bottletype);
        }

        // POST: /BottleType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,Capacity")] BottleType bottletype)
        {
            if (ModelState.IsValid)
            {
                bottletype.AddedBy = this.GetUserName();
                bottletype.Timestamp = DateTime.Now;
                this.Db.Entry(bottletype).State = EntityState.Modified;
                this.Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bottletype);
        }

        // GET: /BottleType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BottleType bottletype = this.Db.BottleTypes.Find(id);
            if (bottletype == null)
            {
                return HttpNotFound();
            }
            return View(bottletype);
        }

        // POST: /BottleType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // todo: check if bottletype exists in transactions


            BottleType bottletype = this.Db.BottleTypes.Find(id);
            this.Db.BottleTypes.Remove(bottletype);
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
