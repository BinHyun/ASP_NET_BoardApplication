using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoardApplication.Models;

namespace BoardApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var BoardList = new List<Boardlist>();
            return View(BoardList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}