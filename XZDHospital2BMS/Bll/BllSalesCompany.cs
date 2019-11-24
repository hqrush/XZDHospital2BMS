using Dal;
using Model;
using System.Data;

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

    public static int getIdByName(string strCompanyName)
    {
      return DalSalesCompany.getIdByName(strCompanyName);
    }

  }

}
