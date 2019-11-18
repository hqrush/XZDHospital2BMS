using System;
using System.Data;
using System.Data.OleDb;
using Aspose.Cells;

namespace XZDHospital2BMS.Helper
{

  public class HelperExcel
  {

    public HelperExcel() { }

    private static string SetConnectionString(string strExcelFilePath)
    {
      //此连接只能操作Excel2007之前(.xls)文件
      //string strConn = "Provider=Microsoft.Jet.OleDb.4.0;" + "Data Source=" +
      //	strExcelFilePath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
      //此连接可以操作.xls与.xlsx文件
      string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
          strExcelFilePath + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";
      return strConn;
    }

    /// <summary>
    /// 根据表名生成Datatable
    /// </summary>
    /// <param name="strFileName">Excel文件路径</param>
    /// <param name="strSheetName">Excel文件里的表名</param>
    /// <returns>返回DataTable</returns>
    public static DataTable GetExcelToDataTableBySheet(string strExcelFilePath, string strSheetName)
    {
      string strConn = SetConnectionString(strExcelFilePath);
      using (OleDbConnection objConn = new OleDbConnection(strConn))
      {
        objConn.Open();
        using (OleDbCommand objCmd = objConn.CreateCommand())
        {
          objCmd.CommandText = string.Format("SELECT * FROM [{0}]", strSheetName);
          OleDbDataAdapter objDA = new OleDbDataAdapter(objCmd);
          DataSet objDS = new DataSet();
          objDA.Fill(objDS, strSheetName);
          return objDS.Tables[0];
        }
      }
    }

    /// <summary>
    /// 获取Excel文件中所有表名
    /// </summary>
    /// <param name="strExcelFilePath">Excel文件路径</param>
    /// <returns>返回字符串数组</returns>
    public static String[] GetExcelAllSheetNames(string strExcelFilePath)
    {
      string strConn = SetConnectionString(strExcelFilePath);
      using (OleDbConnection objConn = new OleDbConnection(strConn))
      {
        objConn.Open();
        using (OleDbCommand objCmd = objConn.CreateCommand())
        {
          DataTable objDT = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
          if (objDT == null)
          {
            return null;
          }
          String[] aryExcelSheets = new String[objDT.Rows.Count];
          int i = 0;
          foreach (DataRow row in objDT.Rows)
          {
            aryExcelSheets[i] = row["TABLE_NAME"].ToString() + "$";
            i++;
          }
          return aryExcelSheets;
        }
      }
    }

    /// <summary>
    /// 根据Excel文件路径并将此内容输出到一个DataTable中
    /// </summary>
    /// <param name="strExcelFilePath">Excel文件完整路径</param>
    /// <param name="intSheetNO">Excel表序号</param>
    /// <returns>包含Excel文件内容的DataTable</returns>
    public static DataTable ReadExcel(string strExcelFilePath, int intSheetNO)
    {
      Workbook objWB = new Workbook(strExcelFilePath);
      Worksheet objSheet = objWB.Worksheets[intSheetNO];
      Cells objCells = objSheet.Cells;
      if (objCells.MaxDataRow > 0)
        return objCells.ExportDataTableAsString(0, 0, objCells.MaxDataRow + 1, objCells.MaxDataColumn + 1, false);
      else return null;
    }

  }

}
