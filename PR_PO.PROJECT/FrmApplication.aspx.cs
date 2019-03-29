using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;

namespace PR_PO.PROJECT
{
    public partial class FrmApplication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
              //  this.lblText.Text = "IsPostBack = False";
            }
            else
            {

                testPostback();

            }


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {



            //string currentFile = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/tmpFrmApplication.html";
            //var url = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/FrmApplication.aspx";
            //var http = (HttpWebRequest)WebRequest.Create(url);
            //var response = http.GetResponse();

            //var stream = response.GetResponseStream();
            //var sr = new StreamReader(stream);
            //string content = sr.ReadToEnd();

            //Int32 pos_start, pos_stop;
            //string MidString;
            //pos_start = content.IndexOf("submit", 0);
            //pos_stop = content.IndexOf("/>", pos_start);
            //MidString = content.Substring(pos_start - 13, (pos_stop + 2) - (pos_start - 13));
            //content = content.Replace(MidString, "");
            //string path = System.AppDomain.CurrentDomain.BaseDirectory + "tmpFrmApplication.html";
            //System.IO.File.WriteAllText(path, content);


            ////   string currentFile = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/tmpFormApplication.html";
            //DateTime time = DateTime.Now;
            //string format = "M-d-h-mm-ss";
            //string tmp = time.ToString(format);



            //if (HtmlToPdf(currentFile, tmp))
            //{

            //    Response.Redirect("./Pdf/" + tmp + ".Pdf");

            //}
            //else
            //{
            //    Response.Redirect("./Pdf/" + tmp + ".Pdf");
            //}

        }

        void testPostback()
        {


            string currentFile = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/tmpFrmApplication.html";
            var url = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/FrmApplication.aspx";
            var http = (HttpWebRequest)WebRequest.Create(url);
            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            string content = sr.ReadToEnd();

            Int32 pos_start, pos_stop;
            string MidString;
            pos_start = content.IndexOf("submit", 0);
            pos_stop = content.IndexOf("/>", pos_start);
            MidString = content.Substring(pos_start - 13, (pos_stop + 2) - (pos_start - 13));
            content = content.Replace(MidString, "");
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "tmpFrmApplication.html";
            System.IO.File.WriteAllText(path, content);


            //   string currentFile = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/tmpFormApplication.html";
            DateTime time = DateTime.Now;
            string format = "M-d-h-mm-ss";
            string tmp = time.ToString(format);



            if (HtmlToPdf(currentFile, tmp))
            {

                Response.Redirect("./Pdf/" + tmp + ".Pdf");

            }
            else
            {
                Response.Redirect("./Pdf/" + tmp + ".Pdf");
            }


        }

        public static bool HtmlToPdf(string Url, string outputFilename)
        {

            // killProcess();


            //  string filename = ConfigurationManager.AppSettings["ExportFilePath"] + "\\" + outputFilename + ".pdf";
            string filename = HttpContext.Current.Server.MapPath("./Pdf/" + outputFilename + ".pdf");





            // string filename = System.AppDomain.CurrentDomain.BaseDirectory + "PDF\\" + outputFilename + ".pdf";


            //System.Web.HttpContext.Current.Response.Write(filename);
            // System.Web.HttpContext.Current.Response.End();



            System.Diagnostics.Process p = new System.Diagnostics.Process();

            //string switches = "--print-media-type ";
            //switches += "--margin-top 4mm --margin-bottom 4mm --margin-right 0mm --margin-left 0mm ";
            //switches += "--page-size Letter ";
            //switches += "--no-background ";

            string switches = "-T 0 -R 0 -B 0 -L 0 -O Landscape -s A4 --disable-smart-shrinking ";

            p.StartInfo.Arguments = switches + " " + Url + " " + filename;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;

            p.StartInfo.FileName = HttpContext.Current.Server.MapPath("~/bin/") + "wkhtmltopdf.exe";


            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;

            p.Start();
            string output = p.StandardOutput.ReadToEnd();

            p.WaitForExit(10000);

            // read the exit code, close process
            int returnCode = p.ExitCode;
            p.Close();
            //p.Dispose();

            // if 0 or 2, it works
            return (returnCode == 0 || returnCode == 2);


            //System.Web.HttpResponse dd;

            //dd.AddHeader("content-disposition", "attachment;filename=abc.pdf");

            //dd.ContentType = "application/pdf";

            //dd.WriteFile(outputFilename);



        }
    }
}