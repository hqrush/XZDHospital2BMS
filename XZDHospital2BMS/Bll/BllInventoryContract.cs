using Dal;
using Model;
using System.Data;

namespace Bll
{
  public class BllInventoryContract
  {

    public static int add(ModelInventoryContract model)
    {
      return DalInventoryContract.add(model);
    }

    public static void deleteById(int intId)
    {
      DalInventoryContract.deleteById(intId);
    }

    public static int update(ModelInventoryContract model)
    {
      return DalInventoryContract.update(model);
    }

    public static ModelInventoryContract getById(int intId)
    {
      return DalInventoryContract.getById(intId);
    }

    public static DataTable getAll()
    {
      return DalInventoryContract.getAll();
    }

    public static DataTable getPage(int intPage, int intPageSize)
    {
      return DalInventoryContract.getPage(intPage, intPageSize);
    }

    public static int getRecordsAmount()
    {
      return DalInventoryContract.getRecordsAmount();
    }

  }

}
