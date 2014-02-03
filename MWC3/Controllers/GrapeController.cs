namespace MWC3.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using MWC3.Models;

    using WebGrease.Css.Extensions;

    public class GrapeController : BaseController
    {

        // GET: /Grape/
        public ActionResult Index()
        {
            return View(this.Db.Grapes.ToList());
        }

        // GET: /Grape/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grape grape = this.Db.Grapes.Find(id);
            if (grape == null)
            {
                return HttpNotFound();
            }

            if (grape.ParentId > 0)
            {
                grape.ParentGrape = this.Db.Grapes.Find(grape.ParentId);
            }

            grape.ChildGrapes = this.Db.Grapes.Where(c => c.ParentId == id).ToList();

            grape.GrapeColor = this.GetGrapeColor(grape.ColorId);

            return View(grape);
        }

        // GET: /Grape/Create
        public ActionResult Create()
        {
            this.PopulateColorList();
            this.PopulateParentGrapeList();
            this.PopulateUserName();
            return View();
        }

        // POST: /Grape/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ParentId,ColorId,AddedBy,TimeStamp")] Grape grape)
        {
            if (ModelState.IsValid)
            {
                // check if color is the same as parent grape
                if (grape.ParentId > 0)
                {
                    var parentGrape = this.Db.Grapes.Find(grape.ParentId);
                    if (parentGrape == null)
                    {
                        return HttpNotFound();
                    }
                    grape.ColorId = parentGrape.ColorId;
                }
                grape.TimeStamp = DateTime.Now;
                grape.AddedBy = this.GetUserName();
                this.Db.Grapes.Add(grape);
                this.Db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grape);
        }

        // GET: /Grape/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grape grape = this.Db.Grapes.Find(id);
            if (grape == null)
            {
                return HttpNotFound();
            }

            this.PopulateColorList();
            this.PopulateParentGrapeList();
            this.PopulateUserName();
            return View(grape);
        }

        // POST: /Grape/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ParentId,ColorId,AddedBy,TimeStamp")] Grape grape)
        {
            if (ModelState.IsValid)
            {
                // check if color is the same as parent grape
                if (grape.ParentId > 0)
                {
                    var parentGrape = this.Db.Grapes.Find(grape.ParentId);
                    if (parentGrape == null)
                    {
                        return HttpNotFound();
                    }
                    grape.ColorId = parentGrape.ColorId;
                }
                else
                {
                    // give childgrapes the same color as parentgrape
                    var childGrapes = this.Db.Grapes.Where(g => g.ParentId == grape.Id);
                    if (childGrapes.Any())
                    {
                        childGrapes.ForEach(g=>g.ColorId = grape.ColorId);
                    }
                }

                grape.TimeStamp = DateTime.Now;
                grape.AddedBy = this.GetUserName();
                this.Db.Entry(grape).State = EntityState.Modified;
                this.Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grape);
        }

        // GET: /Grape/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grape grape = this.Db.Grapes.Find(id);
            if (grape == null)
            {
                return HttpNotFound();
            }

            if (grape.ParentId > 0)
            {
                grape.ParentGrape = this.Db.Grapes.Find(grape.ParentId);
            }

            grape.ChildGrapes = this.Db.Grapes.Where(c => c.ParentId == id).ToList();

            grape.GrapeColor = this.GetGrapeColor(grape.ColorId);

            return View(grape);
        }

        // POST: /Grape/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // check if child grapes exists
            if (this.Db.Grapes.Any(g => g.ParentId == id))
            {
                return HttpNotFound();
            }

            Grape grape = this.Db.Grapes.Find(id);
            this.Db.Grapes.Remove(grape);
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





        private GrapeColor GetGrapeColor(int? id)
        {
            // todo : make multi culti
            // var languages = this.Request.UserLanguages;

            return this.Db.GrapeColors.FirstOrDefault(c => c.ColorId == id && c.LanguageCode == "en");
        }

    }
}
