using Dal;
using Model;
using System.Data;

namespace Bll
{
  public class BllSalesContract
  {

    public static int add(ModelSalesContract model)
    {
      return DalSalesContract.add(model);
    }

    public static void deleteById(int intId)
    {
      // 没有从数据库中删除记录，只是改变了删除标记的值
      changeIsDeleted(intId);
      // DalSalesContract.deleteById(intId);
    }

    public static int update(ModelSalesContract model)
    {
      return DalSalesContract.update(model);
    }

    public static ModelSalesContract getById(int intId)
    {
      return DalSalesContract.getById(intId);
    }

    public static DataTable getAll()
    {
      return DalSalesContract.getAll();
    }

    public static DataTable getPage(int intPage, int intPageSize)
    {
      return DalSalesContract.getPage(intPage, intPageSize);
    }

    public static int getRecordsAmount()
    {
      return DalSalesContract.getRecordsAmount();
    }

    public static void changeIsDeleted(int intID)
    {
      DalSalesContract.changeIsDeleted(intID);
    }

  }

}
