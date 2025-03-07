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
        string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        public ActionResult Index()
        {
            var BoardList = new List<Boardlist>();
            
            string sql = "SELECT * FROM [BOARDLIST]";

            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        BoardList.Add(new Boardlist
                        {
                            LISTID = Convert.ToInt32(reader["LISTID"]),
                            TITLE = reader["TITLE"].ToString(),
                            WRITER = reader["WRITER"].ToString(),
                            CREATED_DATE = reader["CREATED_DATE"].ToString()
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
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TITLE, CONTENTS, WRITER")] Boardlist boardlist)
        {
            if (ModelState.IsValid)
            {
                string sql = @"
                INSERT INTO [BOARDLIST] (TITLE, CONTENTS, WRITER, CREATED_DATE) VALUES (@TITLE, @CONTENTS, @WRITER, @CREATED_DATE)";

                using (con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@TITLE", boardlist.TITLE);
                        cmd.Parameters.AddWithValue("@CONTENTS", boardlist.CONTENTS);
                        cmd.Parameters.AddWithValue("@WRITER", boardlist.WRITER);
                        cmd.Parameters.AddWithValue("@CREATED_DATE", DateTime.Now.ToString("yyyy-MM-dd")); // 현재 시간 설정

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("index");
            }

           return View(boardlist);            
        }

        public ActionResult Read(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            string sql = @"SELECT * FROM [BOARDLIST] WHERE LISTID = @ListId";
            var BoardList = new List<Boardlist>();

            using (con = new SqlConnection(connectionString))
            {
                con.Open();
                using (cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@ListId", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BoardList.Add(new Boardlist
                            {
                                LISTID = Convert.ToInt32(reader["LISTID"]),
                                TITLE = reader["TITLE"].ToString(),
                                WRITER = reader["WRITER"].ToString(),
                                CONTENTS = reader["CONTENTS"].ToString()
                            });
                        }
                    }
                }
            }
            ViewBag.close = false;
            return View(BoardList);
        }

        [HttpPost]
        public JsonResult Update(string contents, string listId)
        {
            if (string.IsNullOrEmpty(listId))
            {
                return Json(new { success = false, message = "listId가 없습니다." });
            }

            string sql = @"UPDATE [BOARDLIST] SET CONTENTS = @CONTENTS, CREATED_DATE = @CREATED_DATE WHERE LISTID = @ListId";

            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@CONTENTS", contents);
                        cmd.Parameters.AddWithValue("@CREATED_DATE", DateTime.Now.ToString("yyyy-MM-dd")); // 현재 시간 설정
                        cmd.Parameters.AddWithValue("@ListId", listId);

                        con.Open();
                        int updateRow = cmd.ExecuteNonQuery();

                        if (updateRow > 0)
                        {
                            return Json(new { success = true, message = "저장이 완료 되었습니다." });
                        }
                        else
                        {
                            return Json(new { success = false, message = "저장이 완료되지 않았습니다." });
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = "서버 오류 발생: " + ex.Message });
            }
        }
    }
}