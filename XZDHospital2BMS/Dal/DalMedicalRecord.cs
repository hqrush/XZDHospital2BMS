using Helper;
using Model;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Dal
{

  public class DalMedicalRecord
  {

    public static int add(ModelMedicalRecord model)
    {
      string strSQL = @"
INSERT INTO medical_record (
  sickbed_number,
  name_real,
  sex,
  birthday,
  department,
  name_disease,
  time_in,
  time_out,
  situation_out,
  situation_in,
  photo_urls,
  comment,
  time_create,
  id_admin,
  is_deleted
) VALUES (
  @sickbed_number,
  @name_real,
  @sex,
  @birthday,
  @department,
  @name_disease,
  @time_in,
  @time_out,
  @situation_out,
  @situation_in,
  @photo_urls,
  @comment,
  @time_create,
  @id_admin,
  @is_deleted
)";
      MySqlParameter[] aryParams = new MySqlParameter[15];
      aryParams[0] = new MySqlParameter("@sickbed_number", model.sickbed_number);
      aryParams[1] = new MySqlParameter("@name_real", model.name_real);
      aryParams[2] = new MySqlParameter("@sex", model.sex);
      aryParams[3] = new MySqlParameter("@birthday", model.birthday);
      aryParams[4] = new MySqlParameter("@department", model.department);
      aryParams[5] = new MySqlParameter("@name_disease", model.name_disease);
      aryParams[6] = new MySqlParameter("@time_in", model.time_in);
      aryParams[7] = new MySqlParameter("@time_out", model.time_out);
      aryParams[8] = new MySqlParameter("@situation_out", model.situation_out);
      aryParams[9] = new MySqlParameter("@situation_in", model.situation_in);
      aryParams[10] = new MySqlParameter("@photo_urls", model.photo_urls);
      aryParams[11] = new MySqlParameter("@comment", model.comment);
      aryParams[12] = new MySqlParameter("@time_create", model.time_create);
      aryParams[13] = new MySqlParameter("@id_admin", model.id_admin);
      aryParams[14] = new MySqlParameter("@is_deleted", model.is_deleted);
      if (HelperMySql.ExecuteNonQuery(strSQL, aryParams) > 0)
      {
        strSQL = "SELECT MAX(id) FROM medical_record";
        object objReturn = HelperMySql.ExecuteScalar(strSQL);
        return objReturn == null ? 0 : Convert.ToInt32(objReturn);
      }
      else return 0;
    }

    public static void deleteById(int intId)
    {
      string strSQL = @"
DELETE FROM medical_record
WHERE
  id = @id
";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

    public static void update(ModelMedicalRecord model)
    {
      string strSQL = @"
UPDATE medical_record
SET
  sickbed_number = @sickbed_number,
  name_real = @name_real,
  sex = @sex,
  birthday = @birthday,
  department = @department,
  name_disease = @name_disease,
  time_in = @time_in,
  time_out = @time_out,
  situation_out = @situation_out,
  situation_in = @situation_in,
  photo_urls = @photo_urls,
  comment = @comment,
  time_create = @time_create,
  id_admin = @id_admin,
  is_deleted = @is_deleted
WHERE
  id = @id
";
      MySqlParameter[] aryParams = new MySqlParameter[16];
      aryParams[0] = new MySqlParameter("@sickbed_number", model.sickbed_number);
      aryParams[1] = new MySqlParameter("@name_real", model.name_real);
      aryParams[2] = new MySqlParameter("@sex", model.sex);
      aryParams[3] = new MySqlParameter("@birthday", model.birthday);
      aryParams[4] = new MySqlParameter("@department", model.department);
      aryParams[5] = new MySqlParameter("@name_disease", model.name_disease);
      aryParams[6] = new MySqlParameter("@time_in", model.time_in);
      aryParams[7] = new MySqlParameter("@time_out", model.time_out);
      aryParams[8] = new MySqlParameter("@situation_out", model.situation_out);
      aryParams[9] = new MySqlParameter("@situation_in", model.situation_in);
      aryParams[10] = new MySqlParameter("@photo_urls", model.photo_urls);
      aryParams[11] = new MySqlParameter("@comment", model.comment);
      aryParams[12] = new MySqlParameter("@time_create", model.time_create);
      aryParams[13] = new MySqlParameter("@id_admin", model.id_admin);
      aryParams[14] = new MySqlParameter("@is_deleted", model.is_deleted);
      aryParams[15] = new MySqlParameter("@id", model.id);
      HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

    public static ModelMedicalRecord getById(int intId)
    {
      string strSQL = @"SELECT * FROM medical_record WHERE id = @id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      if (objDT == null || objDT.Rows.Count <= 0) return null;

      ModelMedicalRecord model = new ModelMedicalRecord();
      model.id = Convert.ToInt32(objDT.Rows[0]["id"]);
      model.sickbed_number = Convert.ToString(objDT.Rows[0]["sickbed_number"]);
      model.name_real = Convert.ToString(objDT.Rows[0]["name_real"]);
      model.sex = Convert.ToString(objDT.Rows[0]["sex"]);
      model.birthday = Convert.ToDateTime(objDT.Rows[0]["birthday"]);
      model.department = Convert.ToString(objDT.Rows[0]["department"]);
      model.name_disease = Convert.ToString(objDT.Rows[0]["name_disease"]);
      model.time_in = Convert.ToDateTime(objDT.Rows[0]["time_in"]);
      model.time_out = Convert.ToDateTime(objDT.Rows[0]["time_out"]);
      model.situation_out = Convert.ToString(objDT.Rows[0]["situation_out"]);
      model.situation_in = Convert.ToString(objDT.Rows[0]["situation_in"]);
      model.photo_urls = Convert.ToString(objDT.Rows[0]["photo_urls"]);
      model.comment = Convert.ToString(objDT.Rows[0]["comment"]);
      model.time_create = Convert.ToDateTime(objDT.Rows[0]["time_create"]);
      model.id_admin = Convert.ToInt32(objDT.Rows[0]["id_admin"]);
      model.is_deleted = Convert.ToInt16(objDT.Rows[0]["is_deleted"]);
      return model;
    }

    public static DataTable getAll()
    {
      string strSQL = @"SELECT * FROM medical_record";
      return HelperMySql.GetDataTable(strSQL);
    }

    /// <summary>
    /// 分页查询所有病历
    /// </summary>
    public static DataTable getPage(int intPage, int intPageSize)
    {
      string strSQL = @"
SELECT *
FROM medical_record
WHERE id <=
(
  SELECT id
  FROM medical_record
  ORDER BY id DESC
  LIMIT " + (intPage - 1) * intPageSize + @" , 1
)
ORDER BY id DESC
LIMIT @PageSize
";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@PageSize", intPageSize);
      return HelperMySql.GetDataTable(strSQL, aryParams);
    }

    /// <summary>
    /// 得到记录总数
    /// </summary>
    public static int getRecordsAmount()
    {
      string strSQL = @"SELECT COUNT(*) FROM medical_record";
      object objReturn = HelperMySql.ExecuteScalar(strSQL);
      return objReturn == null ? 0 : Convert.ToInt32(objReturn);
    }

    public static void changeIsDeleted(int intAdminID)
    {
      if (intAdminID <= 0) return;
      int intIsDeleted = 0;
      string strSQL = "SELECT is_deleted FROM medical_record WHERE id = @id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intAdminID);
      intIsDeleted = Convert.ToInt16(HelperMySql.ExecuteScalar(strSQL, aryParams));
      if (intIsDeleted == 1)
        strSQL = @"UPDATE medical_record SET is_deleted = 0 WHERE id = @id";
      else
        strSQL = @"UPDATE medical_record SET is_deleted = 1 WHERE id = @id";
      aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intAdminID);
      HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

  }

}
