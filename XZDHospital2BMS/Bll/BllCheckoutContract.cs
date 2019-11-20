using Dal;
using Model;
using System.Data;

namespace Bll
{
  public class BllCheckoutContract
  {

    public static int add(ModelCheckoutContract model)
    {
      return DalCheckoutContract.add(model);
    }

    public static void deleteById(int intId)
    {
      DalCheckoutContract.deleteById(intId);
    }

    public static int update(ModelCheckoutContract model)
    {
      return DalCheckoutContract.update(model);
    }

    public static ModelCheckoutContract getById(int intId)
    {
      return DalCheckoutContract.getById(intId);
    }

    public static DataTable getAll()
    {
      return DalCheckoutContract.getAll();
    }

    public static DataTable getPage(int intPage, int intPageSize)
    {
      return DalCheckoutContract.getPage(intPage, intPageSize);
    }

    public static int getRecordsAmount()
    {
      return DalCheckoutContract.getRecordsAmount();
    }

  }

}
