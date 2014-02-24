using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MWC3.Controllers.Api
{
    using MWC3.Helpers;

    public class BusinessController : BaseApiController
    {
        [HttpGet]
        [Route("api/country/{id}/business")]
        public List<Dropdown> BusinessByCountry(int id)
        {
            var dropDownList = new List<Dropdown>();
            var list = this.db.Businesses.Where(r => r.CountryId == id).OrderBy(r => r.Name);
            dropDownList.Add(new Dropdown { Id = 0, Name = string.Empty, Selected = false });

            dropDownList.AddRange(list.Select(business => new Dropdown
            {
                Id = business.Id,
                Name = business.Name + " - " + business.City,
                Selected = false
            }));

            return dropDownList;
        }
        [HttpGet]
        [Route("api/country/{id}/producer")]
        public List<Dropdown> ProducersByCountry(int id)
        {
            var dropDownList = new List<Dropdown>();
            var list = this.db.Businesses.Where(r => r.CountryId == id && r.IsProducer).OrderBy(r => r.Name);
            dropDownList.Add(new Dropdown { Id = 0, Name = string.Empty, Selected = false });

            dropDownList.AddRange(list.Select(business => new Dropdown
            {
                Id = business.Id,
                Name = business.Name + " - " + business.City,
                Selected = false
            }));

            return dropDownList;
        }


        //// GET api/business
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/business/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/business
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/business/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/business/5
        //public void Delete(int id)
        //{
        //}
    }
}
