using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using PR_PO.PROJECT.Models;
using PR_PO.PROJECT.Class;
using System.Data;
using System.Web.Security;
using System.Web.SessionState;


namespace PR_PO.PROJECT.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //RegisterHyperLink.NavigateUrl = "Register";
            //OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            //var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            //if (!String.IsNullOrEmpty(returnUrl))
            //{
            //    RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            //}
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                String _userName, _password;
                _userName = UserName.Text.ToString();
                _password = Password.Text.ToString();
                String sql;
                DataTable dt;
                sql = "Select * from account where username='" + _userName + "' AND password='" + _password + "'";
                clsDB conn = new clsDB();
                dt = conn.ExecuteDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    Session["TITLE"] = dt.Rows[0]["title"].ToString();
                    Session["NAME"] = dt.Rows[0]["name"].ToString();
                    Session["SURNAME"] = dt.Rows[0]["surname"].ToString();
                    Session["LEVEL"] = dt.Rows[0]["level"].ToString();
                    Session["EMAIL"] = dt.Rows[0]["email"].ToString();
                    Session["SIGNATURE"] = dt.Rows[0]["signature"].ToString();

                    Model.Log L = new Model.Log();
                    Helper.Utility Log = new Helper.Utility();

                    L.content = "Log in success.";
                    L.create_by = Session["EMAIL"].ToString();
                    Log.WriteLog(L);
                 

                    Response.Redirect("../DataDocument.aspx");
                }
                else
                {
                    Model.Log L = new Model.Log();
                    Helper.Utility Log = new Helper.Utility();

                    L.content = "Log in fail.";
                    L.create_by = _userName;
                    Log.WriteLog(L);
                }
                    



            //    // Validate the user password
            //    var manager = new UserManager();
            //    ApplicationUser user = manager.Find(UserName.Text, Password.Text);
            //    if (user != null)
            //    {
            //        IdentityHelper.SignIn(manager, user, RememberMe.Checked);
            //        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            //    }
            //    else
            //    {
            //        FailureText.Text = "Invalid username or password.";
            //        ErrorMessage.Visible = true;
            //    }



            }
        }

        private bool ChkLogin()
        {





            return true;
        }
        


        protected void btnChange_Click(object sender, EventArgs e)
        {
            Response.Write("Button Clicked");
        }
    }
}