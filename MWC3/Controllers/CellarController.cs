

namespace MWC3.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;

    using MWC3.Models;
    using MWC3.Services;

    public class CellarController : BaseController
    {
        public CellarService Service { get; set; }

        public CellarController()
        {
            Service = new CellarService();
        }


        //
        // GET: /Cellar/
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult GetCurrentWines()
        {
            var userId = this.GetUserId();
            var wines = Service.GetWinesByUser(userId).Where(x => x.Stock > 0).ToList();
            return this.PartialView("Cellar/_CurrentWines", wines.ToList());
        }

        public PartialViewResult GetTransactions()
        {
            var userId = this.GetUserId();
            var transactions = Service.GetTransactionsByUserId(userId);
            return this.PartialView("Cellar/_Transactions", transactions);
        }

        public PartialViewResult GetTransactionsByWineId(int wineId)
        {
            var userId = this.GetUserId();
            var transactions = Service.GetTransactionsByUserId(userId).Where(t=>t.WineId == wineId);
            return this.PartialView("Cellar/_Transactions", transactions);
        }

        public PartialViewResult GetTransactionsIn()
        {
            var userId = this.GetUserId();
            var transactions = Service.GetTransactionsInByUserId(userId);
            
            var totalValue = transactions.Aggregate<CellarTransactionViewModel, decimal>(0, (current, cellarTransactionViewModel) => current + (cellarTransactionViewModel.Quantity * cellarTransactionViewModel.Price));
            ViewBag.TotalValue = totalValue;

            return this.PartialView("Cellar/_TransactionsIn", transactions);
        }

        public PartialViewResult GetTransactionsOut()
        {
            var userId = this.GetUserId();
            var transactions = Service.GetTransactionsOutByUserId(userId);

            var totalValue = transactions.Aggregate<CellarTransactionViewModel, decimal>(0, (current, cellarTransactionViewModel) => current + (cellarTransactionViewModel.Quantity * cellarTransactionViewModel.Price));
            ViewBag.TotalValue = totalValue;

            return this.PartialView("Cellar/_TransactionsOut", transactions);
        }


        public PartialViewResult AddTransactionIn()
        {
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

            return this.PartialView("Cellar/_AddTransactionIn", transaction);
        }

        public PartialViewResult AddTransactionOut()
        {
            return this.PartialView("Cellar/_AddTransactionIn");
        }

        [HttpPost]
        public PartialViewResult SaveTransaction()
        {
            return this.PartialView("Cellar/_SaveTransaction");
        }

        public PartialViewResult TransactionDetails(int transactionId)
        {
            var transaction = Service.GetTransactionById(transactionId);

            // check if this transaction belongs to this user
            var userId = this.GetUserId();
            if (transaction.UserId != userId)
            {
                transaction = new Transaction();
            }

            return this.PartialView("Cellar/_TransactionDetails", transaction);
        }
        ////
        //// GET: /Cellar/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        ////
        //// GET: /Cellar/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        ////
        //// POST: /Cellar/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Cellar/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Cellar/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Cellar/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Cellar/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

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
