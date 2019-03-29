using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MySql.Data.MySqlClient;


namespace PR_PO.PROJECT.Class
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using MySql.Data.MySqlClient;

    public partial class clsDB : System.Web.UI.Page
    {
        private MySqlConnection objConn;
        private MySqlCommand objCmd;
        private MySqlTransaction Trans;
        private String strConnString;

        public clsDB()
        {
            strConnString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
        }

        public MySqlDataReader ExecuteDataReader(String strSQL)
        {
            MySqlDataReader dtReader;
            objConn = new MySqlConnection();
            objConn.ConnectionString = strConnString;
            objConn.Open();

            objCmd = new MySqlCommand(strSQL, objConn);
            dtReader = objCmd.ExecuteReader();
            return dtReader; //*** Return DataReader ***//
        }

        public DataSet ExecuteDataSet(String strSQL)
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter dtAdapter = new MySqlDataAdapter();
            objConn = new MySqlConnection();
            objConn.ConnectionString = strConnString;
            objConn.Open();

            objCmd = new MySqlCommand();
            objCmd.Connection = objConn;
            objCmd.CommandText = strSQL;
            objCmd.CommandType = CommandType.Text;

            dtAdapter.SelectCommand = objCmd;
            dtAdapter.Fill(ds);
            return ds;   //*** Return DataSet ***//
        }

        public DataTable ExecuteDataTable(String strSQL)
        {
            MySqlDataAdapter dtAdapter;
            DataTable dt = new DataTable();
            objConn = new MySqlConnection();
            objConn.ConnectionString = strConnString;
            objConn.Open();

            dtAdapter = new MySqlDataAdapter(strSQL, objConn);
            dtAdapter.Fill(dt);
            return dt; //*** Return DataTable ***//
        }

        public Boolean ExecuteNonQuery(String strSQL)
        {
            objConn = new MySqlConnection();
            objConn.ConnectionString = strConnString;
            objConn.Open();

            try
            {
                objCmd = new MySqlCommand();
                objCmd.Connection = objConn;
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = strSQL;

                objCmd.ExecuteNonQuery();
                return true; //*** Return True ***//
            }
            catch (Exception)
            {
                return false; //*** Return False ***//
            }
        }


        public Object ExecuteScalar(String strSQL)
        {
            Object obj;
            objConn = new MySqlConnection();
            objConn.ConnectionString = strConnString;
            objConn.Open();

            try
            {
                objCmd = new MySqlCommand();
                objCmd.Connection = objConn;
                objCmd.CommandType = CommandType.Text;
                objCmd.CommandText = strSQL;

                obj = objCmd.ExecuteScalar();  //*** Return Scalar ***//
                return obj;
            }
            catch (Exception)
            {
                return null; //*** Return Nothing ***//
            }
        }

        public void TransStart()
        {
            objConn = new MySqlConnection();
            objConn.ConnectionString = strConnString;
            objConn.Open();
            Trans = objConn.BeginTransaction(IsolationLevel.ReadCommitted);
        }


        public void TransExecute(String strSQL)
        {
            objCmd = new MySqlCommand();
            objCmd.Connection = objConn;
            objCmd.Transaction = Trans;
            objCmd.CommandType = CommandType.Text;
            objCmd.CommandText = strSQL;
            objCmd.ExecuteNonQuery();
        }


        public void TransRollBack()
        {
            Trans.Rollback();
        }

        public void TransCommit()
        {
            Trans.Commit();
        }

        public void Close()
        {
            objConn.Close();
            objConn = null;
        }
    }

}