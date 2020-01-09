using Aspose.Cells;
using Dal;
using Helper;
using Model;
using System.Collections.Generic;
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

    /// <summary>
    /// 导出记录出库单货品清单的Excel文件
    /// </summary>
    /// <param name="intContractId">出库单id号</param>
    /// <returns>5条记录为一个excel文件，每个文件的地址存到数组里输出</returns>
    public static string[] setExcel(int intContractId)
    {
      // 得到此出库单的信息
      ModelCheckoutContract model = getById(intContractId);
      // 得到收货单位名称
      string strNameUnit = model.name_unit;
      string[] aryUnit = strNameUnit.Split(',');
      // 得到收货部门科室名称
      string strNameDepartment = model.name_department;
      // 设置出库单时间
      string strYear = model.time_create.Year.ToString();
      string strMonth = model.time_create.Month.ToString();
      string strDay = model.time_create.Day.ToString();
      string strDateShow = "  " + strYear + "   " + strMonth + "   " + strDay;
      // 根据出库单ID得到此出库单下所有的出库货品记录，转成DataTable作为Excel文件的数据源
      DataTable objDTAll = BllCheckoutRecord.getAll(intContractId);
      DataSet objDS = HelperUtility.splitDataTable(objDTAll, 5);
      List<string> listReturn = new List<string>();
      DataTable objDT;
      string strExcelTemplateFileName, strExcelOutFileName;
      // 不同的收货单位要设置不同的模板，二院的肯定要生成
      if ("信州区第二人民医院".Equals(aryUnit[0]))
      {
        for (int i = 0; i < objDS.Tables.Count; i++)
        {
          // 先对DataTable里的数据进行改造
          objDT = objDS.Tables[i];
          objDT.TableName = "DataTable";
          WorkbookDesigner objDesigner = new WorkbookDesigner();
          objDesigner.SetDataSource("NameUnit", aryUnit[0]);
          objDesigner.SetDataSource("NameDepartment", strNameDepartment);
          objDesigner.SetDataSource("DateShow", strDateShow);
          objDesigner.SetDataSource(objDT);
          strExcelTemplateFileName = "/Excel/Template/02出库单.xlsx";
          // 设置要输出的每个Excel文件的文件名
          strExcelOutFileName = "/Excel/Export/出库单[区二院-" + strNameDepartment + "-" +
            model.time_create.ToString("yyMMdd") + "]-" + (i + 1) + ".xlsx";
          // 根据以上参数生成excel文件，并输出生成的excel文件路径
          HelperExcel.ExportExcelByTemplate(objDesigner, strExcelTemplateFileName, strExcelOutFileName);
          listReturn.Add(strExcelOutFileName);
          // 处理完成后把表名改成别的名字，DS中不能有重名的Table
          objDT.TableName = "DataTable" + i;
        }
      }
      // 如果社区的也要打就在此生成
      if ("东市街道社区卫生服务中心".Equals(aryUnit[1]))
      {
        for (int i = 0; i < objDS.Tables.Count; i++)
        {
          // 先对DataTable里的数据进行改造
          objDT = objDS.Tables[i];
          objDT.TableName = "DataTable";
          WorkbookDesigner objDesigner = new WorkbookDesigner();
          objDesigner.SetDataSource("NameUnit", aryUnit[1]);
          objDesigner.SetDataSource("NameDepartment", strNameDepartment);
          objDesigner.SetDataSource("DateShow", strDateShow);
          objDesigner.SetDataSource(objDT);
          strExcelTemplateFileName = "/Excel/Template/02出库单.xlsx";
          // 设置要输出的每个Excel文件的文件名
          strExcelOutFileName = "/Excel/Export/出库单[东市-" + strNameDepartment + "-" +
            model.time_create.ToString("yyMMdd") + "]-" + (i + 1) + ".xlsx";
          // 根据以上参数生成excel文件，并输出生成的excel文件路径
          HelperExcel.ExportExcelByTemplate(objDesigner, strExcelTemplateFileName, strExcelOutFileName);
          listReturn.Add(strExcelOutFileName);
          // 处理完成后把表名改成别的名字，DS中不能有重名的Table
          objDT.TableName = "DataTable" + i;
        }
      }
      return listReturn.ToArray();
    }

  }

}
