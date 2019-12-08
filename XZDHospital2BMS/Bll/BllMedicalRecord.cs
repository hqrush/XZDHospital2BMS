using Dal;
using Model;
using System.Data;

namespace Bll
{
  public class BllMedicalRecord
  {

    public static int add(ModelMedicalRecord model)
    {
      return DalMedicalRecord.add(model);
    }

    public static void deleteById(int intId)
    {
      DalMedicalRecord.deleteById(intId);
    }

    public static void update(ModelMedicalRecord model)
    {
      DalMedicalRecord.update(model);
    }

    public static ModelMedicalRecord getById(int intId)
    {
      return DalMedicalRecord.getById(intId);
    }

    public static DataTable getAll()
    {
      return DalMedicalRecord.getAll();
    }

    public static DataTable getPage(int intPage, int intPageSize)
    {
      return DalMedicalRecord.getPage(intPage, intPageSize);
    }

    public static int getRecordsAmount()
    {
      return DalMedicalRecord.getRecordsAmount();
    }

    public static void changeIsDeleted(int intMedicalRecordID)
    {
      DalMedicalRecord.changeIsDeleted(intMedicalRecordID);
    }

  }

}
