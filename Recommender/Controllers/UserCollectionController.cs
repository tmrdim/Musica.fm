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

using Recommender.Model;
using Recommender.Models;

namespace Recommender.Controllers
{
    public class UserCollectionController : Controller
    {
        private MusicReccomenderEntities db = new MusicReccomenderEntities();
        private ApplicationUserManager _userManager;

        public UserCollectionController()
        {
        }

        public UserCollectionController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: /UserCollection/
        [Authorize]
        public ActionResult Index()
        {
            var userId = this.User.Identity.GetUserId();

            var modelCollection = (
                    from collection in this.db.UserCollections
                    where collection.UserId == userId
                    select new UserCollection()
                    {
                        UserId = collection.UserId,
                        UserCollectionId = collection.UserCollectionId,
                        UserCollectionName = collection.UserCollectionName,
                        Timestamp = collection.Timestamp,
                        Songs = collection.Songs
                    }
                ).ToList();

            return View(modelCollection);
        }

        // GET: /UserCollection/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var modelSongs = (
                    from song in this.db.Songs
                    join artist in this.db.Artists on song.ArtistId equals artist.ArtistId
                    join author in this.db.Authors on song.AuthorId equals author.AuthorId
                    join genre in this.db.Genres on song.GenreId equals genre.GenreId
                    where song.CollectionId == id
                    select new SongsViewModel()
                    {
                        SongId = song.SongId,
                        SongTitle = song.SongTitle,
                        ArtistId = song.ArtistId,
                        AuthorId = song.AuthorId,
                        ArtistName = artist.ArtistName,
                        AuthorName = author.AuthorName,
                        GenreId = song.GenreId,
                        GenreName = genre.GenreName,
                        Duration = song.Duration,
                        Timestamp = song.Timestamp,
                        CollectionId = song.CollectionId
                    }
                ).ToList();


            ViewData["songs"] = modelSongs;

            return View();
        }

        // GET: /UserCollection/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: /UserCollection/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserCollection usercollection)
        {
            if (ModelState.IsValid)
            {
                db.UserCollections.Add(usercollection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", usercollection.UserId);
            return View(usercollection);
        }

        // GET: /UserCollection/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCollection usercollection = db.UserCollections.Find(id);
            if (usercollection == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", usercollection.UserId);
            return View(usercollection);
        }

        // POST: /UserCollection/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserCollectionId,UserCollectionName,Timestamp,UserId")] UserCollection usercollection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usercollection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", usercollection.UserId);
            return View(usercollection);
        }

        // GET: /UserCollection/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCollection usercollection = db.UserCollections.Find(id);
            if (usercollection == null)
            {
                return HttpNotFound();
            }
            return View(usercollection);
        }

        // POST: /UserCollection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserCollection usercollection = db.UserCollections.Find(id);
            db.UserCollections.Remove(usercollection);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
