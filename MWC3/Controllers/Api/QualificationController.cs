using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MWC3.Controllers.Api
{
    using System;

    using MWC3.Helpers;
    using MWC3.Models;

    public class QualificationController : BaseApiController
    {
        [HttpGet]
        [Route("api/country/{countryId}/qualification")]
        public List<Dropdown> GetQualifications(int countryId)
        {
            var dropDownList = new List<Dropdown>();

            try
            {
                var list = this.db.Qualifications.Where(r => r.CountryId == countryId).OrderBy(r => r.ShortName).ToList();
                dropDownList.Add(new Dropdown { Id = 0, Name = string.Empty, Selected = false });
                dropDownList.AddRange(list.Select(qualification => new Dropdown
                {
                    Id = qualification.Id,
                    Name = qualification.Name,
                    Selected = false
                }));
            }
            catch (Exception)
            {
                
                throw;
            }


            return dropDownList;
        }

        [HttpGet]
        [Route("api/country/{countryId}/region/{regionId}/qualification")]
        public List<Dropdown> GetQualifications(int countryId, int regionId)
        {
            var dropDownList = new List<Dropdown>();

            try
            {
                var list1 = this.db.Qualifications.Where(r => r.CountryId == countryId && r.RegionId == regionId).OrderBy(r => r.ShortName).ToList();
                var list2 = this.db.Qualifications.Where(d => d.CountryId == countryId && d.RegionId == 0).OrderBy(d => d.ShortName).ToList();
                var list3 = this.db.Qualifications.Where(d => d.CountryId == countryId && d.RegionId == null).OrderBy(d => d.ShortName).ToList();
                var list = list1.Union(list2).Union(list3).OrderBy(r => r.ShortName).ToList();

                dropDownList.Add(new Dropdown { Id = 0, Name = string.Empty, Selected = false });

                dropDownList.AddRange(list.Select(qualification => new Dropdown
                {
                    Id = qualification.Id,
                    Name = qualification.Name,
                    Selected = false
                }));
            }
            catch (Exception)
            {
                
                throw;
            }



            return dropDownList;
        }



        //// GET api/qualification
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/qualification/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/qualification
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/qualification/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/qualification/5
        //public void Delete(int id)
        //{
        //}
    }
}
