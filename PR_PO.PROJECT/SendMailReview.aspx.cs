using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using PR_PO.PROJECT.Class;
using System.Net;
using System.IO.Compression;
using Ionic.Zip;
using System.Drawing;
using Spire.Pdf;
using System.Text;
using System.Data;
using System.Configuration;




namespace PR_PO.PROJECT
{
    public partial class SendMailReview : Page
    {

        Model.Criteria.Document Doc = new Model.Criteria.Document();
        Model.M_Account Account = new Model.M_Account();
        string m_email, m_doc_id,m_content;

        protected void Page_Load(object sender, EventArgs e)
        {

            if(Session["EMAIL"] == null)
            {

                Response.Redirect("Authorize.aspx");
            }



            if (!IsPostBack)
            {


                Session["EMAIL"] = Request.QueryString["email"].ToString();
                Session["DOC_ID"] = Request.QueryString["doc_id"].ToString();
                Session["CONTENT"] = Request.QueryString["content"].ToString();
                m_doc_id = Request.QueryString["doc_id"].ToString();
          //      m_email = 


                
             //   initDataAccount(m_email);
                initListbox();
                initDocumentbyID(m_doc_id);
            }
            else
            {
            
            }
         
        }


        protected void initListbox()
        {
            BLL.Upload objBLL = new BLL.Upload();
            var list = new List<Model.Account>();
            list = objBLL.getEmail();


          dlemailCC.DataSource = list;
          dlemailCC.DataTextField = "name";
          dlemailCC.DataValueField= "email";
          dlemailCC.DataBind();
           
        }
        
       
       protected void initDataAccount(string mailAccount)
         {

            DataTable dt;
            BLL.ManageAccount _BLL = new BLL.ManageAccount();
            Model.Criteria.M_AccountCriteria criteria = new Model.Criteria.M_AccountCriteria();
            criteria.email = mailAccount;

            dt = _BLL.getAccount_By_Email(criteria);

           if(dt.Rows.Count > 0 )
           {
               Account.title = dt.Rows[0]["title"].ToString();
               Account.name = dt.Rows[0]["name"].ToString();
               Account.surname = dt.Rows[0]["surname"].ToString();
           }

        }



       protected void initDocumentbyID(string doc_id)
       {

           DataTable dt;
           BLL.Upload _BLL = new BLL.Upload();

           Model.Document m_doc = new Model.Document();
           dt = _BLL.getDocument_By_DocId(m_doc_id);

           if (dt.Rows.Count > 0)
           {
               int x = 0;
               Int32.TryParse(dt.Rows[0]["page_count"].ToString(), out x);
               m_doc.page_count = x;
               Session["PAPER_TYPE"] = dt.Rows[0]["paper_type"].ToString();
               Session["PAGE_COUNT"] = dt.Rows[0]["page_count"].ToString();
               Session["CONTENT"]  = dt.Rows[0]["content"].ToString();
           }

       }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string message = null;
            List<string> _mailCC = new List<string>();
            foreach (ListItem item in dlemailCC.Items)
            {
                if (item.Selected)
                {

                    _mailCC.Add(item.Value);
               
                }
            }
         //   ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + message + "');", true);

            //string[] _mailCC = null;
            //if (strCC != null)
            //{
            //    _mailCC = strCC.Split(',');
            ////String strCC = Request.Form["dlemailCC"].ToString();
            ////string[] _mailCC = null;
            ////if (strCC != null)
            ////{
            ////    _mailCC = strCC.Split(',');
            ////} 

  
            String tmpMail;
     
   
           tmpMail = Request.Form["dlemail"].ToString();

           initDataAccount(tmpMail);

             string[] mailTo = new string[] {tmpMail };
                   List<string> myCollection = new List<string>(); // Attach File

                   string serverName = ConfigurationManager.AppSettings["serverName"];
                   string strPathAttach = MapPath(".//AttachFiles/" + Session["DOC_ID"].ToString() + ".zip");
                     myCollection.Add(strPathAttach);
                   StringBuilder mailBody = new StringBuilder();
                   string sTAB = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

                   string mailForm = Session["TITLE"].ToString() + " " + Session["NAME"].ToString() + "   " + Session["SURNAME"].ToString();

                   mailBody.AppendLine("<table>");
                   mailBody.AppendLine("<tr><td>Subject :" + sTAB + Session["CONTENT"].ToString() + "</td></tr>");
                   mailBody.AppendLine("<tr><td>To :" + sTAB + Account.title +"  " + Account.name + "   " + Account.surname + "</td></tr>");
                   mailBody.AppendLine("</table><br />");


                   string paper_type = Session["PAPER_TYPE"].ToString();
                   string page_count = Session["PAGE_COUNT"].ToString();
            string signature;
            BLL.ManageAccount M_BLL = new BLL.ManageAccount();
            signature = M_BLL.Get_Signature_By_Email(tmpMail);



                   mailBody.AppendLine("<table>");
                   mailBody.AppendLine("<tr><td>Please sign form applicaion <a href='ht" + "tp://" + serverName + "/FrmApplicationReview.aspx?doc_id=" + Session["DOC_ID"].ToString() + "&email=" + tmpMail + "&page_count=" + page_count +"&paper_type=" + paper_type +"&signature=" + signature + "'>Click here.</a> </td></tr>");
                   mailBody.AppendLine("<tr><td><BR/></td></tr>");
                   mailBody.AppendLine("<tr><td><BR/>Best Regards,<td></tr>");
                   mailBody.AppendLine("<tr><td><BR/>" + mailForm + "</td></tr>");    
                   mailBody.AppendLine("</table><br />");

        

                   Model.Criteria.Document _DOC = new Model.Criteria.Document();
                   _DOC.send_mail_approve_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                   _DOC.secure_approve = tmpMail;
                   _DOC.doc_id = Session["DOC_ID"].ToString();
                   BLL.Upload _BLL = new BLL.Upload();
                   _BLL.Update_send_mail_approve_date(_DOC);


                   m_content = Session["CONTENT"].ToString();

 //Helper.Utility.SendEmail(mailTo, _mailCC.ToArray(), m_content,mailBody.ToString(),true,myCollection.ToArray(),mailForm);

                 



        Response.Redirect("DataDocument.aspx");


        }


   



          [System.Web.Services.WebMethod]
          public static List<Model.Account> getEmail()
          {
              BLL.Upload objBLL = new BLL.Upload();
              var list = new List<Model.Account>();
              list = objBLL.getEmail_Level1();
              return list;
          }


          [System.Web.Services.WebMethod]
          public static List<Model.Account> getEmailCC()
          {
              BLL.Upload objBLL = new BLL.Upload();
              var list = new List<Model.Account>();
              list = objBLL.getEmail();
              return list;
          }

        

    }
}