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

    // 当删除一条出货记录时，要将这条货品的出货数量加回到库存里
    public static int addAmountStock(int intGoodsId, decimal dcmAmountOut)
    {
      return DalSalesGoods.addAmountStock(intGoodsId, dcmAmountOut);
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

    public static int updateAmountStockByCheckOut(decimal dcmAmountOut, int intId)
    {
      return DalSalesGoods.updateAmountStockByCheckOut(dcmAmountOut, intId);
    }

    public static int clearAmountStock(int intId)
    {
      return DalSalesGoods.clearAmountStock(intId);
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

  }

}
