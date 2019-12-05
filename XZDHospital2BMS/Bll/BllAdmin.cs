using Dal;
using Model;
using System.Data;

namespace Bll
{
  public class BllAdmin
  {

    public static int add(ModelAdmin model)
    {
      return DalAdmin.add(model);
    }

    public static void deleteById(int intId)
    {
      DalAdmin.deleteById(intId);
    }

    public static void update(ModelAdmin model)
    {
      DalAdmin.update(model);
    }

    public static ModelAdmin getById(int intId)
    {
      return DalAdmin.getById(intId);
    }

    public static DataTable getAll()
    {
      return DalAdmin.getAll();
    }

    public static DataTable getPage(int intPage, int intPageSize)
    {
      return DalAdmin.getPage(intPage, intPageSize);
    }

    public static int getRecordsAmount()
    {
      return DalAdmin.getRecordsAmount();
    }

    public static void login(string strUsername, string strPassword,
      out int intId, out string strPurviews, out int intEnabled, out int intIsDeleted)
    {
      DalAdmin.login(strUsername, strPassword,
        out intId, out strPurviews, out intEnabled, out intIsDeleted);
    }

    public static bool hasUsername(string strUsername)
    {
      return DalAdmin.hasUsername(strUsername);
    }

    public static void changeEnabled(int intAdminID)
    {
      DalAdmin.changeEnabled(intAdminID);
    }

    public static void changeIsDeleted(int intAdminID)
    {
      DalAdmin.changeIsDeleted(intAdminID);
    }

    public static string getRealNameById(int intId)
    {
      return DalAdmin.getRealNameById(intId);
    }

  }

}
