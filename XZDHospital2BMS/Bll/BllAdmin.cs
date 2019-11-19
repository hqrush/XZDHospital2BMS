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

    public static int update(ModelAdmin model)
    {
      return DalAdmin.update(model);
    }

    public static ModelAdmin getById(int intId)
    {
      return DalAdmin.getById(intId);
    }

    public static DataTable getDataTableAll()
    {
      return DalAdmin.getDataTableAll();
    }

    public static int login(string strUsername, string strPassword)
    {
      return DalAdmin.login(strUsername, strPassword);
    }

  }

}
