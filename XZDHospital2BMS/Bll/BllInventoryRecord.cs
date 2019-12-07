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

    public static int update(ModelInventoryRecord model)
    {
      return DalInventoryRecord.update(model);
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

    public static decimal getPriceTotal(int intContractId)
    {
      return DalInventoryRecord.getPriceTotal(intContractId);
    }

    public static decimal getAmountByGoodsId(int intGoodsId)
    {
      return DalInventoryRecord.getAmountByGoodsId(intGoodsId);
    }

  }

}
