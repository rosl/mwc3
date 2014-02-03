namespace MWC3.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    public class SearchController : BaseController
    {
        //
        // GET: /Search/
        public JsonResult Wine()
        {
            var list = this.Db.Wines.ToList();
            return this.Json(list.Select(l => new { Value = l.Id, Text = l.Name + " - " + l.Business.Name }), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult Wine(string searchText)
        //{
        //    var list = this.Db.Wines.Where(w => w.Name.Contains(searchText) || (w.Business.Name.Contains(searchText) && w.Business.IsProducer)).ToList();
        //    return this.Json(list.Select(l => new { Selected = false, Value = l.Id, Text = l.Name + " - " + l.Business.Name }), JsonRequestBehavior.AllowGet);
        //}

    }
}
