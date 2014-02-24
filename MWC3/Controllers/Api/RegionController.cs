using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MWC3.Models;
using MWC3.DAL;

namespace MWC3.Controllers.Api
{
    using System.Web.Mvc;

    using MWC3.Helpers;

    public class RegionController : BaseApiController
    {
        // GET api/Region
        //public List<Region> GetRegions()
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    return this.db.Regions.Include(r => r.Country).OrderBy(x=>x.Country.Name).ThenBy(x=>x.Name).ToList();
        //}

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/country/{id}/region")]
        public List<Dropdown> RegionByCountry(int id)
        {
            var dropDownList = new List<Dropdown>();
            var list = this.db.Regions.Where(r => r.CountryId == id).OrderBy(r => r.Name);
            dropDownList.Add(new Dropdown { Id = 0, Name = string.Empty, Selected = false });

            dropDownList.AddRange(list.Select(region => new Dropdown
                                                        {
                                                            Id = region.Id, Name = region.Name, Selected = false
                                                        }));

            return dropDownList;
        }

        // GET api/Region/5
        //[ResponseType(typeof(Region))]
        //public IHttpActionResult GetRegion(int id)
        //{
        //    Region region = db.Regions.Find(id);
        //    if (region == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(region);
        //}

        // PUT api/Region/5
        //public IHttpActionResult PutRegion(int id, Region region)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != region.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(region).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RegionExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST api/Region
        //[ResponseType(typeof(Region))]
        //public IHttpActionResult PostRegion(Region region)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Regions.Add(region);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = region.Id }, region);
        //}

        // DELETE api/Region/5
        //[ResponseType(typeof(Region))]
        //public IHttpActionResult DeleteRegion(int id)
        //{
        //    Region region = db.Regions.Find(id);
        //    if (region == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Regions.Remove(region);
        //    db.SaveChanges();

        //    return Ok(region);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RegionExists(int id)
        {
            return db.Regions.Count(e => e.Id == id) > 0;
        }
    }
}