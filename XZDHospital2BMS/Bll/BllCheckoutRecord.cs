using Dal;
using Model;
using System.Data;

namespace Bll
{

  public class BllCheckoutRecord
  {

    public static int add(ModelCheckoutRecord model)
    {
      return DalCheckoutRecord.add(model);
    }

    public static void deleteById(int intId)
    {
      DalCheckoutRecord.deleteById(intId);
    }

    public static int update(ModelCheckoutRecord model)
    {
      return DalCheckoutRecord.update(model);
    }

    public static int updateAmountById(decimal dcmAmount, int intId)
    {
      return DalCheckoutRecord.updateAmountById(dcmAmount, intId);
    }

    public static ModelCheckoutRecord getById(int intId)
    {
      return DalCheckoutRecord.getById(intId);
    }

    public static DataTable getAll(int intContractId)
    {
      return DalCheckoutRecord.getAll(intContractId);
    }

    public static DataTable getPage(int intContractId, int intPage, int intPageSize)
    {
      return DalCheckoutRecord.getPage(intContractId, intPage, intPageSize);
    }

    public static int getRecordsAmount(int intContractId)
    {
      return DalCheckoutRecord.getRecordsAmount(intContractId);
    }

    public static decimal getPriceTotal(int intContractId)
    {
      return DalCheckoutRecord.getPriceTotal(intContractId);
    }

    public static decimal getAmountByGoodsId(int intGoodsId)
    {
      return DalCheckoutRecord.getAmountByGoodsId(intGoodsId);
    }

  }

}
