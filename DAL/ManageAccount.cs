using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public  class ManageAccount
    {

       public List<Model.M_Account> getAccount(Model.Criteria.M_AccountCriteria criteria, ref int rsTotalRecord)
       {
           try
           {
     DataSet ds = new DataSet();
            String sql;
            sql = "Select * From account";
            Class.clsDB db = new Class.clsDB();
            ds = db.ExecuteDataSet(sql);

      var result = ds.Tables[0].AsEnumerable().Select(s => new Model.M_Account
            {
                title = s.Field<string>("title"),
                name = s.Field<string>("name"),
                surname = s.Field<string>("surname"),
                username = s.Field<string>("username"),
                password = s.Field<string>("password"),
                email = s.Field<string>("email"),
            }).ToList();

      return result;

           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               
           }
       }


       public DataTable getAccount_By_Email(Model.Criteria.M_AccountCriteria criteria)
       {
           try
           {
               DataSet ds = new DataSet();
               String sql;
               sql = "Select * From account where email='" + criteria.email + "'";
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


       public DataTable getAccount_Level1()
       {
           try
           {
               DataSet ds = new DataSet();
               String sql;
               sql = "Select * From account where level = '1'";
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
  
   
      public int UpdateAccount(Model.Criteria.M_AccountCriteria criteria)
       {
           try
           {
               Class.clsDB db = new Class.clsDB();
               string sql;
               sql = "Update account SET ";
               sql += "title='" + criteria.title + "',";
               sql += "name='" + criteria.name + "',";
               sql += "surname='" + criteria.surname + "',";
               sql += "username='" + criteria.username + "',";
               sql += "password='" + criteria.password + "',";
               sql += "level='" + criteria.level + "',";
               sql += "signature='" + criteria.signature + "' WHERE ";
               sql += " email='" + criteria.email + "'";
               int ret;
               ret = db.ExecuteNonQuery(sql);
               db.Close();
               return ret;
           }
          catch (Exception ex)
           {
               return 0;
           }
       }


       public string Get_Signature_By_Email(string email)
      {
          Class.clsDB db = new Class.clsDB();
          string sql;
          sql = "select signature From account WHERE email='" + email + "'";
          DataTable dt;
          dt = db.ExecuteDataTable(sql);
          db.Close();
           if(dt.Rows.Count > 0 )
           {
               return dt.Rows[0][0].ToString();
           }
           else
           {
               return "Error";
           }


      }

      public int InsertAccount(Model.Criteria.M_AccountCriteria criteria)
      {
          try
          {
              Class.clsDB db = new Class.clsDB();
              string sql;
              sql = "Insert into account ( ";
              sql += "title,";
              sql += "name,";
              sql += "surname,";
              sql += "username,";
              sql += "password,";
              sql += "level,";
              sql += "signature,";
              sql += " email) VALUES(";
              sql += "'" + criteria.title + "',";
              sql += "'" + criteria.name + "',";
              sql += "'" + criteria.surname + "',";
              sql += "'" + criteria.username + "',";
              sql += "'" + criteria.password + "',";
              sql += "'" + criteria.level + "',";
              sql += "'" + criteria.signature + "',";
              sql += "'" + criteria.email + "')";

              int ret;
              ret = db.ExecuteNonQuery(sql);
              db.Close();
              return ret;
          }
          catch(Exception ex)
          {
              return 0;
          } 
      }
   
   }


}
