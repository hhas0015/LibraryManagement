using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;
using LibraryManagement.Models;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace LibraryManagement.Utils
{
    public class EmailSender
    {
        //private const String API_KEY = "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";

        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("testemailyoga@gmail.com", "FIT5032 Example Email User");

            var to = new EmailAddress(toEmail, "");
            
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
            

        }

        public void SendMailAttachment(Email email, List<string> emailList)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("testemailyoga@gmail.com", "FIT5032 Example Email User");


            foreach(string toEmail in emailList)
            {
                var to = new EmailAddress(toEmail, "");
                email.fileAttachment.SaveAs(System.Web.HttpContext.Current.Server.MapPath("~/upload/" + email.fileAttachment.FileName));

                var plainTextContent = email.content;
                var htmlContent = "<p>" + email.content + "</p>";
                var msg = MailHelper.CreateSingleEmail(from, to, "Welcome", email.subject, htmlContent);

                var bytes = File.ReadAllBytes(System.Web.HttpContext.Current.Server.MapPath("~/upload/" + email.fileAttachment.FileName));
                var file = Convert.ToBase64String(bytes);
                msg.AddAttachment(email.fileAttachment.FileName, file);

                var response = client.SendEmailAsync(msg);

                string fileName = email.fileAttachment.FileName;


                email.fileAttachment.SaveAs(System.Web.HttpContext.Current.Server.MapPath("~/upload/" + email.fileAttachment.FileName));
            }
            

            




        }


    }
}