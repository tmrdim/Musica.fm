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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        
        [Authorize] //Со ова не може некој да си го види профилот ако не е логиран
        public ActionResult MyProfile()
        {
            ViewBag.Message = "Your profile pageasd.";

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