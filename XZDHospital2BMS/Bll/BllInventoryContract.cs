using Aspose.Cells;
using Dal;
using Helper;
using Model;
using System;
using System.Data;

namespace Bll
{
  public class BllInventoryContract
  {

    public static int add(ModelInventoryContract model)
    {
      return DalInventoryContract.add(model);
    }

    public static void deleteById(int intId)
    {
      DalInventoryContract.deleteById(intId);
    }

    public static void update(ModelInventoryContract model)
    {
      DalInventoryContract.update(model);
    }

    public static ModelInventoryContract getById(int intId)
    {
      return DalInventoryContract.getById(intId);
    }

    public static DataTable getAll()
    {
      return DalInventoryContract.getAll();
    }

    public static DataTable getPage(int intPage, int intPageSize)
    {
      return DalInventoryContract.getPage(intPage, intPageSize);
    }

    public static int getRecordsAmount()
    {
      return DalInventoryContract.getRecordsAmount();
    }

    public static ModelInventoryContract getLatestContract()
    {
      ModelInventoryContract model;
      int intId = DalInventoryContract.getLatestContractId();
      if (intId > 0)
        model = getById(intId);
      else
      {
        model = new ModelInventoryContract();
        model.time_end = Convert.ToDateTime("2019-01-01 00:00:00");
      }
      return model;
    }

    public static string[] setExcel(int intContractId)
    {
      // 根据盘点单ID得到此盘点单下所有的盘点记录，转成DataTable作为Excel文件的数据源
      DataTable objDT = BllInventoryRecord.getAll(intContractId);
      objDT.TableName = "TableInventory";
      DataRow objDR;
      objDT.Columns.Add(new DataColumn("id_row", typeof(int)));
      objDT.Columns.Add(new DataColumn("temp1", typeof(string)));
      objDT.Columns.Add(new DataColumn("temp2", typeof(string)));
      for (int i = 0; i < objDT.Rows.Count; i++)
      {
        objDR = objDT.Rows[i];
        objDR["id_row"] = i + 1;
        objDR["temp1"] = Convert.ToDateTime(objDR["time_sign"]).ToString("yyyy-MM-dd");
        objDR["temp2"] = Convert.ToDateTime(objDR["validity_period"]).ToString("yyyy-MM-dd");
      }
      objDT.Columns.Remove(objDT.Columns["time_sign"]);
      objDT.Columns.Remove(objDT.Columns["validity_period"]);
      objDT.Columns["temp1"].ColumnName = "time_sign";
      objDT.Columns["temp2"].ColumnName = "validity_period";
      // 得到此盘点单的信息，设置要输出的Excel文件的文件名
      ModelInventoryContract model = BllInventoryContract.getById(intContractId);
      string strDateShow = "盘点单 " +
        model.time_start.ToString("yyyy年MM月dd日") +
        " 至 " +
        model.time_end.ToString("yyyy年MM月dd日");
      string strFileName = "盘点单[" +
        model.time_start.ToString("yyMMdd") +
        "-" +
        model.time_end.ToString("yyMMdd") +
        "]-1.xlsx";
      string strExcelTemplateFileName = "/Excel/Template/03盘点单.xlsx";
      string strExcelOutFileName = "/Excel/Export/" + strFileName;
      // 根据以上参数生成excel文件，并输出生成的excel文件路径
      WorkbookDesigner objDesigner = new WorkbookDesigner();
      objDesigner.SetDataSource("DateShow", strDateShow);
      objDesigner.SetDataSource(objDT);
      HelperExcel.ExportExcelByTemplate(objDesigner, strExcelTemplateFileName, strExcelOutFileName);
      string[] aryReturn = new string[1];
      aryReturn[0] = strExcelOutFileName;
      return aryReturn;
    }

  }

}
