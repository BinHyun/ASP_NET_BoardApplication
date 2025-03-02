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
        public int LISTID { get; set; }
        [DisplayName("제목")]
        public string TITLE { get; set; }
        [DisplayName("내용")]
        public string CONTENTS { get; set; }
        [DisplayName("작성자")]
        public string WRITER { get; set; }
        [DisplayName("작성일")]
        public string CREATED_DATE { get; set; }
    }
}