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
using Ghostscript.NET.Rasterizer;


using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Threading;
using System.Data;




namespace PR_PO.PROJECT
{
    public partial class Upload : Page
    {

        Model.Criteria.Document Doc = new Model.Criteria.Document();
        string paper_type="L";

        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["EMAIL"] == null)
            {
                Response.Redirect("~/Authorize.aspx");
            }

            if (Session["LEVEL"].ToString() != "2")
            {
                Response.Write("<script>alert('คุณไม่มีสิทธิ์ Upload เอกสาร');</script>");
                Response.Redirect("~/DataDocument.aspx");
            }

            if (Request.QueryString["doc_id"] != null)
            {
                Session["DOC_DI"] = Request.QueryString["doc_id"];
               PO_Check.Checked = true;
            }


initCombo();

        }




        private void initCombo()
        {
            DataTable customer;
            BLL.Upload _BLL = new BLL.Upload();
            customer = _BLL.GetCustomer();
            this.ddlCustomer.AllowCustomText = true;
            this.ddlCustomer.MarkFirstMatch = true;
            this.ddlCustomer.DataSource = customer;
            
            this.ddlCustomer.DataBind();
        }



    

        private bool PdfToImage(string pdfName,string fileCurrentName)
        {
            int desired_x_dpi = 300;
            int desired_y_dpi = 300;

            int pageCount = 1; 
            string ServerPath = Server.MapPath(".\\");
            string pdfPath = Server.MapPath("~/Pdf/" + fileCurrentName + ".pdf");

            PdfSharp.Pdf.PdfDocument inputDocument = PdfReader.Open(pdfPath, PdfDocumentOpenMode.ReadOnly);
            int widthPage = 0;
            widthPage = (int)inputDocument.Pages[0].Width;
            if (widthPage > 800)
            {
                paper_type = "L";
            }
            else
            {
                paper_type = "P";
            }


       
            using (var rasterizer = new GhostscriptRasterizer())
            {
                rasterizer.Open(pdfPath);
                pageCount = rasterizer.PageCount;
                for (var pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)
                {

                    if (rasterizer.PageCount > 1)
                    {
                        var pageFilePath = Path.Combine(Server.MapPath("~/PdfToImage/"), fileCurrentName + "_" + (pageNumber) + ".PNG");
                        var img = rasterizer.GetPage(desired_x_dpi, desired_y_dpi, pageNumber);
                        img.Save(pageFilePath);
                    }
                    else
                    {
                        var pageFilePath = Path.Combine(Server.MapPath("~/PdfToImage/"), fileCurrentName + ".PNG");
                        var img = rasterizer.GetPage(desired_x_dpi, desired_y_dpi, pageNumber);
                        img.Save(pageFilePath);
                    }
                }
            }
           



            Model.Criteria.Document doc = new Model.Criteria.Document();
            doc.doc_id = fileCurrentName;
            doc.upload_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            doc.page_count = pageCount;
            doc.paper_type = paper_type;

            BLL.Upload _BLL = new BLL.Upload();


            _BLL.Update_upload_date(doc);
           
    
            // Write LOG

            Model.Log L = new Model.Log();
            Helper.Utility Log = new Helper.Utility();

            L.content = "Convert PDF to Image success.";
            L.create_by = Session["EMAIL"].ToString();

            Log.WriteLog(L);

            return true;
        }

  

        private  bool UploadMain()
        { 

            
           if (FileUpload1.HasFile)
            {
                string folderPath = Server.MapPath("~/Pdf/");
                //Check whether Directory (Folder) exists.


                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(folderPath);
                }

                FileUpload fu = FileUpload1;

                String fileExtension = System.IO.Path.GetExtension(fu.FileName).ToLower();
                String[] allowedExtensions = { ".pdf" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        try
                        {
                            ClsModule mdl = new ClsModule();
                            Doc.doc_id = mdl.getRuningNoDoc();
                            Doc.doc_name = fu.FileName;
                            Doc.create_by = Session["NAME"].ToString();
                            Doc.secure_prepare = Session["EMAIL"].ToString();
                            Doc.attach_file_name = Doc.doc_id + ".zip";
                            Doc.content = Content.Text;
                            Doc.pr_flag = 1;

                            string s_newfilename = Doc.doc_id + fileExtension;
                            fu.PostedFile.SaveAs(folderPath + s_newfilename);
                            Thread.Sleep(3000);
                            // Insert DB

                            string ret ;
                            BLL.Upload _BLL = new BLL.Upload();
                            string temp;

                            ret = _BLL.InsertDocument_step1(Doc);

          if(ret == "1")
          {

              Response.Write("1");
              Model.Log L = new Model.Log();
              Helper.Utility Log = new Helper.Utility();

              L.content = "Upload success.";
              L.create_by = Session["EMAIL"].ToString();

              Log.WriteLog(L);



              PdfToImage("", Doc.doc_id);
          }
          else
          {
              
              Model.Log L = new Model.Log();
              Helper.Utility Log = new Helper.Utility();

              L.content = "Can not upload.";
              L.create_by = Session["EMAIL"].ToString();

              Log.WriteLog(L);

          }



                        //    lblMessage.Text = Path.GetFileName(FileUpload1.FileName) + " has been uploaded.";
                            //imagepath = ImageSavedPath + s_newfilename;
                            return true;
                        }
                        catch (Exception ex)
                        {

                            Response.Write(ex.Message);
                            Response.Write("File could not be uploaded.");
                           Response.End();
                          //  return false;
                        }

                    }
                    else
                    {
                        Response.Write("Please upload pdf file only.");
                        return false;
                    }

                }
                return true;
            }
            else
            {

                Response.Write("<script>alert('โปรดเลือกเอกสารที่ต้องการ Approve ก่อน');</script>");
                return false;
            }

        }



        protected void UploadFile(object sender, EventArgs e)
        {

      
        }

        //protected void UploadMultipleFiles(object sender, EventArgs e)
        //{

        //    foreach (HttpPostedFile postedFile in FileUpload2.PostedFiles)
        //    {
        //        string fileName = Path.GetFileName(postedFile.FileName);
        //        postedFile.SaveAs(Server.MapPath("~/PDF/") + fileName);
        //    }
        //    lblSuccess.Text = string.Format("{0} files have been uploaded successfully.", FileUpload1.PostedFiles.Count);
        //}


        protected void btnUpload_Click(object sender, EventArgs e)
        {
           

            //// For test 

            // String  strCC = Request.Form["dlemailCC"].ToString();
            // string[] _mailCC = null;
            //if (strCC != null) 
            //{
            //    _mailCC = strCC.Split(',');
            //} 

            //////////

            //String tmpMail;
            //tmpMail = Request.Form["dlemail"].ToString();

            //       string[] mailTo = new string[] {tmpMail };
                   List<string> myCollection = new List<string>();
                   List<string> ZipCollection = new List<string>();
            ///  UploadMain PDF
        
           if( UploadMain())
            {
            HttpFileCollection fileCollection = Request.Files;
            for (int i = 1; i < fileCollection.Count; i++)
            {
                HttpPostedFile uploadfile = fileCollection[i];
                string fileName = Path.GetFileName(uploadfile.FileName);
                if (uploadfile.ContentLength > 0)
                {
                    uploadfile.SaveAs(Server.MapPath("~/AttachFiles/") + fileName);
                    myCollection.Add(Server.MapPath("~/AttachFiles/") + fileName);
                    lblMessage.Text += fileName + "  Saved  Successfully<br>";
            
                }
            }


               //Zip 

            using (Ionic.Zip.ZipFile compress = new Ionic.Zip.ZipFile())
            {
                string zipfilepath = Server.MapPath("~/AttachFiles/");
                compress.AddFiles(myCollection.ToArray(), Doc.doc_id);
                compress.Save(Server.MapPath("~/AttachFiles/") + Doc.doc_id + ".zip");
            }

Response.Redirect("DataDocument.aspx");


            }


      //     Helper.Utility.SendEmail("Test ODS",mailTo, _mailCC , myCollection.ToArray(),"This is link",false);

    //       Response.Redirect("Default.aspx");


        }




          [System.Web.Services.WebMethod]
          public static List<Model.Account> getEmail()
          {
              BLL.Upload objBLL = new BLL.Upload();
              var list = new List<Model.Account>();
              list = objBLL.getEmail();
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

          protected void Content_TextChanged(object sender, EventArgs e)
          {

          }

        

    }
}