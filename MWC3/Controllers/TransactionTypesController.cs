using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MWC3.Models;

namespace MWC3.Controllers
{
    using System;

    public class TransactionTypesController : BaseController
    {
        // GET: /TransactionTypes/
        public ActionResult Index()
        {
            return View(Db.TransactionTypes.ToList());
        }

        // GET: /TransactionTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionType transactiontype = Db.TransactionTypes.Find(id);
            if (transactiontype == null)
            {
                return HttpNotFound();
            }
            return View(transactiontype);
        }

        // GET: /TransactionTypes/Create
        public ActionResult Create()
        {
            this.PopulateEnabledLanguages();
            this.PopulateMultiplierList();
            this.PopulateParentMultiplierList();
            return View();
        }

        // POST: /TransactionTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,Multiplier,LanguageCode,ParentId,AddedBy,Timestamp")] TransactionType transactiontype)
        {
            if (ModelState.IsValid)
            {
                if (transactiontype.ParentId > 0)
                {
                    var parent = this.Db.TransactionTypes.Find(transactiontype.ParentId);
                    if (parent == null)
                    {
                        return HttpNotFound();
                    }
                    transactiontype.Multiplier = parent.Multiplier;
                }
                transactiontype.Timestamp = DateTime.Now;
                transactiontype.AddedBy = this.GetUserName();
                Db.TransactionTypes.Add(transactiontype);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transactiontype);
        }

        // GET: /TransactionTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionType transactiontype = Db.TransactionTypes.Find(id);
            if (transactiontype == null)
            {
                return HttpNotFound();
            }

            this.PopulateEnabledLanguages();
            this.PopulateMultiplierList();
            this.PopulateParentMultiplierList();

            return View(transactiontype);
        }

        // POST: /TransactionTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,Multiplier,LanguageCode,ParentId,AddedBy,Timestamp")] TransactionType transactiontype)
        {
            if (ModelState.IsValid)
            {
                if (transactiontype.ParentId > 0)
                {
                    var parent = this.Db.TransactionTypes.Find(transactiontype.ParentId);
                    if (parent == null)
                    {
                        return HttpNotFound();
                    }
                    transactiontype.Multiplier = parent.Multiplier;
                }
                transactiontype.Timestamp = DateTime.Now;
                transactiontype.AddedBy = this.GetUserName();
                Db.TransactionTypes.Add(transactiontype);
                Db.Entry(transactiontype).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transactiontype);
        }

        // GET: /TransactionTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionType transactiontype = Db.TransactionTypes.Find(id);
            if (transactiontype == null)
            {
                return HttpNotFound();
            }
            return View(transactiontype);
        }

        // POST: /TransactionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //TransactionType transactiontype = Db.TransactionTypes.Find(id);
            //Db.TransactionTypes.Remove(transactiontype);
            //Db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
