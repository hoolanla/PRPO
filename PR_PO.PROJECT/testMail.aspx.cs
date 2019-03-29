using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace PR_PO.PROJECT
{
    public partial class testMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

          //  String[] mailto = { "prakasit.y@gmail.com" };

        //    Helper.Utility.SendEmail(mailto, "Test ODS", "This is test smtp GMAIL", false);

            MailMessage mail = new MailMessage();
            mail.To.Add("jintajak@fttl.ten.fujitsu.com");
            //   mail.To.Add("Another Email ID where you wanna send same email");
            mail.From = new MailAddress("gatepass@fttl.ten.fujitsu.com");
            mail.Subject = "Email using Gmail";

            string Body = "Hi, this mail is to test sending mail" +
                          "using code in ASP.NET";
            mail.Body = Body;

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 25;
            smtp.Host = "mail.fttl.ten.fujitsu.com"; //Or Your SMTP Server Address
            smtp.Credentials = new System.Net.NetworkCredential("gatepass@fttl.ten.fujitsu.com", "23e4opf8");
            //Or your Smtp Email ID and Password
            smtp.EnableSsl = false;
            smtp.Send(mail);

        }
    }
}