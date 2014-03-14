namespace MWC3.Controllers
{
    using MWC3.Models;
    using System;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    [Authorize]
    public class TransactionController : BaseController
    {
        // GET: /Transaction/
        [Route("Transaction")]
        [Route("Transaction/Index")]
        public ActionResult Index()
        {
            var userId = this.GetUserId();
            var transactions = Db.Transactions.Include(t => t.BottleType).Include(t => t.Business).Include(t => t.TransactionType).Include(t => t.Wine).Where(x=>x.UserId == userId);
            this.PopulateTransactionTypesList();
            return View(transactions.ToList());
        }

        [Route("Transaction/Type/{id}")]
        public ActionResult Index(int id)
        {
            
            var userId = this.GetUserId();
            IQueryable<Transaction> transactions;

            if (id == 0)
            {
                 transactions = Db.Transactions.Include(t => t.BottleType).Include(t => t.Business).Include(t => t.TransactionType).Include(t => t.Wine).Where(x => x.UserId == userId);
            }
            else
            {
                transactions = Db.Transactions.Include(t => t.BottleType).Include(t => t.Business).Include(t => t.TransactionType).Include(t => t.Wine).Where(x => x.UserId == userId && x.TransactionTypeId == id);    
            }

            this.PopulateTransactionTypesList(id);

            return View(transactions.ToList());
        }

        // GET: /Transaction/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Transaction transaction = Db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: /Transaction/CreateIn
        public ActionResult CreateIn()
        {
            // set default values
            var transaction = new Transaction
                              {
                                  BottleTypeId = 3,
                                  Year = DateTime.Now.Year - 1,
                                  Date = DateTime.Now.Date,
                                  Quantity = 1
                              };

            // ViewBag.BusinessId = new SelectList(Db.Businesses, "Id", "Name");
            // ViewBag.WineId = new SelectList(Db.Wines, "Id", "Name");
            ViewBag.PriceInt = Math.Floor(transaction.Price);
            var priceCents = ("0"
                              + (transaction.Price - Math.Floor(transaction.Price) * 100).ToString(
                                  CultureInfo.InvariantCulture));
            ViewBag.PriceCents = priceCents.Substring(priceCents.Length - 2);

            this.PopulateBottleTypeList();
            this.PopulateTransactionTypesList(true);
            this.PopulateYearList();

            return View(transaction);
        }

        // GET: /Transaction/CreateOut
        public ActionResult CreateOut()
        {
            // set default values
            var transaction = new Transaction
            {
                BottleTypeId = 3,
                Year = DateTime.Now.Year - 1,
                Date = DateTime.Now.Date,
                Quantity = 1
            };

            // ViewBag.BusinessId = new SelectList(Db.Businesses, "Id", "Name");
            // ViewBag.WineId = new SelectList(Db.Wines, "Id", "Name");

            this.PopulateBottleTypeList();
            this.PopulateTransactionTypesList(false);
            this.PopulateYearList();

            return View(transaction);
        }


        // POST: /Transaction/CreateIn
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateIn([Bind(Include="Id,Quantity,TransactionTypeId,BusinessId,BottleTypeId,WineId,Year,Price,Date,Comment")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {

                transaction.UserId = this.GetUserId();
                transaction.AddedBy = this.GetUserName();
                transaction.TimeStamp = DateTime.Now;

                Db.Transactions.Add(transaction);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }

            this.PopulateBottleTypeList();
            this.PopulateTransactionTypesList(false);
            this.PopulateYearList();

            return View(transaction);
        }

        // POST: /Transaction/CreateOut
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOut([Bind(Include = "Id,Quantity,TransactionTypeId,TransactionId, Date,Comment")] Transaction transaction)
        {
            var transactionId = Request.Form["TransactionId"];

            if (ModelState.IsValid)
            {
                var myInTransaction = this.Db.Transactions.Find(Convert.ToInt32(transactionId));
                if (myInTransaction != null)
                {
                    transaction.WineId = myInTransaction.WineId;
                    transaction.Year = myInTransaction.Year;
                    transaction.BusinessId = myInTransaction.BusinessId;
                    transaction.Price = myInTransaction.Price;
                    transaction.TotalPrice = myInTransaction.Price * transaction.Quantity;
                    transaction.Alcohol = myInTransaction.Alcohol;
                    transaction.UserId = this.GetUserId();
                    transaction.AddedBy = this.GetUserName();
                    transaction.TimeStamp = DateTime.Now;
                    transaction.BottleTypeId = myInTransaction.BottleTypeId;
                    Db.Transactions.Add(transaction);
                    Db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.BottleTypeId = new SelectList(Db.BottleTypes, "Id", "Name", transaction.BottleTypeId);
            ViewBag.BusinessId = new SelectList(Db.Businesses, "Id", "Name", transaction.BusinessId);
            ViewBag.TransactionTypeId = new SelectList(Db.TransactionTypes, "Id", "Name", transaction.TransactionTypeId);
            ViewBag.WineId = new SelectList(Db.Wines, "Id", "Name", transaction.WineId);
            return View(transaction);
        }

        // GET: /Transaction/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = Db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.BottleTypeId = new SelectList(Db.BottleTypes, "Id", "Name", transaction.BottleTypeId);
            ViewBag.BusinessId = new SelectList(Db.Businesses, "Id", "Name", transaction.BusinessId);
            ViewBag.TransactionTypeId = new SelectList(Db.TransactionTypes, "Id", "Name", transaction.TransactionTypeId);
            ViewBag.WineId = new SelectList(Db.Wines, "Id", "Name", transaction.WineId);
            return View(transaction);
        }

        // POST: /Transaction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Quantity,TransactionTypeId,UserId,BusinessId,BottleTypeId,WineId,Year,Price,Date,AddedBy,TimeStamp,Alcohol,Comment")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.AddedBy = this.GetUserName();
                transaction.TimeStamp = DateTime.Now;

                Db.Entry(transaction).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BottleTypeId = new SelectList(Db.BottleTypes, "Id", "Name", transaction.BottleTypeId);
            ViewBag.BusinessId = new SelectList(Db.Businesses, "Id", "Name", transaction.BusinessId);
            ViewBag.TransactionTypeId = new SelectList(Db.TransactionTypes, "Id", "Name", transaction.TransactionTypeId);
            ViewBag.WineId = new SelectList(Db.Wines, "Id", "Name", transaction.WineId);
            return View(transaction);
        }

        // GET: /Transaction/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = Db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: /Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = Db.Transactions.Find(id);
            Db.Transactions.Remove(transaction);
            Db.SaveChanges();
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
