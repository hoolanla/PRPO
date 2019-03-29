using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL
{
    public class Authorized
    {
    
        public List<Model.Permission_Read> Get_Permission(Model.Criteria.Permission_Criteria criteria)
        {
            //string connStr = Properties.Settings.Default.InventoryControl_ConnectionString;
            //SqlConnection conn = Common.DataHelper.getSQLServerConnectionObject(connStr);
            DataSet ds = new DataSet();
            //SqlDataAdapter adp = new SqlDataAdapter("usp_Permission_Read", conn);
            //adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            //adp.SelectCommand.Parameters.Add(new SqlParameter("@PRJ_Code", criteria.PRJ_Code));
            //adp.SelectCommand.Parameters.Add(new SqlParameter("@UserAccount", criteria.UserAccount));
            //adp.SelectCommand.Parameters.Add(new SqlParameter("@PAG_Code", criteria.PAG_Code));
          //  adp.Fill(ds);

            var result = ds.Tables[0].AsEnumerable().Select(s => new Model.Permission_Read
            {
                PRJ_ID = s.Field<int?>("PRJ_ID"),
                PRJ_Code = s.Field<string>("PRJ_Code"),
                PRJ_Name = s.Field<string>("PRJ_Name"),
                PRJ_Description = s.Field<string>("PRJ_Description"),
                PRJ_Active = s.Field<bool?>("PRJ_Active"),
                PRJ_SingleSignon = s.Field<bool?>("PRJ_SingleSignon"),
                PRJ_AppType = s.Field<string>("PRJ_AppType"),
                PRJ_CreatedDate = s.Field<System.DateTime?>("PRJ_CreatedDate"),
                PRJ_CreatedBy = s.Field<string>("PRJ_CreatedBy"),
                PAG_ID = s.Field<int?>("PRJ_ID"),
                PAG_PRJ_ID = s.Field<int?>("PRJ_ID"),
                PAG_Code = s.Field<string>("PAG_Code"),
                PAG_Name = s.Field<string>("PAG_Name"),
                PAG_Description = s.Field<string>("PAG_Description"),
                GRP_ID = s.Field<int?>("GRP_ID"),
                GRP_Name = s.Field<string>("GRP_Name"),
                GRP_Type = s.Field<string>("GRP_Type"),
                PMS_ID = s.Field<int?>("PMS_ID"),
                PMS_Create = s.Field<bool?>("PMS_Create"),
                PMS_Read = s.Field<bool?>("PMS_Read"),
                PMS_Update = s.Field<bool?>("PMS_Update"),
                PMS_Delete = s.Field<bool?>("PMS_Delete"),
                PMS_Report = s.Field<bool?>("PMS_Report"),
                GRU_LinkType = s.Field<string>("GRU_LinkType"),
                UserID = s.Field<string>("UserID"),
                UserAccount = s.Field<string>("UserAccount"),
                UserName = s.Field<string>("UserName"),
                Password = s.Field<string>("Password"),
                UserGroup = s.Field<string>("UserGroup"),
                Dept = s.Field<string>("Dept"),
                UserStatus = s.Field<string>("UserStatus")
            }).ToList();
            return result;
        }


        public List<Model.UserMenu_Read> Get_UserMenu(Model.Criteria.UserMenu_Criteria criteria)
        {
            //string connStr = Properties.Settings.Default.InventoryControl_ConnectionString;
            //SqlConnection conn = Common.DataHelper.getSQLServerConnectionObject(connStr);
            DataSet ds = new DataSet();
            //SqlDataAdapter adp = new SqlDataAdapter("usp_User_Menu", conn);
            //adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            //adp.SelectCommand.Parameters.Add(new SqlParameter("@bmUserID", criteria.UserID));
            //adp.SelectCommand.Parameters.Add(new SqlParameter("@bmUserAccount", criteria.UserAccount));
            //adp.SelectCommand.Parameters.Add(new SqlParameter("@bmDept", criteria.Dept));
            //adp.Fill(ds);

            var result = ds.Tables[0].AsEnumerable().Select(s => new Model.UserMenu_Read
            {
                PAG_ID = s.Field<int?>("PAG_ID"),
                PAG_Group = s.Field<int?>("PAG_GROUP"),
                PAG_Group_Name = s.Field<string>("PAG_Group_Name"),
                PAG_Name = s.Field<string>("PAG_Name"),
                Level = s.Field<int?>("Level"),
                PAG_CODE = s.Field<string>("PAG_CODE")
            }).ToList();

            return result;
        }

    }
}
