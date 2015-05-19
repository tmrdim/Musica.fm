using Recommender.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Recommender.Model;
using System.Web.UI.WebControls;

namespace Recommender.Controllers
{
    [Authorize]
    public class SongController : Controller
    {
        MusicReccomenderEntities _db = new MusicReccomenderEntities();

        // GET: Song
        public ActionResult Index()
        {
            var songs = _db.Songs.ToList();

            return View(songs);
        }

        public ActionResult Create()
        
        {
            List<SelectListItem> artists = new List<SelectListItem>();
            foreach (var artist in _db.Artists.ToList()) 
            {
                artists.Insert(artist.ArtistId, new SelectListItem { Value = artist.ArtistId.ToString(), Text = artist.ArtistName });
            }
            ViewBag.Artists = artists;

            List<SelectListItem> collections = new List<SelectListItem>();
            foreach (var collection in _db.Collections.ToList())
            {
                collections.Insert(collection.CollectionId, new SelectListItem { Value = collection.CollectionId.ToString(), Text = collection.CollectionName });
            }
            ViewBag.Collections = collections;

            return View();
        }

        // GET: Song/Create
        [HttpPost]
        public ActionResult Create(Song song)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    song.SongId = _db.Songs.Count() + 1;
                    song.Timestamp = DateTime.Now;
                    _db.Songs.Add(song);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            finally
            {
                
            }
            return View(song);
        }

        
        public ActionResult Details(int id)
        {
            var selectedSong = _db.Songs.Where(x => x.SongId == id).FirstOrDefault();
            //ViewBag.Users = _db.AspNetUsers.Where(x => x.Songs.Contains(selectedSong)).ToList();
            //ViewBag.Friends = _db.AspNetUsers.Where(x => x.Songs.Contains(selectedSong)).Where(x => x.AspNetUsers.Contains(_db.AspNetUsers.Where(y => y.Id == /*ID OF CURRENT USER*/).First())).ToList();

            return View(selectedSong);
        }

        // POST: Song/Delete/5
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
    }
}
