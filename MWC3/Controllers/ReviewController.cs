﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MWC3.Controllers
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity;

    using MWC3.Models;

    public class ReviewController : BaseController
    {
        //
        // GET: /Review/
        public ActionResult Index(int wineId, int year)
        {
            var reviews = this.Db.Reviews.Where(x => x.WineId == wineId && x.Year == year).ToList();




            return View();
        }

        //
        // GET: /Review/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Review/Create
        public ActionResult Create(int wineId, int year)
        {
            var reviewers = this.FindReviewers();
            var wine = this.Db.Wines.Find(wineId);

            if (wine == null)
            {
                // raise error
            }

            var reviews = new List<Review>();
            foreach (var item in reviewers)
            {
                reviews.Add(new Review
                             {
                                 WineId = wineId,
                                 Year = year,
                                 ReviewerId = item.Id,
                                 Wine = wine
                             });
            }

            ;

            return View(reviews);
        }

        //
        // POST: /Review/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Review/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Review/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Review/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Review/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private List<ApplicationUser> FindReviewers()
        {
            var users = new List<ApplicationUser>();
            var userId = this.GetUserId();
            users.Add(this.UserManager.FindById(userId));
            users.Add(this.UserManager.FindByName("HaroldDraaisma"));
            users.Add(this.UserManager.FindByName("NicolaasKlei"));
            users.Add(this.UserManager.FindByName("RobertParker"));

            return users.Where(u => u != null).ToList();
        }
    }
}