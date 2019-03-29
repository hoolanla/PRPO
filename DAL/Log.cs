using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class Log
    {

            public void WriteLog(Model.Log log)
       {
                Class.clsDB db = new Class.clsDB();
                string sql = "Insert into log(create_by,content) Values(";
                sql += "'" + log.create_by + "','" + log.content + "')";
                db.ExecuteNonQuery(sql);
                db.Close();
       }

    }
}
