using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoardApplication.Models;
using System.Configuration;
using System.Data.SqlClient;


namespace BoardApplicationTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void InsertTest()
        {
            var boardList = new Boardlist
            {
                TITLE = "테스트 타이틀",
                CONTENTS = "테스트 작성용 입니다.",
                WRITER = "테스터",
                CREATED_DATE = DateTime.Now.ToString("yyyy-MM-dd")
            };
        }
    }
}
