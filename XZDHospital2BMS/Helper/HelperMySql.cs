using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace XZDHospital2BMS.Helper
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

    public static int CreateSchema()
    {
      string strDBName = "db_xzdhospital2_bms";
      int intReturn = 0;
      string strConn = "Data Source='localhost';User Id='root';Password='hqiang1025';charset='utf8';pooling=true;Allow Zero Datetime=True;";
      string strSQL = "DROP DATABASE IF EXISTS " + strDBName + ";";
      strSQL += "CREATE DATABASE " + strDBName + ";";
      using (MySqlConnection objConn = new MySqlConnection(strConn))
      {
        MySqlCommand objCmd = new MySqlCommand(strSQL, objConn);
        if (objConn.State != ConnectionState.Open)
          objConn.Open();
        intReturn = objCmd.ExecuteNonQuery();
      }
      return intReturn;
    }

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

    public static int ExcuteNoQuerys(string[] arySQL, params MySqlParameter[][] aryParam)
    {
      int intResult = 0;
      using (objConn = new MySqlConnection(strConn))
      {
        objConn.Open();
        using (objCmd = new MySqlCommand())
        {
          MySqlTransaction objTrans = objConn.BeginTransaction();
          objCmd.Transaction = objTrans;
          objCmd.Connection = objConn;
          try
          {
            for (int i = 0; i < arySQL.Length; i++)
            {
              string strSql = arySQL[i];
              objCmd.CommandText = strSql;
              if (aryParam.Length > i)
              {
                objCmd.Parameters.AddRange(aryParam[i]);
              }
              intResult += objCmd.ExecuteNonQuery();
              objCmd.Parameters.Clear();
            }
            objTrans.Commit();
          }
          catch (Exception ex)
          {
            objTrans.Rollback();
            throw ex;
          }
        }
      }
      return intResult;
    }

    /// <summary>   
    /// 构建 MySqlCommand 对象(用来返回一个结果集，而不是一个整数值)   
    /// </summary>
    private static MySqlCommand BuildQueryCommand(string strSPName, IDataParameter[] aryParams)
    {
      objCmd = new MySqlCommand(strSPName, objConn);
      objCmd.CommandType = CommandType.StoredProcedure;
      foreach (MySqlParameter objParam in aryParams)
      {
        objCmd.Parameters.Add(objParam);
      }
      return objCmd;
    }

    /// <summary>
    /// 执行存储过程
    /// </summary>
    /// <param name="strSPName">存储过程名</param>
    /// <param name="aryParams">存储过程参数</param>
    /// <param name="strTableName">DataSet结果中的表名</param>
    /// <returns>DataSet</returns>
    public static DataSet RunProcedure(string strSPName, IDataParameter[] aryParams, string strTableName)
    {
      using (objConn = new MySqlConnection(strConn))
      {
        DataSet objDS = new DataSet();
        objConn.Open();
        objAdapter = new MySqlDataAdapter();
        objAdapter.SelectCommand = BuildQueryCommand(strSPName, aryParams);
        objAdapter.Fill(objDS, strTableName);
        objConn.Close();
        return objDS;
      }
    }

    public static MySqlDataReader RunProcedure(string strSPName, IDataParameter[] objParams)
    {
      objConn = new MySqlConnection(strConn);
      objConn.Open();
      objCmd = BuildQueryCommand(strSPName, objParams);
      objCmd.CommandType = CommandType.StoredProcedure;
      objReader = objCmd.ExecuteReader();
      return objReader;
    }

    public static void GetPage(out int intTotalPage, out int intRowsCount, int intPageSize, int intCurrentPage, string strTableName)
    {
      MySqlParameter[] aryParam = {
            new MySqlParameter("@TotalPage", MySqlDbType.Int32,4),
            new MySqlParameter("@RowsCount", MySqlDbType.Int32,4),
            new MySqlParameter("@PageSize", MySqlDbType.Int32,4),
            new MySqlParameter("@CurrentPage", MySqlDbType.Int32,4),
            new MySqlParameter("@SelectFields", MySqlDbType.Text,700),
            new MySqlParameter("@IdField",MySqlDbType.Text,50),
            new MySqlParameter("@OrderField", MySqlDbType.Text,200),
            new MySqlParameter("@OrderType", MySqlDbType.Text,2),
            new MySqlParameter("@TableName", MySqlDbType.Text,300),
            new MySqlParameter("@strWhere", MySqlDbType.Text,300),
        };
      aryParam[0].Direction = ParameterDirection.Output;
      aryParam[1].Direction = ParameterDirection.Output;
      aryParam[2].Value = intPageSize;
      aryParam[3].Value = intCurrentPage;
      aryParam[4].Value = "*";
      aryParam[5].Value = "id";
      aryParam[6].Value = "desc";
      aryParam[7].Value = "1";
      aryParam[8].Value = strTableName;
      aryParam[9].Value = "1=1";

      DataSet objDS = RunProcedure("SP_PAGER", aryParam, "TableName");
      DataTable objDT = objDS.Tables[0];
      intTotalPage = int.Parse(aryParam[0].Value.ToString());
      intRowsCount = int.Parse(aryParam[1].Value.ToString());
    }

  }

}