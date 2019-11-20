using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Helper
{

  public class HelperMySql
  {

    public HelperMySql()
    {
    }

    public static string strConn = "Database='db_xzdhospital2_bms';Data Source=localhost;User ID=root;Password=hqiang1025;CharSet=utf8;";
    public static MySqlConnection objConn;
    public static MySqlCommand objCmd;
    public static MySqlDataAdapter objAdapter;
    public static MySqlDataReader objReader;
    public static DataTable objDT;

    public static DataTable GetDataTable(string strSQL, params MySqlParameter[] aryParam)
    {
      using (objConn = new MySqlConnection(strConn))
      {
        objConn.Open();
        using (objCmd = objConn.CreateCommand())
        {
          objCmd.CommandText = strSQL;
          objCmd.Parameters.AddRange(aryParam);
          MySqlDataAdapter adapter = new MySqlDataAdapter(objCmd);
          DataTable objDT = new DataTable();
          adapter.Fill(objDT);
          return objDT;
        }
      }
    }

    public static int ExcuteNoQuery(string strSQL, params MySqlParameter[] aryParam)
    {
      using (objConn = new MySqlConnection(strConn))
      {
        objConn.Open();
        using (objCmd = new MySqlCommand(strSQL, objConn))
        {
          objCmd.Parameters.AddRange(aryParam);
          return objCmd.ExecuteNonQuery();
        }
      }
    }

    public static int ExecuteScalar(string strSQL, params MySqlParameter[] aryParam)
    {
      using (objConn = new MySqlConnection(strConn))
      {
        objConn.Open();
        using (objCmd = new MySqlCommand(strSQL, objConn))
        {
          objCmd.Parameters.AddRange(aryParam);
          return Convert.ToInt32(objCmd.ExecuteScalar());
        }
      }
    }

  }

}