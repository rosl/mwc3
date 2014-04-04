namespace MWC3.Controllers.Api
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Http;

    using Microsoft.Ajax.Utilities;

    using MWC3.Models;
    using MWC3.Services;

    public class CellarController : BaseApiController
    {
        

        [HttpGet]
        [Route("api/cellar/{userId}/currentwines")]
        public List<CellarWineViewModel> CurrentWinesByUser(string userId)
        {
            var service = new CellarService();
            return service.GetWinesByUser(userId).Where(x => x.Stock > 0).ToList();
        }

        [HttpGet]
        [Route("api/cellar/{userId}/wines")]
        public List<CellarWineViewModel> AllWinesByUser(string userId)
        {
            var service = new CellarService();
            return service.GetWinesByUser(userId).ToList();
        }






    }
}
