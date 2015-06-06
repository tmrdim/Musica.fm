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
        static List<AspNetUser> _friends = null;
        static AspNetUser _loggedUser = null;
        static string _loggedUserId = null;
        
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserProfile(string id)
        {
            var user = _db.AspNetUsers.Where(x => x.Id == id).FirstOrDefault();
            _loggedUser = user;
            _loggedUserId = id;
            var friends = _db.AspNetUsers.Where(x => x.Id == id).FirstOrDefault().AspNetUsers1.ToList();
            ViewBag.Friends = friends;
            ViewBag.Collections = GetUserCollections(id);

            return View(user);
        }

        //GET : User/Details/Id
        public ActionResult Details(string id, string loggedId)
        {
            var user = _db.AspNetUsers.Where(x => x.Id == id).FirstOrDefault();
            var friends = _db.AspNetUsers.Where(x => x.Id == id).FirstOrDefault().AspNetUsers1.ToList();
            ViewBag.Friends = friends;

            ViewBag.LoggedUser = _loggedUser;
            ViewBag.LoggedUserFriends = _friends;

            ViewBag.Collections = GetUserCollections(id);

            return View(user);
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
            var user = _db.AspNetUsers.Where(x => x.Id == id).FirstOrDefault();
            _loggedUser = user;
            _loggedUserId = id;
            ViewBag.user = user;
            var friends = _db.AspNetUsers.Where(x => x.Id == id).FirstOrDefault().AspNetUsers1.ToList();
            _friends = friends;

            return View(friends);
        }

        public ActionResult Follow(string id)
        {
            var toFriend = _db.AspNetUsers.Where(x => x.Id == id).FirstOrDefault();
            var loggedUser = _db.AspNetUsers.Where(x => x.Id == _loggedUserId).FirstOrDefault();
            var friends = loggedUser.AspNetUsers1.ToList();
            _loggedUser.AspNetUsers1.Add(toFriend);

            _db.Entry<AspNetUser>(_loggedUser).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            _friends = loggedUser.AspNetUsers1.ToList();
            ViewBag.LoggedUserFriends = loggedUser.AspNetUsers1.ToList();
            return RedirectToAction("Details", "User", new { id = id, loggedId = _loggedUser.Id });
        }

        public ActionResult Unfollow(string id)
        {
            var unfriend = _db.AspNetUsers.Where(x => x.Id == id).FirstOrDefault();
            var loggedUser = _db.AspNetUsers.Where(x => x.Id == _loggedUserId).FirstOrDefault();
            loggedUser.AspNetUsers1.Remove(unfriend);

            _db.Entry<AspNetUser>(loggedUser).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            _friends = loggedUser.AspNetUsers1.ToList();
            ViewBag.LoggedUserFriends = loggedUser.AspNetUsers1.ToList();
            return RedirectToAction("Details", "User", new { id = id, loggedId = _loggedUserId });
        }
    }
}
