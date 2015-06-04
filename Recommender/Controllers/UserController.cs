using Recommender.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recommender.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        MusicReccomenderEntities _db = new MusicReccomenderEntities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        //GET : User/Details/Id
        public ActionResult Details(string id)
        {
            ViewBag.user = _db.AspNetUsers.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.friends = _db.AspNetUsers.Where(x => x.Id == id).FirstOrDefault().AspNetUsers.ToList();
            ViewBag.collections = GetUserCollections(id);

            return View();
        }

        public List<UserCollection> GetUserCollections(string id)
        {
            var user = _db.AspNetUsers.Where(x => x.Id == id).FirstOrDefault();
            var userId = user.Id;

            return _db.UserCollections.Where(x => x.UserId == userId).ToList();
        }
        
        //GET : User/Friends/Id
        public ActionResult Friends(string id)
        {

            //Ги земаме пријателите на јузерот и ги прикажуваме во Friends/id aka Following од Dashboard.


            //Не знам баш како да ги добијам пријателите.
            ViewBag.user = _db.AspNetUsers.Where(x => x.Id == id).FirstOrDefault();
            //ViewBag.friends = _db.AspNetUsers.Where(x => x.Id == id).FirstOrDefault().AspNetUsers.ToList();

            return View();
        }

        public ActionResult Follow(string username)
        {
            //Код за да се заследи пријателот.
            var userToBeFollowed = _db.AspNetUsers.Where(x => x.UserName == username).FirstOrDefault();


            return RedirectToAction("Details", new { id = userToBeFollowed.Id });
        }

        public ActionResult Unfollow(string username)
        {
            //Код за да се одследи пријателот.
            var userToBeUnfollowed = _db.AspNetUsers.Where(x => x.UserName == username).FirstOrDefault();


            return RedirectToAction("Details", new { id = userToBeUnfollowed.Id });
        }
    }
}
