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

    public static ModelCheckoutRecord getById(int intId)
    {
      return DalCheckoutRecord.getById(intId);
    }

    public static DataTable getAll()
    {
      return DalCheckoutRecord.getAll();
    }

    public static DataTable getPage(int intPage, int intPageSize)
    {
      return DalCheckoutRecord.getPage(intPage, intPageSize);
    }

    public static int getRecordsAmount()
    {
      return DalCheckoutRecord.getRecordsAmount();
    }

  }

}
