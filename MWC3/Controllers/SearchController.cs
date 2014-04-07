namespace MWC3.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using MWC3.Models;

    public class SearchController : BaseController
    {
        //
        // GET: /Search/
        [HttpGet]
        [Route("search/distributor")]
        public JsonResult Distributor()
        {
            var list = this.Db.Businesses.Where(b => b.IsDistributor).ToList();
            return this.Json(list.Select(l => new { Key = l.Id, Value = l.Name + " - " + l.City }), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("search/distributor/{id}")]
        public JsonResult Distributor(int id)
        {
            var bus = this.Db.Businesses.Find(id);
            if (bus != null && bus.IsDistributor)
            {
                return
                    this.Json(
                        new
                        {
                            id = bus.Id,
                            name = bus.Name,
                            address = bus.Address,
                            city = bus.City
                        },
                        JsonRequestBehavior.AllowGet);
            }
            return this.Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("search/wine")]
        public JsonResult Wine()
        {
            var list = this.Db.Wines.ToList();
            var jsonList = list.Where(l => l.Business.IsProducer).Select(l => new { Key = l.Id, Value = l.Name + " - " + l.Business.Name + " - " + l.Business.City });
            return this.Json(jsonList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("search/wine/{id}")]
        public JsonResult Wine(int id)
        {
            var wine = this.Db.Wines.Find(id);

            if (wine != null)
            {
                return
                    this.Json(
                        new
                        {
                            id = wine.Id,
                            name = wine.Name,
                            isFortified = wine.IsFortified? Resources.Common.Yes: Resources.Common.No,
                            isSparkling = wine.IsSparkling ? Resources.Common.Yes : Resources.Common.No,
                            producer = wine.Business,
                            grapes = wine.Grapes.Select(g => g.Name),
                            color = wine.WineColor.Name
                        },
                        JsonRequestBehavior.AllowGet);
            }

            return this.Json(null, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        [Route("search/transaction/user/{userId}")]
        public JsonResult Transaction(string userId)
        {
            var list = this.Db.Transactions.Where(t => t.UserId == userId && t.TransactionType.Multiplier > 0).OrderBy(t => t.WineId).ThenBy(t => t.Year).ToList();
            var distinctList = list.Distinct(new DistinctTransactionComparer());
            var jsonList =
                distinctList.Select(
                    t =>
                        new
                        {
                            Key = t.Id,
                            Value = t.Wine.Name + " - " + t.Year + " - " + t.Wine.Business.Name + " - " + t.Wine.Business.City
                        });
            return this.Json(jsonList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("search/transaction/{id}")]
        public JsonResult Transaction(int id)
        {
            var transaction = this.Db.Transactions.Find(id);

            if (transaction != null)
            {
                return
                    this.Json(
                        new
                        {
                            id = transaction.Id,
                            wineId = transaction.Wine.Id,
                            wineName = transaction.Wine.Name,
                            wineIsFortified = transaction.Wine.IsFortified,
                            wineIsSparkling = transaction.Wine.IsSparkling,
                            producer = transaction.Wine.Business,
                            grapes = transaction.Wine.Grapes.Select(g => g.Name),
                            color = transaction.Wine.WineColor.Name,
                            distributor = transaction.Business
                        },
                        JsonRequestBehavior.AllowGet);
            }

            return this.Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("search/country/{id}/region")]
        public JsonResult RegionByCountry(int id)
        {
            var list = this.Db.Regions.Where(r => r.CountryId == id).OrderBy(r => r.Name).ToList();
            list.Insert(0, new Region { Name = string.Empty });
            return this.Json(list.Select(l => new { Selected = false, Value = l.Id, Text = l.Name }), JsonRequestBehavior.AllowGet);
        }
    }



    class DistinctTransactionComparer : IEqualityComparer<Transaction>
    {

        public bool Equals(Transaction x, Transaction y)
        {
            return x.WineId == y.WineId && x.Year == y.Year;
        }

        public int GetHashCode(Transaction obj)
        {
            return obj.WineId.GetHashCode() ^ obj.Year.GetHashCode();
        }
    }
}
