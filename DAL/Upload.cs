using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace DAL
{
    public class Upload
    {



        public DataTable GetCustomer()
        {
            DataTable table;
            try
            {
                DataSet set = new DataSet();
                string strSQL = "Select * From supplier order by supp_short_name ";
              Class.clsDB sdb1 = new Class.clsDB();
                set = sdb1.ExecuteDataSet(strSQL);
                sdb1.Close();
                table = set.Tables[0];
            }
            catch (Exception exception1)
            {
                throw exception1;
            }
            return table;
        }



        public DataTable GetCustomerByID(string id)
        {
            DataTable table;
            try
            {
                if (id == "")
                {
                    id = "0";
                }
                DataSet set = new DataSet();
                string strSQL = "Select * From supplier Where supp_code = " + id + " order by supp_short_name ";
                Class.clsDB sdb1 = new Class.clsDB();
                set = sdb1.ExecuteDataSet(strSQL);
                sdb1.Close();
                table = set.Tables[0];
            }
            catch (Exception exception1)
            {
                throw exception1;
            }
            return table;
        }


        public List<Model.Document> getAll_Document(Model.Criteria.Document criteria)
        {
            //string connStr = Properties.Settings.Default.InventoryControl_ConnectionString;
            //SqlConnection conn = Common.DataHelper.getSQLServerConnectionObject(connStr);
            DataSet ds = new DataSet();
            String sql;
            sql = "Select * From document where doc_id=" + criteria.doc_id ;
            Class.clsDB db = new Class.clsDB();
            ds = db.ExecuteDataSet(sql);
            db.Close();
            //SqlDataAdapter adp = new SqlDataAdapter("usp_Permission_Read", conn);
            //adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            //adp.SelectCommand.Parameters.Add(new SqlParameter("@PRJ_Code", criteria.PRJ_Code));
            //adp.SelectCommand.Parameters.Add(new SqlParameter("@UserAccount", criteria.UserAccount));
            //adp.SelectCommand.Parameters.Add(new SqlParameter("@PAG_Code", criteria.PAG_Code));
            //  adp.Fill(ds);

            return ds.Tables[0].AsEnumerable().Select(s => new Model.Document
            {
                doc_id_int = s.Field<int>("doc_id"),
                doc_name = s.Field<string>("doc_name"),
                secure_approve = s.Field<string>("secure_approve"),
                secure_prepare = s.Field<string>("secure_prepare"),
                approve_problem = s.Field<string>("approve_problem"),
                content = s.Field<string>("content")
            }).ToList();

        }

        public List<Model.Account> getEmail()
        {
      
            DataSet ds = new DataSet();
            String sql;
            sql = "Select * From account";
            Class.clsDB db = new Class.clsDB();
            ds = db.ExecuteDataSet(sql);
     
      return ds.Tables[0].AsEnumerable().Select(s => new Model.Account
            {
                Title = s.Field<string>("Title"),
                Name = s.Field<string>("Name"),
                Surname = s.Field<string>("Surname"),
                Email = s.Field<string>("Email"),
            }).ToList();
  
        }



        public List<Model.Account> getEmail_Level1()
        {
       
            DataSet ds = new DataSet();
            String sql;
            sql = "Select * From account where level='1'";
            Class.clsDB db = new Class.clsDB();
            ds = db.ExecuteDataSet(sql);
            db.Close();
       

            return ds.Tables[0].AsEnumerable().Select(s => new Model.Account
            {
                Title = s.Field<string>("Title"),
                Name = s.Field<string>("Name"),
                Surname = s.Field<string>("Surname"),
                Email = s.Field<string>("Email"),
            }).ToList();

        }


        public List<Model.Account> getEmail_Level2()
        {
            //string connStr = Properties.Settings.Default.InventoryControl_ConnectionString;
            //SqlConnection conn = Common.DataHelper.getSQLServerConnectionObject(connStr);
            DataSet ds = new DataSet();
            String sql;
            sql = "Select * From account where level='2'";
            Class.clsDB db = new Class.clsDB();
            ds = db.ExecuteDataSet(sql);
            db.Close();
            //SqlDataAdapter adp = new SqlDataAdapter("usp_Permission_Read", conn);
            //adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            //adp.SelectCommand.Parameters.Add(new SqlParameter("@PRJ_Code", criteria.PRJ_Code));
            //adp.SelectCommand.Parameters.Add(new SqlParameter("@UserAccount", criteria.UserAccount));
            //adp.SelectCommand.Parameters.Add(new SqlParameter("@PAG_Code", criteria.PAG_Code));
            //  adp.Fill(ds);

            return ds.Tables[0].AsEnumerable().Select(s => new Model.Account
            {
                Title = s.Field<string>("Title"),
                Name = s.Field<string>("Name"),
                Surname = s.Field<string>("Surname"),
                Email = s.Field<string>("Email"),
            }).ToList();

        }


        public List<Model.Account> getEmail_Level0()
        {
    
            DataSet ds = new DataSet();
            String sql;
            sql = "Select * From account where level='0'";
            Class.clsDB db = new Class.clsDB();
            ds = db.ExecuteDataSet(sql);
            db.Close();
       

            return ds.Tables[0].AsEnumerable().Select(s => new Model.Account
            {
                Title = s.Field<string>("Title"),
                Name = s.Field<string>("Name"),
                Surname = s.Field<string>("Surname"),
                Email = s.Field<string>("Email"),
            }).ToList();

        }

        public bool InsertSupplier( List<Model.Supplier> lstSupp)
        {

                int k;
                k = lstSupp.Count();
            
                for (int i = 0; i < k; i++)
                {

                    string sql = null;
                    Model.Supplier m_supp = new Model.Supplier();
                    m_supp = (Model.Supplier)lstSupp[i];

                    sql += "Insert into supplier(supp_company,";
                    sql += "supp_code,";
                    sql += "supp_name,";
                    sql += "supp_short_name,";
                    sql += "supp_address_1,";
                    sql += "supp_address_2,";
                    sql += "supp_contact_person,";
                    sql += "supp_contact_position) Values('";
                    sql += m_supp.supp_company + "','";
                    sql += m_supp.supp_code + "','";
                    sql += m_supp.supp_name + "','";
                    sql += m_supp.supp_short_name + "','";
                    sql += m_supp.supp_address_1 + "','";
                    sql += m_supp.supp_address_2 + "','";
                    sql += m_supp.supp_contact_person + "','";
                    sql += m_supp.supp_contact_position + "')";



                    try
                    {
                        Class.clsDB db = new Class.clsDB();
                        int ret;
                        ret = db.ExecuteNonQuery(sql);
                        db.Close();
                    }
                    catch
                    {

                    }


                }
      


            return true;
        }




        public string InsertDocument_step1(Model.Criteria.Document criteria)
        {

            Class.clsDB db = new Class.clsDB();
            string sql;
            sql = "Insert into document(";
            sql += "doc_id,";
            sql += "doc_name,";
            sql += "create_by,";
            sql += "content,";
            sql += "supplier_id,";
            sql += "supplier_name,";
            sql += "attach_file_name,";
            sql += "comment,";
            sql += "secure_prepare) Values(";
            sql += "'" + criteria.doc_id + "',";
            sql += "'" + criteria.doc_name + "',";
            sql += "'" + criteria.create_by + "',";
            sql += "'" + criteria.content + "',";
            sql += "'" + criteria.suplier_id + "',";
            sql += "'" + criteria.supplier_name + "',";
            sql += "'" + criteria.attach_file_name + "',";
            sql += "'" + criteria.comment + "',";
            sql += "'" + criteria.secure_prepare + "')";

            
            
            int ret;
            ret = db.ExecuteNonQuery(sql);
            db.Close();

           return ret.ToString();
        }





        public int Update_Upload_date(Model.Criteria.Document criteria)
        {


            string sql;
            sql = "Update document SET upload_date = '" + criteria.upload_date + "',";
            sql += "step1='1',page_count = " + criteria.page_count + ",paper_type = '" + criteria.paper_type + "'";
            sql += " WHERE doc_id='" + criteria.doc_id + "'";

            Class.clsDB db = new Class.clsDB();
            int ret;
            ret = db.ExecuteNonQuery(sql);
            db.Close();
            return ret;
  

        }


        public int Update_AttachFile(Model.Criteria.Document criteria,int val)
        {


            string sql;
            sql = "Update document SET attach_file_name = " + val;
            sql += " WHERE doc_id='" + criteria.doc_id + "'";

            Class.clsDB db = new Class.clsDB();
            int ret;
            ret = db.ExecuteNonQuery(sql);
            db.Close();
            return ret;


        }

        public int Update_sign_review_date(Model.Criteria.Document criteria)
        {


            string sql;
            sql = "Update document SET sign_prepare_date = '" + criteria.sign_prepare_date + "',step2=1 ";
            sql += " WHERE doc_id='" + criteria.doc_id + "'";

            Class.clsDB db = new Class.clsDB();
            int ret;
            ret = db.ExecuteNonQuery(sql);
            db.Close();
            return ret;


        }

        public int Update_sign_approve_date(Model.Criteria.Document criteria)
        {


            string sql;
            sql = "Update document SET sign_approve_date = '" + criteria.sign_approve_date + "',step4=1 ";
            sql += " WHERE doc_id='" + criteria.doc_id + "'";

            Class.clsDB db = new Class.clsDB();
            int ret;
            ret = db.ExecuteNonQuery(sql);
            db.Close();
            return ret;

        }


        public int Update_sign_approve_problem(Model.Criteria.Document criteria)
        {


            string sql;
            sql = "Update document SET approve_problem = '" + criteria.approve_problem + "',step4=1 ";
            sql += " WHERE doc_id='" + criteria.doc_id + "'";

            Class.clsDB db = new Class.clsDB();
            int ret;
            ret = db.ExecuteNonQuery(sql);
            db.Close();
            return ret;

        }

        public int Delete_Document(Model.Criteria.Document criteria)
        {


            string sql;
            sql = "Delete From document ";
            sql += " WHERE doc_id='" + criteria.doc_id + "'";

            Class.clsDB db = new Class.clsDB();
            int ret;
            ret = db.ExecuteNonQuery(sql);
            db.Close();
           
            return ret;

        }

        public int Update_send_mail_Request(Model.Criteria.Document criteria)
        {


            string sql;
            sql = "Update document SET send_mail_approve_date = '" + criteria.send_mail_approve_date + "',step2=1,secure_approve='" + criteria.secure_approve + "'";
            sql += " WHERE doc_id='" + criteria.doc_id + "'";

            Class.clsDB db = new Class.clsDB();
            int ret;
            ret = db.ExecuteNonQuery(sql);
            db.Close();
            return ret;

        }

        public int Update_send_mail_Review(Model.Criteria.Document criteria)
        {


            string sql;
            sql = "Update document SET send_mail_approve_date = '" + criteria.send_mail_approve_date + "',step3=1,secure_approve='" + criteria.secure_approve + "'";
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
            sql = " Select page_count From document where doc_id=" + doc_id;
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

        public string Get_Paper_type(string doc_id)
        {
            Class.clsDB db = new Class.clsDB();
            string sql;
            sql = " Select paper_type From document where doc_id=" + doc_id;
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


        public DataTable getDocument_By_DocId(string doc_id)
        {
            try
            {
                DataSet ds = new DataSet();
                String sql;
                sql = "Select * From document where doc_id = '" + doc_id + "'";
                Class.clsDB db = new Class.clsDB();
                ds = db.ExecuteDataSet(sql);
                db.Close();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
  

    }
}
