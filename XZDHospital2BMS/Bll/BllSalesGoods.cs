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

    public static void updateContractId(int intId, int intContractId)
    {
      DalSalesGoods.updateContractId(intId, intContractId);
    }

    public static int updateAmountStock(decimal dcmAmountOut, int intId)
    {
      return DalSalesGoods.updateAmountStock(dcmAmountOut, intId);
    }

    public static int updateAmountStockByInventory(decimal dcmAmountInventoryShow, int intId)
    {
      return DalSalesGoods.updateAmountStockByInventory(dcmAmountInventoryShow, intId);
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

    public static DataTable getDTByName(string strProductName, string strFactoryName)
    {
      return DalSalesGoods.getDTByName(strProductName, strFactoryName);
    }

    public static DataTable getInventoryDTByName(string strProductName, string strFactoryName)
    {
      return DalSalesGoods.getInventoryDTByName(strProductName, strFactoryName);
    }

  }

}
