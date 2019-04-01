using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Configuration;
using System.Text;
using System.Data;


namespace PR_PO.PROJECT
{
    public partial class FrmApplicationApprove : System.Web.UI.Page
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

                if (Request.QueryString["email"] != null)
                {
                    Session["EMAIL"] = Request.QueryString["email"].ToString();
                
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



        }


        void testPostback()
        {

            if(txtNot.Text != "")
            {

                Model.Criteria.Document _doc = new Model.Criteria.Document();
                _doc.doc_id = doc_id.Value;
                _doc.approve_problem = txtNot.Text;
                BLL.Upload _BLL = new BLL.Upload();
                _BLL.Update_sign_approve_problem(_doc);


             //   Response.Redirect("./DataDocument.aspx");
            }
            else
            {
                Model.Criteria.Document _doc = new Model.Criteria.Document();
                _doc.doc_id = doc_id.Value;
                _doc.approve_problem = "0";
                BLL.Upload _BLL = new BLL.Upload();
                _BLL.Update_sign_approve_problem(_doc);
            }
            
            string currentFile = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/tmpFrmApplication.html";
            var url = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/FrmApplicationApprove.aspx?doc_id=" + doc_id.Value + "&signature=" + signature_file.Value + "&page_count=" + page_count.Value + "&paper_type=" + paper_type.Value + "&top=" + dTop.Value + "&left=" + dLeft.Value;
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

            pos_start = content.IndexOf("txtNot", 0);
            pos_stop = content.IndexOf("/>", pos_start);
            MidString = content.Substring(pos_start - 13, (pos_stop + 2) - (pos_start - 13));
            content = content.Replace(MidString, "");

 
            content = content.Replace("top:5000px", "top:" + dTop.Value.Replace("'",""));
            content = content.Replace("left:5000px", "left:" + dLeft.Value.Replace("'",""));
            if (txtNot.Text == "")
            {
                content = content.Replace("hidden='hidden'", "");
            }
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "tmpFrmApplication.html";
            System.IO.File.WriteAllText(path, content);


            //   string currentFile = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/tmpFormApplication.html";
            DateTime time = DateTime.Now;
            string format = "M-d-h-mm-ss";
            string tmp = time.ToString(format);



            if (HtmlToPdf(currentFile, doc_id.Value,paper_type.Value))
            {


                Model.Log L = new Model.Log();
                Helper.Utility Log = new Helper.Utility();

                L.content = "Sign approve success.";
                L.create_by = Session["EMAIL"].ToString();
                Log.WriteLog(L);

                Model.Criteria.Document _Doc = new Model.Criteria.Document();
                BLL.Upload _BLL = new BLL.Upload();

                _Doc.sign_approve_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                _Doc.doc_id = doc_id.Value;
                _BLL.Update_sign_approve_date(_Doc);


             /////////////////////////   ReplyMail();


                Response.Redirect("./DataDocument.aspx");

            }
            else
            {
             //   Response.Redirect("./Pdf/" + tmp + ".Pdf");
            }


        }

