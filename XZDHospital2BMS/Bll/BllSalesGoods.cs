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

    public static DataTable getAll(int intContractId)
    {
      return DalSalesGoods.getAll(intContractId);
    }

    public static DataTable getPage(int intContractId, int intPage, int intPageSize)
    {
      return DalSalesGoods.getPage(intContractId, intPage, intPageSize);
    }

    public static int getRecordsAmount(int intContractId)
    {
      return DalSalesGoods.getRecordsAmount(intContractId);
    }

    public static decimal getPriceTotal(int intContractId)
    {
      return DalSalesGoods.getPriceTotal(intContractId);
    }

  }

}
