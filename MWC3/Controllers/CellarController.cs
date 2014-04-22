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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveTransaction([Bind(Include = "Id,Quantity,TransactionTypeId,BusinessId,BottleTypeId,WineId,Year,Price,Date,Comment")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {

                transaction.UserId = this.GetUserId();
                transaction.AddedBy = this.GetUserName();
                transaction.TimeStamp = DateTime.Now;

                Db.Transactions.Add(transaction);
                Db.SaveChanges();
                return this.Json(true);
            }

            return this.Json(false);
        }

        public PartialViewResult AddTransactionOut()
        {
            return this.PartialView("Cellar/_AddTransactionIn");
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
