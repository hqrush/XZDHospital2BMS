using Dal;
using Model;
using System;
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

    public static void update(ModelInventoryContract model)
    {
      DalInventoryContract.update(model);
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

    public static ModelInventoryContract getLatestContract()
    {
      ModelInventoryContract model;
      int intId = DalInventoryContract.getLatestContractId();
      if (intId > 0)
        model = getById(intId);
      else
      {
        model = new ModelInventoryContract();
        model.time_end = Convert.ToDateTime("2019-01-01 00:00:00");
      }
      return model;
    }

  }

}
