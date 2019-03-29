using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Permission_Read
    {
        public int? PRJ_ID { get; set; }
        public string PRJ_Code { get; set; }
        public string PRJ_Name { get; set; }
        public string PRJ_Description { get; set; }
        public bool? PRJ_Active { get; set; }
        public bool? PRJ_SingleSignon { get; set; }
        public string PRJ_AppType { get; set; }
        public System.DateTime? PRJ_CreatedDate_Start { get; set; }
        public System.DateTime? PRJ_CreatedDate { get; set; }
        public string PRJ_CreatedBy { get; set; }
        public int? PAG_ID { get; set; }
        public int? PAG_PRJ_ID { get; set; }
        public string PAG_Code { get; set; }
        public string PAG_Name { get; set; }
        public string PAG_Description { get; set; }
        public int? GRP_ID { get; set; }
        public string GRP_Name { get; set; }
        public string GRP_Type { get; set; }
        public int? PMS_ID { get; set; }
        public bool? PMS_Create { get; set; }
        public bool? PMS_Read { get; set; }
        public bool? PMS_Update { get; set; }
        public bool? PMS_Delete { get; set; }
        public bool? PMS_Report { get; set; }
        public string GRU_LinkType { get; set; }
        public string UserID { get; set; }
        public string UserAccount { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserGroup { get; set; }
        public string Dept { get; set; }
        public string UserStatus { get; set; }
    }


    public class UserMenu_Read
    {
        public int? PAG_ID { get; set; }
        public int? PAG_Group { get; set; }
        public string PAG_Group_Name { get; set; }
        public string PAG_Name { get; set; }
        public int? Level { get; set; }
        public string PAG_CODE { get; set; }
    }
}
