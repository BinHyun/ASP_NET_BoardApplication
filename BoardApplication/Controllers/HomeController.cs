using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using BoardApplication.Models;

namespace BoardApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var BoardList = new List<Boardlist>();

            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    ViewBag.Message = "Database connection successful!";

                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "DB 연결 실패 : " + ex.Message;
            }

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