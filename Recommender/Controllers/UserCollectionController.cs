using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Recommender.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

using Recommender.Models;

namespace Recommender.Controllers
{
    [Authorize]
    public class UserCollectionController : Controller
    {
        private MusicReccomenderEntities _db = new MusicReccomenderEntities();
        private static string _id = null;


        public ActionResult Index(string id)
        {
            var user = _db.AspNetUsers.Where(x => x.Id == id).FirstOrDefault();
            var collections = user.UserCollections.ToList();

            _id = id;
            ViewBag.UserId = _id;
            return View(collections);
        }

        public ActionResult Details(int id)
        {
            var userCollection = _db.UserCollections.Where(x => x.UserCollectionId == id).FirstOrDefault();

            return View(userCollection);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserCollection usercollection)
        {
            if (ModelState.IsValid)
            {
                usercollection.UserCollectionId = _db.UserCollections.Count();
                usercollection.UserId = _id;

                _db.UserCollections.Add(usercollection);
                _db.SaveChanges();

                return RedirectToAction("Index", new { id = _id });
            }

            return View(usercollection);
        }

        public ActionResult Delete()
        {
            ViewBag.UserId = _id;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var usercollection = _db.UserCollections.Where(x => x.UserCollectionId == id).First();

            var user = _db.AspNetUsers.Where(x => x.Id == _id).First();
            user.UserCollections.Remove(usercollection);
            _db.UserCollections.Remove(usercollection);

            _db.Entry<UserCollection>(usercollection).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            ViewBag.UserId = _id;
            return RedirectToAction("Index", new { id = _id });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
