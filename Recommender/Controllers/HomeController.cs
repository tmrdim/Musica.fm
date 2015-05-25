using Microsoft.AspNet.Identity.EntityFramework;
using Recommender.Model;
using Recommender.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Recommender.Controllers
{
    public class HomeController : Controller
    {
        MusicReccomenderEntities _db = new MusicReccomenderEntities();

        public ActionResult Index()
        {
            return View();
        }


        

        [Authorize]
        public ActionResult Dashboard()
        {
            ViewBag.Message = "Your profile page.";


            string email = HttpContext.User.Identity.Name;
            var user = _db.AspNetUsers.Where(x => x.UserName == email).FirstOrDefault();
            ViewBag.user = user;

            return View();
        }

        [Authorize]
        public ActionResult MyCollections()
        {
            ViewBag.Message = "Your collections page.";

            return View();
        }


        public ActionResult Recommend()
        {
            List<Song> recommendations = new List<Song>();
            
            //var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            //var user = manager.FindByNameAsync(User.Identity.Name);
            
            //AspNetUser wantedUser = _db.AspNetUsers.First(x => x.Id.Equals(user.Id));
            //var songs = wantedUser.Songs.Take(11);

            //foreach (var song in songs)
            //{
            //    UserCollection collection = song.UserCollections
            //                                        .OrderByDescending(x => x.Timestamp)
            //                                        .First();
            //    Song recommendatedSong = collection.Songs
            //                                        .OrderByDescending(x => x.Timestamp)
            //                                        .First();
            //    recommendations.Add(recommendatedSong);
            //}

            var songs = _db.Songs.Take(10).ToList();
            return View(songs);
        }
    }
}