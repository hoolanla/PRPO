using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spire.Pdf;
using System.Text;
using System.Drawing;
using System.IO;

namespace PR_PO.PROJECT
{
    public partial class PdfToImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {





          //////  PdfDocument doc = new PdfDocument();
          //////  doc.LoadFromFile(@"C:\_CODE\WEB_APP\ODS\ODS\ODS.PROJECT\Pdf\SAMPLE INV (CONFIDENTIAL).pdf");
          //////  System.Drawing.Image bmp = doc.SaveAsImage(0);
          //////  System.Drawing.Image emf = doc.SaveAsImage(0, Spire.Pdf.Graphics.PdfImageType.Metafile);
          //////  System.Drawing.Image zoomImg = new Bitmap((int)(emf.Size.Width * 2), (int)(emf.Size.Height * 2));
          //////  using (Graphics g = Graphics.FromImage(zoomImg))
          //////  {
          //////      g.ScaleTransform(2.0f, 2.0f);
          //////      g.DrawImage(emf, new Rectangle(new Point(0, 0), emf.Size), new Rectangle(new Point(0, 0), emf.Size), GraphicsUnit.Pixel);
          //////  }
          //////  bmp.Save(@"C:\_CODE\WEB_APP\ODS\ODS\ODS.PROJECT\ImagesDoc/convertToBmp.jpeg");
          //////  System.Diagnostics.Process.Start(@"C:\_CODE\WEB_APP\ODS\ODS\ODS.PROJECT\ImagesDoc/convertToBmp.jpeg");
          ////////  emf.Save("convertToEmf.png");
          //////// System.Diagnostics.Process.Start(@"C:\_CODE\WEB_APP\ODS\ODS\ODS.PROJECT\Images/convertToEmf.png");
          //////  zoomImg.Save(@"C:\_CODE\WEB_APP\ODS\ODS\ODS.PROJECT\ImagesDoc/convertToZoom.PNG");
          //////  System.Diagnostics.Process.Start(@"C:\_CODE\WEB_APP\ODS\ODS\ODS.PROJECT\ImagesDoc/convertToZoom.PNG");


     PdfDocument pdf = new PdfDocument();
            //pdf.LoadFromFile(@"C:\_CODE\WEB_APP\ODS\ODS\ODS.PROJECT\Pdf\SAMPLE INV (CONFIDENTIAL).pdf");
     pdf.LoadFromFile(@"C:\_CODE\WEB_APP\ODS\ODS\ODS.PROJECT\Pdf\Sample Inv format.pdf");
      
         
            System.Drawing.Image jpg;
         
           for (int i = 0; i < pdf.Pages.Count; i++)
           {
              //jpg = pdf.SaveAsImage(i);
              //jpg.Save("C://" + i + ".jpg");

               System.Drawing.Image bmp = pdf.SaveAsImage(i);
               System.Drawing.Image emf = pdf.SaveAsImage(i, Spire.Pdf.Graphics.PdfImageType.Metafile);
               System.Drawing.Image zoomImg = new Bitmap((int)(emf.Size.Width * 2), (int)(emf.Size.Height * 2));
               using (Graphics g = Graphics.FromImage(zoomImg))
               {
                   g.ScaleTransform(2.0f, 2.0f);
                   g.DrawImage(emf, new Rectangle(new Point(0, 0), emf.Size), new Rectangle(new Point(0, 0), emf.Size), GraphicsUnit.Pixel);
               }
               bmp.Save(@"C:\_CODE\WEB_APP\ODS\ODS\ODS.PROJECT\PdfToImage/convertToBmp" + i + ".jpeg");
               System.Diagnostics.Process.Start(@"C:\_CODE\WEB_APP\ODS\ODS\ODS.PROJECT\PdfToImage/convertToBmp" + i + ".jpeg");
               //  emf.Save("convertToEmf.png");
               // System.Diagnostics.Process.Start(@"C:\_CODE\WEB_APP\ODS\ODS\ODS.PROJECT\Images/convertToEmf.png");
               zoomImg.Save(@"C:\_CODE\WEB_APP\ODS\ODS\ODS.PROJECT\PdfToImage/convertToZoom" + i + ".PNG");
               System.Diagnostics.Process.Start(@"C:\_CODE\WEB_APP\ODS\ODS\ODS.PROJECT\PdfToImage/convertToZoom" + i + ".PNG");
           }

            


        }




    }
}