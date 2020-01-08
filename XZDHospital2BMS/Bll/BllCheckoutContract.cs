using Aspose.Cells;
using Dal;
using Helper;
using Model;
using System.Data;

namespace Bll
{
  public class BllCheckoutContract
  {

    public static int add(ModelCheckoutContract model)
    {
      return DalCheckoutContract.add(model);
    }

    public static void deleteById(int intId)
    {
      DalCheckoutContract.deleteById(intId);
    }

    public static int update(ModelCheckoutContract model)
    {
      return DalCheckoutContract.update(model);
    }

    public static ModelCheckoutContract getById(int intId)
    {
      return DalCheckoutContract.getById(intId);
    }

    public static DataTable getAll()
    {
      return DalCheckoutContract.getAll();
    }

    public static DataTable getPage(int intPage, int intPageSize)
    {
      return DalCheckoutContract.getPage(intPage, intPageSize);
    }

    public static int getRecordsAmount()
    {
      return DalCheckoutContract.getRecordsAmount();
    }

    // 导出出库单货品清单以便打印的Excel文件
    public static string[] setExcel(int intContractId)
    {
      // 得到此入库单的信息
      ModelCheckoutContract model = getById(intContractId);
      string strCompanyName = BllSalesCompany.getById(model.name_department).name;
      string strYear = model.time_sign.Year.ToString();
      string strMonth = model.time_sign.Month.ToString();
      string strDay = model.time_sign.Day.ToString();
      string strDateShow = " " + strYear + "   " + strMonth + "   " + strDay;
      // 根据入库单ID得到此入库单下所有的入库货品记录，转成DataTable作为Excel文件的数据源
      DataTable objDTAll = BllSalesGoods.getAll(intContractId);
      DataSet objDS = HelperUtility.splitDataTable(objDTAll, 5);
      string[] aryReturn = new string[objDS.Tables.Count];
      DataTable objDT;
      string strExcelTemplateFileName = "/Excel/Template/01入库单.xlsx";
      string strExcelOutFileName;
      for (int i = 0; i < objDS.Tables.Count; i++)
      {
        // 先对DataTable里的数据进行改造
        objDT = objDS.Tables[i];
        objDT.TableName = "DataTable";
        WorkbookDesigner objDesigner = new WorkbookDesigner();
        objDesigner.SetDataSource("SalesCompany", "           " + strCompanyName);
        objDesigner.SetDataSource("DateShow", strDateShow);
        objDesigner.SetDataSource(objDT);
        // 设置要输出的每个Excel文件的文件名
        strExcelOutFileName = "/Excel/Export/出库单[" + strCompanyName +
          model.time_sign.ToString("yyMMdd") + "]-" + (i + 1) + ".xlsx";
        // 根据以上参数生成excel文件，并输出生成的excel文件路径
        HelperExcel.ExportExcelByTemplate(objDesigner, strExcelTemplateFileName, strExcelOutFileName);
        aryReturn[i] = strExcelOutFileName;
        // 处理完成后把表名改成别的名字，DS中不能有重名的Table
        objDT.TableName = "DataTable" + i;
      }
      return aryReturn;
    }

  }

}
