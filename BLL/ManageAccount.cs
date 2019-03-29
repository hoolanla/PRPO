using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace BLL
{
   public  class ManageAccount
    {

       private DAL.ManageAccount _objDAL = new DAL.ManageAccount();

       public List<Model.M_Account> getAccount(Model.Criteria.M_AccountCriteria criteria, ref int rsTotalRecord)
       {
           return _objDAL.getAccount(criteria, ref rsTotalRecord);
       }

       public DataTable getAccount_By_Email(Model.Criteria.M_AccountCriteria criteria)
       {
           return _objDAL.getAccount_By_Email(criteria);
       }

       public DataTable getAccount_Level1()
       {
           return _objDAL.getAccount_Level1();
       }


       public int UpdateAccount(Model.Criteria.M_AccountCriteria criteria)
       {
           return _objDAL.UpdateAccount(criteria);
       }

       public int InsertAccount(Model.Criteria.M_AccountCriteria criteria)
       {
           return _objDAL.InsertAccount(criteria);
       }


       public string Get_Signature_By_Email(string email)
       {
           return _objDAL.Get_Signature_By_Email(email);
       }



    }
}
