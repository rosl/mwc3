namespace MWC3.Controllers.Api
{
    using System.Web.Http;

    using MWC3.DAL;

    public class BaseApiController : ApiController
    {
        
        public ApplicationDbContext db = new ApplicationDbContext();
        
    }
}