        public static bool HtmlToPdf(string Url, string outputFilename,string paper_type)
        {

            // killProcess();


            //  string filename = ConfigurationManager.AppSettings["ExportFilePath"] + "\\" + outputFilename + ".pdf";
            string filename = HttpContext.Current.Server.MapPath("./PdfApprove/" + outputFilename + ".pdf");


            System.Diagnostics.Process p = new System.Diagnostics.Process();

            //string switches = "--print-media-type ";
            //switches += "--margin-top 4mm --margin-bottom 4mm --margin-right 0mm --margin-left 0mm ";
            //switches += "--page-size Letter ";
            //switches += "--no-background ";

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
          //  return (returnCode == 0 || returnCode == 2);


            //System.Web.HttpResponse dd;

            //dd.AddHeader("content-disposition", "attachment;filename=abc.pdf");

            //dd.ContentType = "application/pdf";

            //dd.WriteFile(outputFilename);



        }



//        void ReplyMail()
//        {

//            BLL.Upload _BLL =  new BLL.Upload();
//            Model.Criteria.Document criteria = new Model.Criteria.Document();
//            criteria.doc_id = Session["DOC_ID"].ToString();

//            List<Model.Document> m_doc = new List<Model.Document>();
//            m_doc = _BLL.getAll_Document(criteria);



//            String tmpMail="",doc_name="",secure_approve="",secure_prepare="",doc_id="",approve_problem="",content="";
//            int doc_id_int=0;
//            if (m_doc.Count > 0)
//            {
//                tmpMail = m_doc[0].secure_prepare;
//                doc_name = m_doc[0].doc_name;
//                secure_approve = m_doc[0].secure_approve;
//                secure_prepare = m_doc[0].secure_prepare;
//                doc_id_int = m_doc[0].doc_id_int;
//                approve_problem = m_doc[0].approve_problem;
//                content = m_doc[0].content;
//            }


//            /// Secure prepare

//            DataTable dt;
//            BLL.ManageAccount a_BLL = new BLL.ManageAccount();
//            Model.Criteria.M_AccountCriteria a_criteria = new Model.Criteria.M_AccountCriteria();
//            a_criteria.email = secure_prepare;
//            Model.M_Account Account = new Model.M_Account();
//            dt = a_BLL.getAccount_By_Email(a_criteria);

//            if (dt.Rows.Count > 0)
//            {
//                Account.title = dt.Rows[0]["title"].ToString();
//                Account.name = dt.Rows[0]["name"].ToString();
//                Account.surname = dt.Rows[0]["surname"].ToString();
//            }


//            /// Secure Approve
//            /// 
//            DataTable f_dt;
//            BLL.ManageAccount f_BLL = new BLL.ManageAccount();
//            Model.Criteria.M_AccountCriteria f_criteria = new Model.Criteria.M_AccountCriteria();
//            f_criteria.email = secure_approve;
//            Model.M_Account f_Account = new Model.M_Account();
//            f_dt = a_BLL.getAccount_By_Email(f_criteria);

//            if (f_dt.Rows.Count > 0)
//            {
//                f_Account.title = f_dt.Rows[0]["title"].ToString();
//                f_Account.name = f_dt.Rows[0]["name"].ToString();
//                f_Account.surname = f_dt.Rows[0]["surname"].ToString();
//            }


//            string mailForm = f_Account.title + " " + f_Account.name + "   " + f_Account.surname;




//            string[] mailTo = new string[] { tmpMail };
//            List<string> myCollection = new List<string>(); // Attach File
//            string m_content = "";
//            m_content = "Re: " + content;
//            string serverName = ConfigurationManager.AppSettings["serverName"];
//          //  string strPathAttach = MapPath(".//AttachFiles/" + Session["DOC_ID"].ToString() + ".zip");
////myCollection.Add(strPathAttach);
//            StringBuilder mailBody = new StringBuilder();
//            string sTAB = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

//          //  string mailForm = Session["TITLE"].ToString() + " " + Session["NAME"].ToString() + "   " + Session["SURNAME"].ToString();

//            mailBody.AppendLine("<table>");
//            mailBody.AppendLine("<tr><td>Subject : อ้างอิงจากเอกสารเลขที่ " + sTAB + doc_id_int + "  [" + m_content + "]</td></tr>");
//            mailBody.AppendLine("<tr><td>To :" + sTAB + Account.title + "  " + Account.name + "   " + Account.surname + "</td></tr>");
//            mailBody.AppendLine("</table><br />");


//            mailBody.AppendLine("<table>");
//            if (txtNot.Text == "")
//            {
//                mailBody.AppendLine("<tr><td>Status approve <a href='ht" + "tp://" + serverName + "/ODS/PdfApprove/" + doc_id_int + ".pdf'>Click here.</a> </td></tr>");
//            }
//            else
//            {
//                mailBody.AppendLine("<tr><td>Status not approve.   " + approve_problem + " </td></tr>");
//            }
//            mailBody.AppendLine("<tr><td><BR/></td></tr>");
//            mailBody.AppendLine("<tr><td><BR/>Best Regards,<td></tr>");
//            mailBody.AppendLine("<tr><td><BR/>" + mailForm + "</td></tr>");
//            mailBody.AppendLine("</table><br />");

         
//            List<string> _mailCC = new List<string>();

//       //     Helper.Utility.SendEmail(mailTo, _mailCC.ToArray(), m_content, mailBody.ToString(), true, myCollection.ToArray(), mailForm);

//            Response.Redirect("DataDocument.aspx");
//        }
    }
}