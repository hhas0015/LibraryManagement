using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class Email
    {
        public string subject { get; set; }
        public string content { get; set; }

        public HttpPostedFileBase fileAttachment { get; set; }
    }
}