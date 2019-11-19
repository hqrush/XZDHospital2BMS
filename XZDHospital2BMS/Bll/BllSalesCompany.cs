using Dal;
using Model;
using System.Data;

namespace Bll
{
  public class BllSalesCompany
  {

    public static int add(ModelSalesCompany model)
    {
      return DalSalesCompany.add(model);
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

    public static DataTable getDataTableAll()
    {
      return DalSalesCompany.getDataTableAll();
    }

  }

}
