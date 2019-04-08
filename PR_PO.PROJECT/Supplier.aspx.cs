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
    public partial class Supplier : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        
     

            //if (Session["EMAIL"] == null)
            //{
            //    Response.Redirect("~/Authorize.aspx");
            //}


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
            string sql = "select * From Supplier order by supp_short_name";
    
            DataTable dt;
            dt = DB.ExecuteDataTable(sql);
            DB.Close();
            DB.Dispose();

            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();

            Session["DT"] = dt;


        }






        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {



 

        }

        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
     
      
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

        protected void ddlCustomer_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {


            DataTable customerByID;
            BLL.Upload _BLL = new BLL.Upload();

            customerByID = _BLL.GetCustomerByID(e.Value);
            this.RadGrid1.DataSource = customerByID;
            this.RadGrid1.DataBind();

        }

    

    }

}

