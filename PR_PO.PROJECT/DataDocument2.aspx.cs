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

namespace PR_PO.PROJECT
{
    public partial class DataDocument2 : System.Web.UI.Page
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

       

        }


        private void BindGrid()
        {
            Class.clsDB DB = new Class.clsDB();
            string sql = "select doc_id,doc_name,content,create_by,create_date,secure_prepare,step1,step2,step3,step4,secure_approve,approve_problem From document order by doc_id desc";
            DataTable dt;
            dt = DB.ExecuteDataTable(sql);
            DB.Close();
            DB.Dispose();
            grid.DataSource = dt;
            Session["DT"] = dt;
            // grid.DataKeyNames = "doc_id";
            grid.DataBind();

        }


        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowIndex > -1)
            {
                if (e.Row.Cells[4].Text == Session["EMAIL"].ToString())
                {
                    //ImageButton imgPDF = (ImageButton)e.Row.Cells[6].Controls[0];
                    //imgPDF.ImageUrl = "Images/pdf.png";
                  
                    //ImageButton imgEML = (ImageButton)e.Row.Cells[8].Controls[0];
                    //imgEML.ImageUrl = "Images/email.png";
      
                }
                else
                {
                    //ImageButton imgPDF = (ImageButton)e.Row.Cells[6].Controls[0];
                    //imgPDF.ImageUrl = "Images/no.png";

                    //ImageButton imgEML = (ImageButton)e.Row.Cells[8].Controls[0];
                    //imgEML.ImageUrl = "Images/no.png";

                }



                ImageButton myImageButton2 = e.Row.FindControl("step2") as ImageButton;
                if (myImageButton2 != null)
                {
                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "step2")))
                    {
                        myImageButton2.ImageUrl = "Images/check.png";
                    }
                    else
                    {
                        myImageButton2.ImageUrl = "Images/wait.png";
                    }
                }


                ImageButton myImageButton3 = e.Row.FindControl("step3") as ImageButton;
                if (myImageButton3 != null)
                {
                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "step3")))
                    {
                        myImageButton3.ImageUrl = "Images/check.png";
                    }
                    else
                    {
                        myImageButton3.ImageUrl = "Images/wait.png";
                    }
                }

                ImageButton myImageButton4 = e.Row.FindControl("step4") as ImageButton;
                if (myImageButton4 != null)
                {
                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "step4")))
                    {
                        myImageButton4.ImageUrl = "Images/check.png";
                    }
                    else
                    {
                        myImageButton4.ImageUrl = "Images/wait.png";
                    }
                }


                if(e.Row.Cells[15].Text != "0")
                {
                    myImageButton4.ImageUrl = "Images/DeleteHS.png";

                }

                //if (e.Row.Cells[9].Text == "1")
                //{
                //    ImageButton imgPDF = (ImageButton)e.Row.Cells[9].Controls[0].Controls[0];
                //    imgPDF.ImageUrl = "Images/check.png";

               

                //}
                //else
                //{




                //    ImageButton imgPDF = (ImageButton)e.Row.Cells[9].Controls[0];
                //    imgPDF.ImageUrl = "Images/wait.png";

                //}




           



            }




            }

        protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "step2")
            {

                DataTable _dt;
                _dt = (DataTable)Session["DT"];
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = grid.Rows[index];

                if(selectedRow.Cells[4].Text != Session["EMAIL"].ToString())
                {
                    Model.Log L = new Model.Log();
                    Helper.Utility Log = new Helper.Utility();
                    L.content = "[Access denied!] Go to FrmApplicationPrepare.";
                    L.create_by = Session["EMAIL"].ToString();
                    Log.WriteLog(L);

                    Response.Write("<script>alert('คุณไม่มีสิทธิ์ Sign prepare.');</script>");

                }

                else if (_dt.Rows[index]["step2"].ToString() == "1")
                {

                    Response.Write("<script>alert('คุณไม่สามารถ Sign prepare ได้เนื่องจาก Sign ไปแล้ว');</script>");
                }

                else
                {


                    string page_count="";
                    string doc_id;
                    string signature;
                    string paper_type;

                    BLL.Upload _BLL = new BLL.Upload();
                  
                    doc_id = selectedRow.Cells[0].Text;
                    page_count = _BLL.get_Pagecount(doc_id);
                    paper_type = _BLL.Get_Paper_type(doc_id);
                    signature = Session["SIGNATURE"].ToString();
                    Model.Log L = new Model.Log();
                    Helper.Utility Log = new Helper.Utility();
                    L.content = "Go to FrmApplicationPrepare.";
                    L.create_by = Session["EMAIL"].ToString();
                    Log.WriteLog(L);
                    Response.Redirect("FrmApplicationPrepare.aspx?doc_id=" + doc_id + "&signature=" + signature + "&page_count=" + page_count + "&paper_type=" + paper_type);
                }
            }

            // APPROVE CHECK SECURE_APPROVE
            if (e.CommandName == "step4")
            {

                DataTable _dt;
                _dt = (DataTable)Session["DT"];
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = grid.Rows[index];
                if (selectedRow.Cells[14].Text != Session["EMAIL"].ToString())
                {
                    Model.Log L = new Model.Log();
                    Helper.Utility Log = new Helper.Utility();
                    L.content = "[Access denied!] Go to FrmApplicationApprove.";
                    L.create_by = Session["EMAIL"].ToString();
                    Log.WriteLog(L);

                    Response.Write("<script>alert('คุณไม่มีสิทธิ์ Sign Approve.');</script>");
                }


                else if (_dt.Rows[index]["step4"].ToString() == "1")
                {

                    Response.Write("<script>alert('คุณไม่สามารถ Sign approve ได้เนื่องจากได้ Sign ไปแล้ว');</script>");
                }

                else
                {
                    string doc_id;
                    string signature;
                    string page_count;
                    string paper_type;
                    doc_id = selectedRow.Cells[0].Text;

                    BLL.Upload _BLL = new BLL.Upload();
                    page_count = _BLL.get_Pagecount(doc_id);
                    paper_type = _BLL.Get_Paper_type(doc_id);

                    signature = Session["SIGNATURE"].ToString();
                    Model.Log L = new Model.Log();
                    Helper.Utility Log = new Helper.Utility();
                    L.content = "Go to FrmApplicationApprove.";
                    L.create_by = Session["EMAIL"].ToString();
                    Log.WriteLog(L);
                    Response.Redirect("FrmApplicationApprove.aspx?doc_id=" + doc_id + "&signature=" + signature + "&page_count=" + page_count + "&paper_type=" + paper_type);
                }
            }

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
                    string path = MapPath("AttachFiles/" + filename + ".zip");
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


            if (e.CommandName == "PDF_APPROVE")
            {
                string filename = e.CommandArgument.ToString();
                if (filename != "")
                {

                    Model.Log L = new Model.Log();
                    Helper.Utility Log = new Helper.Utility();

                    L.content = "View Approve " + filename + ".pdf";
                    L.create_by = Session["EMAIL"].ToString();
                    Log.WriteLog(L);
                    string path = MapPath("PdfApprove/" + filename + ".pdf");


                    if (!File.Exists(path))
                    {
                        Response.Write("<script>alert('ไฟล์นี้ยังไม่ได้ถูก Sign approve.');</script>");
                        return;
                    }



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


            if (e.CommandName == "PDF_PREPARE")
            {
                string filename = e.CommandArgument.ToString();
                if (filename != "")
                {

                    Model.Log L = new Model.Log();
                    Helper.Utility Log = new Helper.Utility();

                    L.content = "View Prepare " + filename + ".pdf";
                    L.create_by = Session["EMAIL"].ToString();
                    Log.WriteLog(L);
                    string path = MapPath("PdfPrepare/" + filename + ".pdf");
                    
                    if(!File.Exists(path))
                    {
                        Response.Write("<script>alert('ไฟล์นี้ยังไม่ได้ถูก Sign prepare.');</script>");
                        return;
                    }
                    
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


            //if (e.CommandName == "DELETE")
            //{

            //      int index = Convert.ToInt32(e.CommandArgument);
            //    GridViewRow selectedRow = grid.Rows[index];
            //    if(selectedRow.Cells[5].Text != Session["EMAIL"].ToString())
            //    {
            //        Model.Log L = new Model.Log();
            //        Helper.Utility Log = new Helper.Utility();
            //        L.content = "[Access denied!] Delete " + selectedRow.Cells[0].Text;
            //        L.create_by = Session["EMAIL"].ToString();
            //        Log.WriteLog(L);
                
            //    }
            //    else
            //    {
                

            //            Model.Log L = new Model.Log();
            //            Helper.Utility Log = new Helper.Utility();


            //            Model.Criteria.Document criteria = new Model.Criteria.Document();
            //            criteria.doc_id = selectedRow.Cells[0].Text;
            //            BLL.Upload BL = new BLL.Upload();
            //            BL.Delete_Document(criteria);


            //            L.content = "Delete Documnet  " + selectedRow.Cells[0].Text;
            //            L.create_by = Session["EMAIL"].ToString();
            //            Log.WriteLog(L);
            //            BindGrid();
            //    }


           
           // }

            if (e.CommandName == "step3")


            {

           DataTable _dt;
                _dt = (DataTable)Session["DT"];
                int idx = Convert.ToInt32(e.CommandArgument);
                GridViewRow Row = grid.Rows[idx];

                if (Row.Cells[4].Text != Session["EMAIL"].ToString())
                {
                    Model.Log L = new Model.Log();
                    Helper.Utility Log = new Helper.Utility();
                    L.content = "[Access denied!] Go to sendMailApprove.aspx.";
                    L.create_by = Session["EMAIL"].ToString();
                    Log.WriteLog(L);

                    Response.Write("<script>alert('คุณไม่มีสิทธิ์ส่งเมล์เพื่อ Approve.');</script>");
                }

                    
                else if (_dt.Rows[idx]["step2"].ToString() == "0")
                {

                    Response.Write("<script>alert('คุณไม่สามารถส่งเมล์ได้เนื่องจากเอกสารยังไม่ได้ Sign prepare.');</script>");
                }

                else if (_dt.Rows[idx]["step3"].ToString() == "1")
                {

                    Response.Write("<script>alert('คุณไม่สามารถส่งเมล์ได้เนื่องจากส่งไปแล้ว');</script>");
                }


                else
                {
                    string doc_id;
                    string email;
                    string content;

                    doc_id = Row.Cells[0].Text;
                    email = Session["EMAIL"].ToString();
                    content = Row.Cells[2].Text;

                    Model.Log L = new Model.Log();
                    Helper.Utility Log = new Helper.Utility();
                    L.content = "Go to SendMailApprove.aspx.";
                    L.create_by = Session["EMAIL"].ToString();
                    Log.WriteLog(L);
                    Response.Redirect("SendMailApprove.aspx?doc_id=" + doc_id + "&email=" + email + "&content=" + content);
                }
            }


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

        protected void grid_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

               grid.PageIndex = e.NewPageIndex;
               BindGrid();


        }

        protected void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


            int idx = Convert.ToInt32(e.RowIndex);
            GridViewRow Row = grid.Rows[idx];

     
        
            Model.Criteria.Document MDL = new Model.Criteria.Document();
            MDL.doc_id = Row.Cells[0].Text;

            BLL.Upload _BLL = new BLL.Upload();
            _BLL.Delete_Document(MDL);

            BindGrid();
            //BLL.job _BLL = new BLL.job();
            //_BLL.Delete_Job(job_id);

            //BindGrid();
        }

        protected void grid_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }

    

    

        }

   
    }
