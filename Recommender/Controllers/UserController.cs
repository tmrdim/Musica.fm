using Recommender.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recommender.Controllers
{
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

            ViewBag.collections = new List<UserCollection>(); //GetUserCollections(id);
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
            //Не знам баш како да ги добијам пријателите.
            ViewBag.user = _db.AspNetUsers.Where(x => x.Id == id).FirstOrDefault();
            return View();
        }

    }
}
