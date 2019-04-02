using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class PO
    {
        private DAL.PO _DAL = new DAL.PO();

               public string InsertDocument_po_step1(Model.PO_Document criteria)
        {
            return _DAL.InsertDocument_po_step1(criteria);
        }

         public int Update_Upload_date(Model.PO_Document criteria)
               {
                   return _DAL.Update_Upload_date(criteria);
               } 

          public int Update_AttachFile(Model.PO_Document criteria)
         {
             return _DAL.Update_AttachFile(criteria);
         }

         public int Update_sign_prepare_date(Model.PO_Document criteria)
         {
             return _DAL.Update_sign_prepare_date(criteria);
         } 

       public int Update_sign_review_date(Model.PO_Document criteria)
         {
             return _DAL.Update_sign_review_date(criteria);
         }

       public string Get_PageCount(string doc_id)
         {
             return _DAL.Get_PageCount(doc_id);
         }

       public int Update_sign_approve_problem(Model.PO_Document criteria)
               {

                   return _DAL.Update_sign_approve_problem(criteria);
               }  

       
        public int Update_sign_approve_date(Model.PO_Document criteria)
               {

                   return _DAL.Update_sign_approve_date(criteria);
               }
    }
}
