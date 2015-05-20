using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Recommender.Model;
using Recommender.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Recommender.Controllers
{
    [Authorize]
    public class SongController : Controller
    {
        MusicReccomenderEntities _db = new MusicReccomenderEntities();

        public ActionResult Index()
        {
            var songs = _db.Songs.ToList();

            return View(songs);
        }

        public ActionResult Create()
        {
            ViewBag.Artists = GetArtists();
            ViewBag.Collections = GetCollections();
            ViewBag.Authors = GetAuthors();
            ViewBag.Genres = GetGenres();

            return View();
        }

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
            ViewBag.UserCollections = GetUserCollections();

            return View(selectedSong);
        }

        public ActionResult AddSongToUserCollection(List<int> collectionIds, int SongId)
        {
            try
            {
                var song = _db.Songs.First(x => x.SongId == SongId);
                foreach (var id in collectionIds)
                {
                    var wantedCollection = _db.UserCollections.Where(x => x.UserCollectionId == id).First();
                    song.UserCollections.Add(wantedCollection);
                }
                _db.Entry<Song>(song).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();

                return Json("Success");
            }
            catch (Exception e)
            {
                return Json("Fail");
            }
        }

        // POST: Song/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var wantedSong = _db.Songs.Remove(_db.Songs.Where(x => x.SongId == id).First());
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult AddGenre()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddGenre(Genre genre)
        {
            genre.Timestamp = DateTime.Now;
            genre.GenreId = _db.Genres.Count();
            _db.Genres.Add(genre);
            _db.SaveChanges();

            return RedirectToAction("Create");
        }

        public ActionResult AddAuthor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAuthor(Author author)
        {
            author.Timestamp = DateTime.Now;
            author.AuthorId = _db.Authors.Count();
            _db.Authors.Add(author);
            _db.SaveChanges();

            return RedirectToAction("Create");
        }

        public ActionResult AddArtist()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddArtist(Artist artist)
        {
            artist.Timestamp = DateTime.Now;
            artist.ArtistId = _db.Artists.Count();
            _db.Artists.Add(artist);
            _db.SaveChanges();

            return RedirectToAction("Create");
        }

        public ActionResult AddCollection()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCollection(Collection collection)
        {
            collection.Timestamp = DateTime.Now;
            collection.CollectionId = _db.Collections.Count();
            _db.Collections.Add(collection);
            _db.SaveChanges();

            return RedirectToAction("Create");
        }

        public List<SelectListItem> GetAuthors()
        {
            List<SelectListItem> authors = new List<SelectListItem>();
            foreach (var author in _db.Authors.ToList())
            {
                authors.Insert(author.AuthorId, new SelectListItem { Value = author.AuthorId.ToString(), Text = author.AuthorName });
            }
            return authors;
        }

        public List<SelectListItem> GetCollections()
        {
            List<SelectListItem> collections = new List<SelectListItem>();
            foreach (var collection in _db.Collections.ToList())
            {
                collections.Insert(collection.CollectionId, new SelectListItem { Value = collection.CollectionId.ToString(), Text = collection.CollectionName });
            }
            return collections;
        }

        public List<SelectListItem> GetArtists()
        {
            List<SelectListItem> artists = new List<SelectListItem>();
            foreach (var artist in _db.Artists.ToList())
            {
                artists.Insert(artist.ArtistId, new SelectListItem { Value = artist.ArtistId.ToString(), Text = artist.ArtistName });
            }
            return artists;
        }

        public List<SelectListItem> GetGenres()
        {
            List<SelectListItem> genres = new List<SelectListItem>();
            foreach (var genre in _db.Genres.ToList())
            {
                genres.Insert(genre.GenreId, new SelectListItem { Value = genre.GenreId.ToString(), Text = genre.GenreName });
            }
            return genres;
        }

        public List<UserCollection> GetUserCollections()
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = manager.FindByName(User.Identity.Name);

            var userId = user.Id;

            return _db.UserCollections.Where(x => x.UserId == userId).ToList();
        }
    }


}

