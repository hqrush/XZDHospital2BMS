using Dal;
using Model;
using System.Data;
using System.Web.UI.WebControls;

namespace Bll
{
  public class BllSalesCompany
  {

    public static int add(ModelSalesCompany model)
    {
      // 先判断有没有重名
      int intId = getIdByName(model.name);
      if (intId > 0) return intId;
      else return DalSalesCompany.add(model);
    }

    public static void deleteById(int intId)
    {
      DalSalesCompany.deleteById(intId);
    }

    public static int update(ModelSalesCompany model)
    {
      return DalSalesCompany.update(model);
    }

    public static ModelSalesCompany getById(int intId)
    {
      return DalSalesCompany.getById(intId);
    }

    public static DataTable getAll()
    {
      return DalSalesCompany.getAll();
    }

    public static DataTable getPage(int intPage, int intPageSize)
    {
      return DalSalesCompany.getPage(intPage, intPageSize);
    }

    public static int getRecordsAmount()
    {
      return DalSalesCompany.getRecordsAmount();
    }

    // 此方法只有一个参数，根据名称查找id，用在add里，如果已经添加了这个公司则不加
    public static int getIdByName(string strCompanyName)
    {
      return DalSalesCompany.getIdByName(strCompanyName);
    }

    // 此方法有二个参数，根据名称查找id，用在添加入库单页面上
    // 如果已经添加了这个公司则直接返回id，如果没加则新建销售公司
    public static int getIdByName(string strCompanyName, int intAdminId)
    {
      int intId = getIdByName(strCompanyName);
      if (intId > 0) return intId;
      ModelSalesCompany model = new ModelSalesCompany();
      model.name = strCompanyName;
      model.id_admin = intAdminId;
      intId = DalSalesCompany.add(model);
      if (intId > 0) return intId; else return 0;
    }

    public static void bindDDLData(DropDownList ddl)
    {
      DataTable objDT = getAll();
      DataRow objDR = objDT.NewRow();
      objDR["id"] = 0;
      objDR["name"] = "预入库";
      objDT.Rows.InsertAt(objDR, 0);
      // 绑定下拉菜单
      ddl.DataSource = objDT;
      ddl.DataTextField = "name";
      ddl.DataValueField = "id";
      ddl.DataBind();
    }

    public static void bindRPT(Repeater rpt)
    {
      // 得到所有公司
      DataTable objDT = getAll();
      // 添加“预入库”标识
      DataRow objDR = objDT.NewRow();
      objDR["id"] = 0;
      objDR["name"] = "预入库";
      objDT.Rows.InsertAt(objDR, 0);
      // 把数据绑定到Repeater上
      rpt.DataSource = objDT;
      rpt.DataBind();
    }

  }

}
