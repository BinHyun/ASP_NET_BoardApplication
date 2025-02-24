using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace BoardApplication.Models
{
    public class Boardlist
    {
        [Key]
        public int Listid { get; set; }
        [DisplayName("순번")]
        public int Number { get; set; }
        [DisplayName("제목")]
        public string Title { get; set; }
        [DisplayName("내용")]
        public string Content { get; set; }
        [DisplayName("작성자")]
        public string Writer { get; set; }
        [DisplayName("작성일")]
        public int Published_data { get; set; }
    }
}