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


        
        [Authorize] //Со ова не може некој да си го види профилот ако не е логиран
        public ActionResult MyDashboard()
        {
            ViewBag.Message = "Your profile page.";

            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = manager.FindByNameAsync(User.Identity.Name);
            ViewBag.id = user.Id;

            return View();
        }

        [Authorize]
        public ActionResult MyCollections()
        {
            ViewBag.Message = "Your collections page.";

            return View();
        }
    }
}