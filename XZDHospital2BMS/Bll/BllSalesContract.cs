using Aspose.Cells;
using Dal;
using Helper;
using Model;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace Bll
{
  public class BllSalesContract
  {

    public static int add(ModelSalesContract model)
    {
      return DalSalesContract.add(model);
    }

    public static void deleteById(int intId)
    {
      // 没有从数据库中删除记录，只是改变了删除标记的值
      changeIsDeleted(intId);
      // DalSalesContract.deleteById(intId);
    }

    public static int update(ModelSalesContract model)
    {
      return DalSalesContract.update(model);
    }

    public static ModelSalesContract getById(int intId)
    {
      return DalSalesContract.getById(intId);
    }

    public static DataTable getAll()
    {
      return DalSalesContract.getAll();
    }

    public static DataTable getForDDL()
    {
      return DalSalesContract.getForDDL();
    }

    public static DataTable getPage(int intPage, int intPageSize)
    {
      return DalSalesContract.getPage(intPage, intPageSize);
    }

    public static int getRecordsAmount()
    {
      return DalSalesContract.getRecordsAmount();
    }

    public static void changeIsDeleted(int intID)
    {
      DalSalesContract.changeIsDeleted(intID);
    }

    // 导出入库单货品清单以便打印的Excel文件
    public static string[] setExcel(int intContractId)
    {
      // 得到此入库单的信息
      ModelSalesContract model = getById(intContractId);
      string strCompanyName = BllSalesCompany.getById(model.id_company).name;
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
        strExcelOutFileName = "/Excel/Export/入库单[" + strCompanyName +
          model.time_sign.ToString("yyMMdd") + "]-" + (i + 1) + ".xlsx";
        // 根据以上参数生成excel文件，并输出生成的excel文件路径
        HelperExcel.ExportExcelByTemplate(objDesigner, strExcelTemplateFileName, strExcelOutFileName);
        aryReturn[i] = strExcelOutFileName;
        // 处理完成后把表名改成别的名字，DS中不能有重名的Table
        objDT.TableName = "DataTable" + i;
      }
      return aryReturn;
    }

    // 将入库单绑定到ddl上
    public static void bindDDL(DropDownList objDDL)
    {
      objDDL.DataSource = getForDDL();
      objDDL.DataTextField = "text_show";
      objDDL.DataValueField = "id";
      objDDL.DataBind();
    }

  }

}
