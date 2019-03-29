using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.VisualBasic;
using System.Configuration;

namespace PR_PO.PROJECT
{
    public partial class SiteMaster : MasterPage
    {

        public Boolean _readPermission = false;
        public Boolean _createPermission = false;
        public Boolean _updatePermission = false;
        public Boolean _deletePermission = false;
        public Boolean _reportPermission = false;
        public String sEnvironmentUserName = "";


        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut();
        }

        private void CheckPermission()
        {
            if ((Session["EnvironmentUserName"] == null))
            {
                String tmp;
                tmp = Request.LogonUserIdentity.Name.Replace("\\", "|");
                sEnvironmentUserName = tmp.Split('|')[1].ToString();
            }
            else
            {
                sEnvironmentUserName = Session["EnvironmentUserName"].ToString();
            }

            

            BLL.Authorized BLL = new BLL.Authorized();


            Model.Criteria.Permission_Criteria criteria = new Model.Criteria.Permission_Criteria();

            List<Model.Permission_Read> obj = new List<Model.Permission_Read>();

            string PAG_Code = getPageCode();

            criteria.UserAccount = sEnvironmentUserName;
            criteria.PRJ_Code = ConfigurationManager.AppSettings["PRJ_CODE"];
            criteria.PAG_Code = PAG_Code;

            obj = BLL.Get_Permission(criteria);


            if ((obj.Count > 0))
            {

                Session["User"] = obj[0].UserAccount;
                Session["UserName"] = obj[0].UserName;
                Session["GroupID"] = obj[0].GRP_ID;
                Session["GroupCode"] = obj[0].UserGroup;


                //  Session["UserAuthorization"] = dtUser;
                //  BLL.Log.WriteLog(_pageName, _methodName, "Log in successful", _ipAddress, Session["UserName"], Now, Session["UserName"], Now, Session["UserName"]);
                //Response.Redirect("../FrontEnd/Main.aspx")

            }
            else
            {
                //BLL.Log.WriteLog(_pageName, _methodName, "Log in failed", _ipAddress, txtUserID.Text, Now, "System Validation", Now, "System Validation")
                // BLL.Log.WriteLog(_pageName, _methodName, "Log in failed", _ipAddress, sEnvironmentUserName, Now, "System Validation", Now, "System Validation");
                Response.Redirect("NoAuthorized.aspx");
            }

        }

        public string getPageCode()
        {
            string pageName = Strings.Mid(Page.AppRelativeVirtualPath, Strings.InStr(Page.AppRelativeVirtualPath, "/") + 1, Strings.Len(Page.AppRelativeVirtualPath) - (Strings.InStr(Page.AppRelativeVirtualPath, "/")));
            if (Strings.InStr(pageName, "/") > 0)
            {
                pageName = Strings.Mid(pageName, pageName.LastIndexOf('/') + 2, Strings.Len(pageName) - pageName.LastIndexOf('/'));
            }
            pageName = Strings.Mid(pageName, Strings.InStr(pageName, "/") + 1, Strings.Len(pageName) - (Strings.InStr(pageName, "/")));
            string pageCode = pageName.Split('.')[0].ToString();
            return pageCode;
        }

    }

}