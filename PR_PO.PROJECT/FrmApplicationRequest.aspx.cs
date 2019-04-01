using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Drawing;
using Ghostscript.NET.Rasterizer;



namespace PR_PO.PROJECT
{
    public partial class FrmApplicationRequest : System.Web.UI.Page
    {
        String DL = null;
        String DT = null;
  

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {


                if (Request.QueryString["doc_id"] != null)
                {
                    doc_id.Value = Request.QueryString["doc_id"].ToString();
                    Session["DOC_ID"] = Request.QueryString["doc_id"].ToString();
                }

                if (Request.QueryString["signature"] != null)
                {
                    signature_file.Value = Request.QueryString["signature"].ToString();
                    Session["SIGNATURE"] = Request.QueryString["signature"].ToString();
                }

                if (Request.QueryString["page_count"] != null)
                {
                    page_count.Value = Request.QueryString["page_count"].ToString();
                    Session["PAGE_COUNT"] = Request.QueryString["page_count"].ToString();
                }

                if (Request.QueryString["paper_type"] != null)
                {
                    paper_type.Value = Request.QueryString["paper_type"].ToString();
                    Session["PAPER_TYPE"] = Request.QueryString["paper_type"].ToString();
                }
            }
            else
            {

              


                String DL = dLeft.Value.Replace("'","");
                String DT = dTop.Value.Replace("'","");

                signature_file.Value = Session["SIGNATURE"].ToString();
                doc_id.Value = Session["DOC_ID"].ToString();
                page_count.Value = Session["PAGE_COUNT"].ToString();
                paper_type.Value = Session["PAPER_TYPE"].ToString();


                
             testPostback();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {


        //   testPostback();

        
         


        }


        void testPostback()
        {

        
            
            string currentFile = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/tmpFrmApplication.html";
            //string currentFile = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/testHtml.html";
            var url = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/FrmApplicationRequest.aspx?doc_id=" + doc_id.Value + "&signature=" + signature_file.Value + "&page_count=" + page_count.Value + "&paper_type=" + paper_type.Value;
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
            //content = content.Replace("Images/", "Images/" + signature_file.Value);
            //content = content.Replace("PdfToImage/", "PdfToImage/" + doc_id.Value);
            content = content.Replace("top:123px", "top:" + dTop.Value.Replace("'",""));
            content = content.Replace("left:123px", "left:" + dLeft.Value.Replace("'",""));
            content = content.Replace("hidden='hidden'", "");
            content = content.Replace("<div class='row vertical-center-row'><div class='text-center col-md-1 col-md-offset-1'></div></div></div>", "");
            //if (paper_type.Value =="L")
            //{
            //    content = content.Replace("width='790'", "width='1120'");
            //    content = content.Replace("height='1120'", "height='790'");
            //}
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "tmpFrmApplication.html";
            System.IO.File.WriteAllText(path, content);



            // test git

            //   string currentFile = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/tmpFormApplication.html";
            //DateTime time = DateTime.Now;
            //string format = "M-d-h-mm-ss";
            //string tmp = time.ToString(format);



            if (HtmlToPdf(currentFile, doc_id.Value,paper_type.Value))
            {



           



                Model.Log L = new Model.Log();
                Helper.Utility Log = new Helper.Utility();

                L.content = "Sign request success.";
                L.create_by = Session["EMAIL"].ToString();
                Log.WriteLog(L);

                Model.Criteria.Document _Doc = new Model.Criteria.Document();
                BLL.Upload _BLL = new BLL.Upload();

                _Doc.sign_prepare_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                _Doc.doc_id = doc_id.Value;
                _BLL.Update_sign_prepare_date(_Doc);


                PdfToImage("", _Doc.doc_id);

                Response.Redirect("./DataDocument.aspx");

//Response.Redirect("./PdfPrepare/" + doc_id.Value + ".Pdf");
            }
            else
            {
               // Response.Redirect("./PdfPrepare/" + doc_id.Value + ".Pdf");
            }


        }


        private bool PdfToImage(string pdfName, string fileCurrentName)
        {

            int desired_x_dpi = 300;
            int desired_y_dpi = 300;

            string ServerPath = Server.MapPath(".\\");
            string pdfPath = Server.MapPath(".\\") + "PdfRequest/" + fileCurrentName + ".pdf";

  




            string pageCount;
            BLL.Upload _BLL = new BLL.Upload();
            pageCount = _BLL.get_Pagecount(Session["DOC_ID"].ToString());




            //for (int i = 0; i < int.Parse(pageCount); i++)
            //{





        

            //    if (int.Parse(pageCount) > 1)
            //    {
            //        zoomImg.Save(@ServerPath + "/PdfToImageApprove/" + fileCurrentName + "_" + (i + 1) + ".PNG");
             
            //    }
            //    else
            //    {
            //        zoomImg.Save(@ServerPath + "/PdfToImageApprove/" + fileCurrentName + ".PNG");
     
            //    }
            //}


            using (var rasterizer = new GhostscriptRasterizer())
            {
                rasterizer.Open(pdfPath);
             
                for (var pageNumber = 1; pageNumber <= int.Parse(pageCount); pageNumber++)
                {

                    if (int.Parse(pageCount) > 1)
                    {
                        var pageFilePath = Path.Combine(Server.MapPath("~/PdfToImageRequest/"), fileCurrentName + "_" + (pageNumber) + ".PNG");
                        var img = rasterizer.GetPage(desired_x_dpi, desired_y_dpi, pageNumber);
                        img.Save(pageFilePath);
                    }
                    else
                    {
                        var pageFilePath = Path.Combine(Server.MapPath("~/PdfToImageRequest/"), fileCurrentName + ".PNG");
                        var img = rasterizer.GetPage(desired_x_dpi, desired_y_dpi, pageNumber);
                        img.Save(pageFilePath);
                    }
                }
            }

            //Model.Criteria.Document doc = new Model.Criteria.Document();
            //doc.doc_id = fileCurrentName;
            //doc.upload_date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            //BLL.Upload _BLL = new BLL.Upload();
            //int ret;
            //ret = _BLL.Update_upload_date(doc);


            // Write LOG

            Model.Log L = new Model.Log();
            Helper.Utility Log = new Helper.Utility();

            L.content = "Convert PDF to Image success.";
            L.create_by = Session["EMAIL"].ToString();

            Log.WriteLog(L);

            return true;
        }

        public static bool HtmlToPdf(string Url, string outputFilename,string paper_type)
        {

            // killProcess();


            //  string filename = ConfigurationManager.AppSettings["ExportFilePath"] + "\\" + outputFilename + ".pdf";
            string filename = HttpContext.Current.Server.MapPath("./PdfRequest/" + outputFilename + ".pdf");





            // string filename = System.AppDomain.CurrentDomain.BaseDirectory + "PDF\\" + outputFilename + ".pdf";


            //System.Web.HttpContext.Current.Response.Write(filename);
            // System.Web.HttpContext.Current.Response.End();



            System.Diagnostics.Process p = new System.Diagnostics.Process();

            //string switches = "--print-media-type ";
            //switches += "--margin-top 4mm --margin-bottom 4mm --margin-right 0mm --margin-left 0mm ";
            //switches += "--page-size Letter ";
            //switches += "--no-background ";
          //  switches = "-T 0 -R 0 -B 0 -L 0 -O Portrait -s A4 --disable-smart-shrinking ";
            string switches;
            if (paper_type == "P")
            {
                switches = "-T 0 -R 0 -B 0 -L 0 -O Portrait -s A4 --disable-smart-shrinking ";
            }
            else
            {
                switches = "-T 0 -R 0 -B 0 -L 0 -O Landscape -s A4 --disable-smart-shrinking ";
            }
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

            return true;
//return (returnCode == 0 || returnCode == 2);


            //System.Web.HttpResponse dd;

            //dd.AddHeader("content-disposition", "attachment;filename=abc.pdf");

            //dd.ContentType = "application/pdf";

            //dd.WriteFile(outputFilename);



        }
    }
}