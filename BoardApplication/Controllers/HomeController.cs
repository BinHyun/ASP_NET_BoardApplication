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

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        BoardList.Add(new Boardlist
                        {
                            Listid = Convert.ToInt32(reader["Listid"]),
                            Number = Convert.ToInt32(reader["number"]),
                            Title = reader["Title"].ToString(),
                            Writer = reader["Writer"].ToString(),
                            Published_data = Convert.ToInt32(reader["Published_data"])
                        });
                    }
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