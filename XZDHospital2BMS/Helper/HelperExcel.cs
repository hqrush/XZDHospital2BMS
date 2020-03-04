using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web;
using Aspose.Cells;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace Helper
{

  public class HelperExcel
  {

    private static string strRootPath = HttpContext.Current.Request.PhysicalApplicationPath;

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

    /// <summary>
    /// 根据Excel模板生成excel文件
    /// </summary>
    /// <param name="objDT">DataTable格式数据源</param>
    /// <param name="strExcelTemplateFileName">Excel模板文件，包括相对网站根目录的路径</param>
    /// <param name="strExcelOutFileName">导出的Excel文件，包括相对网站根目录的路径</param>
    public static void ExportExcelByTemplate(WorkbookDesigner objDesigner,
      string strExcelTemplateFileName,
      string strExcelOutFileName)
    {
      string strExcelTemplateFullFileName = strRootPath + strExcelTemplateFileName;
      string strExcelOutFullFileName = strRootPath + strExcelOutFileName;
      // 如果不存在此目录则创建目录
      string strExcelOutFullPath = Path.GetDirectoryName(strExcelOutFullFileName);
      if (Directory.Exists(strExcelOutFullPath))
        Directory.CreateDirectory(strExcelOutFullPath);
      objDesigner.Workbook = new Workbook(strExcelTemplateFullFileName);
      objDesigner.Process();
      objDesigner.Workbook.Save(strExcelOutFullFileName);
    }

    public static string SetExcelZip(string[] aryExcel)
    {
      if (aryExcel.Length == 0) return "";
      // strRootPath = "F:\\WorkSpaceVS\\XZDHospital2BMS\\XZDHospital2BMS\\XZDHospital2BMS\\"
      // aryExcel[0] = "/Excel/Export/入库单[浙江美达制药191226]-1.xlsx"
      string strZipFileName = aryExcel[0].Substring(aryExcel[0].LastIndexOf('/') + 1);
      strZipFileName = strZipFileName.Substring(0, strZipFileName.LastIndexOf('-')) + ".zip";
      // 多文件压缩
      ZipFile objZip;
      string strFullZipPath = strRootPath + @"Excel\Export\";
      using (objZip = ZipFile.Create(strFullZipPath + strZipFileName))
      {
        objZip.BeginUpdate();
        objZip.NameTransform = new MyNameTransfom();
        for (int i = 0; i < aryExcel.Length; i++)
        {
          objZip.Add(strRootPath + aryExcel[i]);
        }
        objZip.CommitUpdate();
      }
      // 删除xlsx文件
      for (int i = 0; i < aryExcel.Length; i++)
      {
        if (File.Exists(strRootPath + aryExcel[i]))
          File.Delete(strRootPath + aryExcel[i]);
      }
      return "/Excel/Export/" + strZipFileName;
    }

  }

  public class MyNameTransfom : INameTransform
  {

    public string TransformDirectory(string name)
    {
      return null;
    }

    public string TransformFile(string name)
    {
      return Path.GetFileName(name);
    }

  }

}
