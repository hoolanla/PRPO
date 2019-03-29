using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Criteria
{
    public class Permission_Criteria
    {
        public string PRJ_Code { get; set; }
        public string UserAccount { get; set; }
        public string PAG_Code { get; set; }
    }

    public class UserMenu_Criteria
    {
        public string UserID { get; set; }
        public string UserAccount { get; set; }
        public string Dept { get; set; }
    }
}
