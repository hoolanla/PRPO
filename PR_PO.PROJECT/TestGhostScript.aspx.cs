using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ghostscript.NET.Rasterizer;
using System.Drawing.Imaging;
using System.IO;

namespace PR_PO.PROJECT
{
    public partial class TestGhostScript : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int desired_x_dpi = 300;
            int desired_y_dpi = 300;

            string inputPdfPath = @"C:\3page.pdf";
            string outputPath = @"C:\OUTPUT";

            using (var rasterizer = new GhostscriptRasterizer())
            {
                rasterizer.Open(inputPdfPath);

                for (var pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)
                {
                    var pageFilePath = Path.Combine(outputPath, string.Format("Page-{0}.png", pageNumber));

                    var img = rasterizer.GetPage(desired_x_dpi, desired_y_dpi, pageNumber);
                    img.Save(pageFilePath);

                }
            }




        }
    }
}