using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PO
    {


        public string InsertDocument_po_step1(Model.PO_Document criteria)
        {

            Class.clsDB db = new Class.clsDB();
            string sql;
            sql = "Insert into po_document(";
            sql += "doc_id,";
            sql += "pr_doc_id,";
            sql += "doc_name,";
            sql += "create_by,";
            sql += "content,";
            sql += "secure_prepare) Values(";
            sql += "'" + criteria.doc_id + "',";
            sql += "'" + criteria.pr_doc_id + "',";
            sql += "'" + criteria.doc_name + "',";
            sql += "'" + criteria.create_by + "',";
            sql += "'" + criteria.content + "',";

            sql += "'" + criteria.secure_prepare + "')";



            int ret;
            ret = db.ExecuteNonQuery(sql);
            db.Close();

            return ret.ToString();
        }


        public int Update_Upload_date(Model.PO_Document criteria)
        {


            string sql;
            sql = "Update po_document SET upload_date = '" + criteria.upload_date + "',";
            sql += "step1='1',page_count = " + criteria.page_count + ",paper_type = '" + criteria.paper_type + "'";
            sql += " WHERE doc_id='" + criteria.doc_id + "'";

            Class.clsDB db = new Class.clsDB();
            int ret;
            ret = db.ExecuteNonQuery(sql);
            db.Close();
            return ret;


        }


        public int Update_sign_prepare_date(Model.PO_Document criteria)
        {


            string sql;
            sql = "Update po_document SET sign_prepare_date = '" + criteria.sign_prepare_date + "',step2=1 ";
            sql += " WHERE doc_id='" + criteria.doc_id + "'";

            Class.clsDB db = new Class.clsDB();
            int ret;
            ret = db.ExecuteNonQuery(sql);
            db.Close();
            return ret;


        }


        public int Update_sign_review_date(Model.PO_Document criteria)
        {


            string sql;
            sql = "Update po_document SET sign_prepare_date = '" + criteria.sign_prepare_date + "',step3=1 ";
            sql += " WHERE doc_id='" + criteria.doc_id + "'";

            Class.clsDB db = new Class.clsDB();
            int ret;
            ret = db.ExecuteNonQuery(sql);
            db.Close();
            return ret;


        }

        public int Update_AttachFile(Model.PO_Document criteria)
        {


            string sql;
            sql = "Update po_document SET attach_file_name = 0";
            sql += " WHERE doc_id='" + criteria.doc_id + "'";

            Class.clsDB db = new Class.clsDB();
            int ret;
            ret = db.ExecuteNonQuery(sql);
            db.Close();
            return ret;


        }

        public string Get_PageCount(string doc_id)
        {
            Class.clsDB db = new Class.clsDB();
            string sql;
            sql = " Select page_count From po_document where doc_id=" + doc_id;
            DataTable dt;
            dt = db.ExecuteDataTable(sql);
            db.Close();
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public int Update_sign_approve_problem(Model.PO_Document criteria)
        {


            string sql;
            sql = "Update po_document SET approve_problem = '" + criteria.approve_problem + "',step4=1 ";
            sql += " WHERE doc_id='" + criteria.doc_id + "'";

            Class.clsDB db = new Class.clsDB();
            int ret;
            ret = db.ExecuteNonQuery(sql);
            db.Close();
            return ret;

        }


        public int Update_sign_approve_date(Model.PO_Document criteria)
        {


            string sql;
            sql = "Update po_document SET sign_approve_date = '" + criteria.sign_approve_date + "',step4=1 ";
            sql += " WHERE doc_id='" + criteria.doc_id + "'";

            Class.clsDB db = new Class.clsDB();
            int ret;
            ret = db.ExecuteNonQuery(sql);
            db.Close();
            return ret;

        }
    }
}
