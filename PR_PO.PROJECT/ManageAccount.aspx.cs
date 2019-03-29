using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Helper;


namespace PR_PO.PROJECT
{
    public partial class ManageAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }




#region "Webmethod"

        [System.Web.Services.WebMethod]
        public static Json<Model.M_Account> getAccount(Model.Criteria.M_AccountCriteria criteria, Helper.JqGridParameter gridParam)
        {
            BLL.ManageAccount _BLL = new BLL.ManageAccount();

            int rsTotalRecord = 0;

            var result = _BLL.getAccount(criteria, ref rsTotalRecord);

            //turn Helper.UtilityHelper.ToJqGridResult<Models.M_Department_SearchLv1>(gridParam, result, rsTotalRecord);
            return Helper.Utility.ToJqGridResult<Model.M_Account>(gridParam, result);
        }


#endregion







    }
}