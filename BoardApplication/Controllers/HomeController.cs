using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using BoardApplication.Models;
using System.Data;

namespace BoardApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var BoardList = new List<Boardlist>();

            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            string sql = "SELECT * FROM [BoardList]";
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(sql, con);
                    da.Fill(ds);
                    ViewBag.Message = "Database connection successful!";
                    ViewBag.Ds = ds;

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