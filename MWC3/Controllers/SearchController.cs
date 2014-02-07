namespace MWC3.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    public class SearchController : BaseController
    {
        //
        // GET: /Search/
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
