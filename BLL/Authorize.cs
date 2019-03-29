using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Authorized
    {
        private DAL.Authorized _objDAL;

        public Authorized()
        {
            _objDAL = new DAL.Authorized();
        }

        public List<Model.Permission_Read> Get_Permission(Model.Criteria.Permission_Criteria criteria)
        {
            return _objDAL.Get_Permission(criteria);
        }

        public List<Model.UserMenu_Read> Get_UserMenu_Permission(Model.Criteria.UserMenu_Criteria criteria)
        {
            return _objDAL.Get_UserMenu(criteria);
        }

     

    }
}
