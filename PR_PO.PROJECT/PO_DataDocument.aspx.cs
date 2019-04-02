using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using Spire.Pdf;
using System.IO;
using Telerik.Web.UI;

namespace PR_PO.PROJECT
{
    public partial class PO_DataDocument : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
     
       
            if(Session["EMAIL"] == null )
            {
                Response.Redirect("~/Authorize.aspx");
            }


            if (!IsPostBack)
            {
                BindGrid();
            }
            else
            {
                BindGrid();
            }
       

        }


        private void BindGrid()
        {
            Class.clsDB DB = new Class.clsDB();
            string sql = "select po.doc_id,po.doc_name,po.content,po.create_by,po.create_date,po.secure_prepare,po.paper_type,po.page_count,";
            sql += " po.step1,po.step2,po.step3,po.step4,po.secure_approve,po.approve_problem,po.attach_file_name,pr.supplier_name";
            sql += " From po_document as po,document as pr";
            sql += " where po.step1 = 1 and po.doc_id = pr.doc_id  order by po.doc_id desc";
            DataTable dt;
            dt = DB.ExecuteDataTable(sql);
            DB.Close();
            DB.Dispose();

            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();
          
            Session["DT"] = dt;
            
           

        }


      
    
        private bool PdfToImage(string pdfName, string fileCurrentName)
        {

            PdfDocument pdf = new PdfDocument();
            string ServerPath = Server.MapPath(".\\");
            string pdfPath = Server.MapPath(".\\") + "PdfPrepare/" + fileCurrentName + ".pdf";
            pdf.LoadFromFile(@pdfPath);

            for (int i = 0; i < pdf.Pages.Count; i++)
            {

                System.Drawing.Image bmp = pdf.SaveAsImage(i);
                System.Drawing.Image emf = pdf.SaveAsImage(i, Spire.Pdf.Graphics.PdfImageType.Metafile);
                System.Drawing.Image zoomImg = new Bitmap((int)(emf.Size.Width * 2), (int)(emf.Size.Height * 2));
                using (Graphics g = Graphics.FromImage(zoomImg))
                {
                    g.ScaleTransform(2.0f, 2.0f);
                    g.DrawImage(emf, new Rectangle(new Point(0, 0), emf.Size), new Rectangle(new Point(0, 0), emf.Size), GraphicsUnit.Pixel);
                }
                //bmp.Save(@ServerPath + "PdfToImage/" + fileCurrentName + ".jpeg");
                //System.Diagnostics.Process.Start(@ServerPath + "PdfToImage/" + fileCurrentName + ".jpeg");
                //  emf.Save("convertToEmf.png");
                // System.Diagnostics.Process.Start(@"C:\_CODE\WEB_APP\ODS\ODS\ODS.PROJECT\Images/convertToEmf.png");
                zoomImg.Save(@ServerPath + "/PdfToImagePrepare/" + fileCurrentName + ".PNG");
              //  System.Diagnostics.Process.Start(@ServerPath + "PdfToImagePrepare/" + fileCurrentName + ".PNG");
            }
            return true;
        }


        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            string path = MapPath("Pdf/" + filePath + ".pdf");
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(path));
            Response.WriteFile(path);
            Response.End();
        }

  


        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {




            GridDataItem item = null;
            if (e.Item is GridDataItem)
            {
                item = (GridDataItem)e.Item;


                LinkButton PO_PDF = item.FindControl("PO_PDF") as LinkButton;
                if (PO_PDF != null)
                {
                    mgr1.RegisterPostBackControl(PO_PDF);
                }


                LinkButton PDF_Approve = item.FindControl("PDF_Approve") as LinkButton;
                if (PDF_Approve != null)
                {
                    mgr1.RegisterPostBackControl(PDF_Approve);
                }


                LinkButton PO_PDF_APROVE = item.FindControl("PO_PDF_APROVE") as LinkButton;
                if (PO_PDF_APROVE != null)
                {
                    mgr1.RegisterPostBackControl(PO_PDF_APROVE);
                }

                

                LinkButton lnkDownload = item.FindControl("lnkDownload") as LinkButton;
                if (lnkDownload != null)
                {
                    mgr1.RegisterPostBackControl(lnkDownload);
                }

                System.Web.UI.WebControls.Image img_complete = item.FindControl("img_complete") as System.Web.UI.WebControls.Image;
                if (img_complete != null)
                {

                    if (Convert.ToBoolean(DataBinder.Eval(item.DataItem, "attach_file_name")))
                    {
                        img_complete.ImageUrl = "Images/pdf.png";
                    }
                    else
                    {
                        img_complete.ImageUrl = "Images/wait.png";
                    }
                }


                System.Web.UI.WebControls.Image img_zip = item.FindControl("img_zip") as System.Web.UI.WebControls.Image;
                if (img_zip != null)
                {

                    if (Convert.ToBoolean(DataBinder.Eval(item.DataItem, "attach_file_name")))
                    {
                        img_zip.ImageUrl = "Images/zip.png";
                    }
                    else
                    {
                        img_zip.ImageUrl = "Images/wait.png";
                    }
                }







                ImageButton myImageButton2 = item.FindControl("step2") as ImageButton;
                if (myImageButton2 != null)
                {
                    if (Convert.ToBoolean(DataBinder.Eval(item.DataItem, "step2")))
                    {
                        myImageButton2.ImageUrl = "Images/check.png";
                        myImageButton2.Height = 20;
                        myImageButton2.Width = 20;

                    }
                    else
                    {
                        myImageButton2.ImageUrl = "Images/wait.png";
                        myImageButton2.Height = 20;
                        myImageButton2.Width = 20;
                    }
                }


                ImageButton myImageButton3 = item.FindControl("step3") as ImageButton;
                if (myImageButton3 != null)
                {
                    if (Convert.ToBoolean(DataBinder.Eval(item.DataItem, "step3")))
                    {
                        myImageButton3.ImageUrl = "Images/check.png";
                        myImageButton3.Height = 20;
                        myImageButton3.Width = 20;

                    }
                    else
                    {
                        myImageButton3.ImageUrl = "Images/wait.png";
                        myImageButton3.Height = 20;
                        myImageButton3.Width = 20;
                    }
                }


                ImageButton myImageButton4 = item.FindControl("step4") as ImageButton;
                if (myImageButton4 != null)
                {
                    if (Convert.ToBoolean(DataBinder.Eval(item.DataItem, "step4")))
                    {
                        myImageButton4.ImageUrl = "Images/check.png";
                        myImageButton4.Height = 20;
                        myImageButton4.Width = 20;

                    }
                    else
                    {
                        myImageButton4.ImageUrl = "Images/wait.png";
                        myImageButton4.Height = 20;
                        myImageButton4.Width = 20;
                    }
                }
            }

   

        }

        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

            GridDataItem dataItem = null;
            if (e.Item is GridDataItem)
            {
                dataItem = e.Item as GridDataItem;

                int selectedRowIndex = dataItem.RowIndex;

            } 



            if (e.CommandName == "PO_PDF") 


            {


                 string filename = e.CommandArgument.ToString();
                if (filename != "")
                {

                    Model.Log L = new Model.Log();
                    Helper.Utility Log = new Helper.Utility();

                    L.content = "View PO " + filename + ".pdf";
                    L.create_by = Session["EMAIL"].ToString();
                    Log.WriteLog(L);
                    string path = MapPath("PO_Pdf/" + filename + ".pdf");

                    //if (!File.Exists(path))
                    //{
                    //    Response.Write("<script>alert('ไฟล์นี้ยังไม่ได้ถูก Sign prepare.');</script>");
                    //    return;
                    //}

                    byte[] bts = System.IO.File.ReadAllBytes(path);
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.AddHeader("Content-Type", "Application/octet-stream");
                    Response.AddHeader("Content-Length", bts.Length.ToString());
                    Response.AddHeader("Content-Disposition", "attachment;   filename=" + filename + ".pdf");
                    Response.BinaryWrite(bts);
                    Response.Flush();
                    Response.End();
                }
            }


                       if (e.CommandName == "PDF_Approve") 
                       {

                string filename = e.CommandArgument.ToString();
                if (filename != "")
                {

                    Model.Log L = new Model.Log();
                    Helper.Utility Log = new Helper.Utility();

                    L.content = "View PR Approve " + filename + ".pdf";
                    L.create_by = Session["EMAIL"].ToString();
                    Log.WriteLog(L);
                    string path = MapPath("PdfApprove/" + filename + ".pdf");

                    //if (!File.Exists(path))
                    //{
                    //    Response.Write("<script>alert('ไฟล์นี้ยังไม่ได้ถูก Sign prepare.');</script>");
                    //    return;
                    //}

                    byte[] bts = System.IO.File.ReadAllBytes(path);
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.AddHeader("Content-Type", "Application/octet-stream");
                    Response.AddHeader("Content-Length", bts.Length.ToString());
                    Response.AddHeader("Content-Disposition", "attachment;   filename=" + filename + ".pdf");
                    Response.BinaryWrite(bts);
                    Response.Flush();
                    Response.End();
                }
            }



                       if (e.CommandName == "PO_PDF_APPROVE")
                       {

                           string filename = e.CommandArgument.ToString();
                           try
                           {
                               if (filename != "")
                               {

                                   Model.Log L = new Model.Log();
                                   Helper.Utility Log = new Helper.Utility();

                                   L.content = "View PO Approve " + filename + ".pdf";
                                   L.create_by = Session["EMAIL"].ToString();
                                   Log.WriteLog(L);
                                   string path = MapPath("PO_PdfApprove/" + filename + ".pdf");

                                   //if (!File.Exists(path))
                                   //{
                                   //    Response.Write("<script>alert('ไฟล์นี้ยังไม่ได้ถูก Sign prepare.');</script>");
                                   //    return;
                                   //}

                                   byte[] bts = System.IO.File.ReadAllBytes(path);
                                   Response.Clear();
                                   Response.ClearHeaders();
                                   Response.AddHeader("Content-Type", "Application/octet-stream");
                                   Response.AddHeader("Content-Length", bts.Length.ToString());
                                   Response.AddHeader("Content-Disposition", "attachment;   filename=" + filename + ".pdf");
                                   Response.BinaryWrite(bts);
                                   Response.Flush();
                                   Response.End();
                               }
                           }
                           catch(FileNotFoundException Exception)
                           {

                           }
                       }





            // Attath file
            if (e.CommandName == "cmd")
            {
                string filename = e.CommandArgument.ToString();
                if (filename != "")
                {

                    Model.Log L = new Model.Log();
                    Helper.Utility Log = new Helper.Utility();

                    L.content = "Download attach file name " + filename + ".zip";
                    L.create_by = Session["EMAIL"].ToString();
                    Log.WriteLog(L);
                    string path = MapPath("PO_AttachFiles/" + filename + ".zip");
                    byte[] bts = System.IO.File.ReadAllBytes(path);
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.AddHeader("Content-Type", "Application/octet-stream");
                    Response.AddHeader("Content-Length", bts.Length.ToString());
                    Response.AddHeader("Content-Disposition", "attachment;   filename=" + filename + ".zip");
                    Response.BinaryWrite(bts);
                    Response.Flush();
                    Response.End();
                }
            }

            if (e.CommandName == "step2")
            {



                int idx = Convert.ToInt32(e.CommandArgument);


                if (Session["LEVEL"].ToString() != "2")
                {
                    Model.Log L = new Model.Log();
                    Helper.Utility Log = new Helper.Utility();
                    L.content = "[Access denied!] Go to sendMailRequest.aspx.";
                    L.create_by = Session["EMAIL"].ToString();
                    Log.WriteLog(L);

                    Response.Write("<script>alert('คุณไม่มีสิทธิ์ส่งเมล์เพื่อ Request.');</script>");
                }



                else if (e.CommandArgument.ToString() == "1")
                {

                    Response.Write("<script>alert('คุณไม่สามารถส่งเมล์ได้เนื่องจากส่งไปแล้ว');</script>");
                }


                else
                {
                    string doc_id;
                    string email;
                    string content;

                    doc_id = dataItem.Cells[2].Text;
                    email = Session["EMAIL"].ToString();
                    content = dataItem.Cells[4].Text;

                    Model.Log L = new Model.Log();
                    Helper.Utility Log = new Helper.Utility();
                    L.content = "Go PO_SendMailRequest.aspx.";
                    L.create_by = Session["EMAIL"].ToString();
                    Log.WriteLog(L);
                    Response.Redirect("PO_SendMailRequest.aspx?doc_id=" + doc_id + "&email=" + email + "&content=" + content);
                }
            }


            if (e.CommandName == "step3")
            {

                int idx = Convert.ToInt32(e.CommandArgument);
                if (Session["LEVEL"].ToString() != "2")
                {
                    Model.Log L = new Model.Log();
                    Helper.Utility Log = new Helper.Utility();
                    L.content = "[Access denied!] Go to sendMailReview.aspx.";
                    L.create_by = Session["EMAIL"].ToString();
                    Log.WriteLog(L);

                    Response.Write("<script>alert('คุณไม่มีสิทธิ์ส่งเมล์เพื่อ Review.');</script>");
                }

                //else if (  e.CommandArgument.ToString() == "1")
                //{
                //    Response.Write("<script>alert('คุณไม่สามารถส่งเมล์ได้เนื่องจากส่งไปแล้ว');</script>");
                //}

                else
                {
                    string doc_id;
                    string email;
                    string content;

                    doc_id = dataItem.Cells[2].Text;
                    email = Session["EMAIL"].ToString();
                    content = dataItem.Cells[4].Text;

                    Model.Log L = new Model.Log();
                    Helper.Utility Log = new Helper.Utility();
                    L.content = "Go to PO_SendMailReview.aspx.";
                    L.create_by = Session["EMAIL"].ToString();
                    Log.WriteLog(L);
                    Response.Redirect("PO_SendMailReview.aspx?doc_id=" + doc_id + "&email=" + email + "&content=" + content);
                }
            }

            if (e.CommandName == "step4")
            {

                int idx = Convert.ToInt32(e.CommandArgument);
                if (Session["LEVEL"].ToString() != "2")
                {
                    Model.Log L = new Model.Log();
                    Helper.Utility Log = new Helper.Utility();
                    L.content = "[Access denied!] Go to sendMailReview.aspx.";
                    L.create_by = Session["EMAIL"].ToString();
                    Log.WriteLog(L);

                    Response.Write("<script>alert('คุณไม่มีสิทธิ์ส่งเมล์เพื่อ Review.');</script>");
                }

                //else if (  e.CommandArgument.ToString() == "1")
                //{
                //    Response.Write("<script>alert('คุณไม่สามารถส่งเมล์ได้เนื่องจากส่งไปแล้ว');</script>");
                //}

                else
                {
                    string doc_id;
                    string email;
                    string content;

                    doc_id = dataItem.Cells[2].Text;
                    email = Session["EMAIL"].ToString();
                    content = dataItem.Cells[4].Text;

                    Model.Log L = new Model.Log();
                    Helper.Utility Log = new Helper.Utility();
                    L.content = "Go to PO_SendMailApprove.aspx.";
                    L.create_by = Session["EMAIL"].ToString();
                    Log.WriteLog(L);
                    Response.Redirect("PO_SendMailApprove.aspx?doc_id=" + doc_id + "&email=" + email + "&content=" + content);
                }
            }








        }

        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

        }

        protected void RadGrid1_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.RadGrid1.CurrentPageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)Session["DT"];
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();
        }

        protected void RadGrid1_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            RadGrid1.CurrentPageIndex = e.NewPageSize;
            DataTable dt = (DataTable)Session["DT"];
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();


        }

    

    

        }

   
    }
