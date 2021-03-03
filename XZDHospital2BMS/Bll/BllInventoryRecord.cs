using Dal;
using Model;
using System.Data;

namespace Bll
{
  public class BllInventoryRecord
  {

    public static int add(ModelInventoryRecord model)
    {
      return DalInventoryRecord.add(model);
    }

    public static void deleteById(int intId)
    {
      DalInventoryRecord.deleteById(intId);
    }

    public static void update(ModelInventoryRecord model)
    {
      DalInventoryRecord.update(model);
    }

    public static void updateRealById(decimal dcmAmountReal, int intId)
    {
      DalInventoryRecord.updateRealById(dcmAmountReal, intId);
    }

    public static void updateStockById(decimal dcmAmountStock, int intId)
    {
      DalInventoryRecord.updateStockById(dcmAmountStock, intId);
    }

    public static void updateFillById(decimal dcmAmountFill, int intId)
    {
      DalInventoryRecord.updateFillById(dcmAmountFill, intId);
    }

    public static ModelInventoryRecord getById(int intId)
    {
      return DalInventoryRecord.getById(intId);
    }

    public static DataTable getAll(int intContractId)
    {
      return DalInventoryRecord.getAll(intContractId);
    }

    public static DataTable getPage(int intContractId, int intPage, int intPageSize)
    {
      return DalInventoryRecord.getPage(intContractId, intPage, intPageSize);
    }

    public static int getRecordsAmount(int intContractId)
    {
      return DalInventoryRecord.getRecordsAmount(intContractId);
    }

    public static DataTable getByQuery(int intContractId, string strProductName)
    {
      return DalInventoryRecord.getByQuery(intContractId, strProductName);
    }

    public static decimal[] getPriceTotalInventory(int intContractId)
    {
      return DalInventoryRecord.getPriceTotalInventory(intContractId);
    }

    public static void setInventoryRecord(int intContractId)
    {
      DalInventoryRecord.setInventoryRecord(intContractId);
    }

    public static bool isRecordAdded(int intContractId, int intGoodsId)
    {
      return DalInventoryRecord.isRecordAdded(intContractId, intGoodsId);
    }

    public static void clearZero(int intContractId)
    {
      DalInventoryRecord.clearZero(intContractId);
    }

  }

}
