using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace PR_PO.PROJECT.Class
{
    public  class ClsModule
    {

        public string getRuningNoDoc()
        {
            //Format yyyyMMdd-01   2018011501


            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");


            clsDB db = new clsDB();
            string sql = null;
            string curDate;
           curDate = DateTime.Now.ToString("yyyyMMdd",cultureinfo);
            //DateTime dateEng = Convert.ToDateTime(curDateTmp, cultureinfo);
            //curDate = dateEng.ToString("yyyyMMdd");


            sql = "Select doc_id From Document where doc_id like '" + curDate + "%'";
            object MyScalar = null;
            DataTable dt;
            
            dt = db.ExecuteDataTable(sql);
            db.Close();
            db.Dispose();
            if(dt != null)
            {
                if(dt.Rows.Count > 0)
                {
                    sql = "Select Max(doc_id) + 1 From Document";
                    MyScalar = db.ExecuteScalar(sql);
                    return MyScalar.ToString();
                }
                else
                {
          
                    return   curDate.ToString() + "01";
                }


            }



         





            if (MyScalar != null)
            {

            }
            else
            {


            }


            return curDate;
        }



    }
}