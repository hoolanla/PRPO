using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BLL
{
    public class Upload
    {
        private DAL.Upload _uplDAL = new DAL.Upload();

        public List<Model.Account> getEmail()
        {

          //  _uplDAL.getEmail();
            return _uplDAL.getEmail();
        }

                public int Update_AttachFile(Model.Criteria.Document criteria,int val)
        {
            return _uplDAL.Update_AttachFile(criteria,val);
        }

        public List<Model.Document> getAll_Document(Model.Criteria.Document criteria)
        {
            return _uplDAL.getAll_Document(criteria);
        }

        public List<Model.Account> getEmail_Level1()
        {
            return _uplDAL.getEmail_Level1();
        }


        public List<Model.Account> getEmail_Level2()
        {
            return _uplDAL.getEmail_Level2();
        }

        public List<Model.Account> getEmail_Level0()
        {
            return _uplDAL.getEmail_Level0();
        }


        public string InsertDocument_step1(Model.Criteria.Document criteria)
                   {
            return _uplDAL.InsertDocument_step1(criteria);
        }


          public bool InsertSupplier( List<Model.Supplier> lstSupp)
        {
            return _uplDAL.InsertSupplier(lstSupp);
        }


        public int Delete_Document(Model.Criteria.Document criteria)
        {
            return _uplDAL.Delete_Document(criteria);
        }


        public int Update_upload_date(Model.Criteria.Document criteria)
        {
            return _uplDAL.Update_Upload_date(criteria);
        }




       public int Update_sign_review_date(Model.Criteria.Document criteria)
        {
            return _uplDAL.Update_sign_review_date(criteria);
        }
        public int Update_sign_approve_date(Model.Criteria.Document criteria)
        {
            return _uplDAL.Update_sign_approve_date(criteria);
        }

        public int Update_sign_approve_problem(Model.Criteria.Document criteria)
        {
            return _uplDAL.Update_sign_approve_problem(criteria);
        }

        public int Update_send_mail_Request(Model.Criteria.Document criteria)
        {
            return _uplDAL.Update_send_mail_Request(criteria);
        }

        public int Update_send_mail_Review(Model.Criteria.Document criteria)
        {
            return _uplDAL.Update_send_mail_Review(criteria);
        }

        public string get_Pagecount(string doc_id)
        {
           return  _uplDAL.Get_PageCount(doc_id);
        }

        public string Get_Paper_type(string doc_id)
        {
            return _uplDAL.Get_Paper_type(doc_id);
        }

        public DataTable getDocument_By_DocId(string doc_id)
        {
            return _uplDAL.getDocument_By_DocId(doc_id);
        }

      public DataTable GetCustomer()
        {
            return _uplDAL.GetCustomer();
        }
 
    public DataTable GetCustomerByID(string id)
      {
          return _uplDAL.GetCustomerByID(id);
      }
    
    }
}
