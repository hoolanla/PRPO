using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;


namespace PR_PO.PROJECT
{
    public partial class UploadSupplier : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void btnUpload_Click(object sender, EventArgs e)
        {

            UploadMain();

        }

        private bool UploadMain()
        {
            if (FileUpload1.HasFile)
            {
                string folderPath = Server.MapPath("~/EXCEL/");
                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(folderPath);
                }

                FileUpload fu = FileUpload1;
                String filename = fu.FileName;
                String fileExtension = System.IO.Path.GetExtension(fu.FileName).ToLower();
                String[] allowedExtensions = { ".xlsx" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        try
                        {
                            string job_id;
                        


                          //string s_newfilename = job_id + fileExtension;
                          fu.PostedFile.SaveAs(folderPath + filename);



                            ReadExcel(filename);


                            return true;
                        }
                        catch (Exception ex)
                        {
                            Response.Write("File could not be uploaded." + ex.Message);
                            return false;
                        }

                    }
                    else
                    {
                        Response.Write("Please upload xlsx file only.");
                        return false;
                    }

                }
                return true;
            }
            else
            {

                Response.Write("<script>alert('โปรดเลือกไฟล์ก่อน');</script>");
                return false;
            }

        }

        private bool ReadExcel(string filename)
        {

            FileInfo excel = new FileInfo(Server.MapPath("~/EXCEL/" + filename ));

            using (ExcelPackage package = new ExcelPackage(excel))
            {
                ExcelWorkbook workbook = package.Workbook;
                //*** Sheet 1
                ExcelWorksheet worksheet = workbook.Worksheets.First();

          

             //   job.job_name = worksheet.Cells["A3"].Text;


                List<Model.Supplier> lstSupp = new List<Model.Supplier>();
            

                int i = 2;

                do
                {

                    Model.Supplier m_supp = new Model.Supplier();

                    m_supp.supp_company = worksheet.Cells[i, 1].Text.Trim().Replace("'","");
                    m_supp.supp_code = worksheet.Cells[i, 2].Text.Trim().Replace("'", "");
                    m_supp.supp_name = worksheet.Cells[i, 3].Text.Trim().Replace("'", "");
                    m_supp.supp_short_name = worksheet.Cells[i, 4].Text.Trim().Replace("'", "");
                    m_supp.supp_address_1 = worksheet.Cells[i, 5].Text.Trim().Replace("'", "");
                    m_supp.supp_address_2 = worksheet.Cells[i, 6].Text.Trim().Replace("'", "");
                    m_supp.supp_contact_person = worksheet.Cells[i, 7].Text.Trim().Replace("'", "");
                    m_supp.supp_contact_position = worksheet.Cells[i, 8].Text.Trim().Replace("'", "");
                 
                    lstSupp.Add(m_supp);
                    i++;
                } while (worksheet.Cells[i, 1].Text != "");


                BLL.Upload _BLL = new BLL.Upload();
                _BLL.InsertSupplier(lstSupp);
      
            }

            return true;
        }


    }
}