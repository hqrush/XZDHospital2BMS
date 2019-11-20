using Dal;
using Model;
using System.Data;

namespace Bll
{
  public class BllSalesGoods
  {

    public static int add(ModelSalesGoods model)
    {
      return DalSalesGoods.add(model);
    }

    public static void deleteById(int intId)
    {
      DalSalesGoods.deleteById(intId);
    }

    public static int update(ModelSalesGoods model)
    {
      return DalSalesGoods.update(model);
    }

    public static ModelSalesGoods getById(int intId)
    {
      return DalSalesGoods.getById(intId);
    }

    public static DataTable getAll()
    {
      return DalSalesGoods.getAll();
    }

    public static DataTable getPage(int intPage, int intPageSize)
    {
      return DalSalesGoods.getPage(intPage, intPageSize);
    }

    public static int getRecordsAmount()
    {
      return DalSalesGoods.getRecordsAmount();
    }

  }

}
