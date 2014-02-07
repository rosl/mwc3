namespace MWC3.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    public class SearchController : BaseController
    {
        //
        // GET: /Search/
        [HttpGet]
        [Route("search/distributor")]
        public JsonResult Distributor()
        {
            var list = this.Db.Businesses.Where(b=>b.IsDistributor).ToList();
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
            return this.Json(list.Where(l => l.Business.IsProducer).Select(l => new { Key = l.Id, Value = l.Name + " - " + l.Business.Name + " - " + l.Business.City }), JsonRequestBehavior.AllowGet);
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
                            isFortified = wine.IsFortified,
                            isSparkling = wine.IsSparkling,
                            producer = wine.Business,
                            grapes = wine.Grapes.Select(g => g.Name),
                            color = wine.WineColor.Name
                        },
                        JsonRequestBehavior.AllowGet);
            }

            return this.Json(null, JsonRequestBehavior.AllowGet);

        }
    }
}
