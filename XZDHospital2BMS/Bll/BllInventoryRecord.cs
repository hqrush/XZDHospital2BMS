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

    public static DataTable getAll()
    {
      return DalInventoryRecord.getAll();
    }

    public static DataTable getPage(int intPage, int intPageSize)
    {
      return DalInventoryRecord.getPage(intPage, intPageSize);
    }

    public static int getRecordsAmount()
    {
      return DalInventoryRecord.getRecordsAmount();
    }

  }

}
