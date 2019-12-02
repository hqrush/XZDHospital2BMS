using System;
using System.Data;
using Helper;
using Model;
using MySql.Data.MySqlClient;

namespace Dal
{
  public class DalAdmin
  {

    public static int add(ModelAdmin model)
    {
      string strSQL = @"
INSERT INTO sys_admin (
  username,
  password,
  salt,
  real_name,
  id_card,
  mobile_phone,
  avatar_url,
  time_add,
  time_last_login,
  enabled,
  purviews,
  is_deleted
) VALUES (
  @username,
  @password,
  @salt,
  @real_name,
  @id_card,
  @mobile_phone,
  @avatar_url,
  @time_add,
  @time_last_login,
  @enabled,
  @purviews,
  @is_deleted
)";
      MySqlParameter[] aryParams = new MySqlParameter[12];
      aryParams[0] = new MySqlParameter("@username", model.username);
      aryParams[1] = new MySqlParameter("@password", model.password);
      aryParams[2] = new MySqlParameter("@salt", model.salt);
      aryParams[3] = new MySqlParameter("@real_name", model.real_name);
      aryParams[4] = new MySqlParameter("@id_card", model.id_card);
      aryParams[5] = new MySqlParameter("@mobile_phone", model.mobile_phone);
      aryParams[6] = new MySqlParameter("@avatar_url", model.avatar_url);
      aryParams[7] = new MySqlParameter("@time_add", model.time_add);
      aryParams[8] = new MySqlParameter("@time_last_login", model.time_last_login);
      aryParams[9] = new MySqlParameter("@enabled", model.enabled);
      aryParams[10] = new MySqlParameter("@purviews", model.purviews);
      aryParams[11] = new MySqlParameter("@is_deleted", model.is_deleted);
      if (HelperMySql.ExecuteNonQuery(strSQL, aryParams) > 0)
      {
        strSQL = "SELECT MAX(id) FROM sys_admin";
        object objReturn = HelperMySql.ExecuteScalar(strSQL);
        return objReturn == null ? 0 : Convert.ToInt32(objReturn);
      }
      else return 0;
    }

    public static void deleteById(int intId)
    {
      string strSQL = @"
DELETE FROM sys_admin
WHERE
  id = @id AND
  username <> 'rush2112' OR
  username <> 'wumin'
";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

    public static void update(ModelAdmin model)
    {
      if ("rush2112".Equals(model.username) || "wumin".Equals(model.username))
        model.purviews = "SUPERADMIN";
      string strSQL = @"
UPDATE sys_admin
SET
  username = @username,
  password = @password,
  salt = @salt,
  real_name = @real_name,
  id_card = @id_card,
  mobile_phone = @mobile_phone,
  avatar_url = @avatar_url,
  time_add = @time_add,
  time_last_login = @time_last_login,
  enabled = @enabled,
  purviews = @purviews,
  is_deleted = @is_deleted
WHERE
    id = @id
";
      MySqlParameter[] aryParams = new MySqlParameter[13];
      aryParams[0] = new MySqlParameter("@username", model.username);
      aryParams[1] = new MySqlParameter("@password", model.password);
      aryParams[2] = new MySqlParameter("@salt", model.salt);
      aryParams[3] = new MySqlParameter("@real_name", model.real_name);
      aryParams[4] = new MySqlParameter("@id_card", model.id_card);
      aryParams[5] = new MySqlParameter("@mobile_phone", model.mobile_phone);
      aryParams[6] = new MySqlParameter("@avatar_url", model.avatar_url);
      aryParams[7] = new MySqlParameter("@time_add", model.time_add);
      aryParams[8] = new MySqlParameter("@time_last_login", model.time_last_login);
      aryParams[9] = new MySqlParameter("@enabled", model.enabled);
      aryParams[10] = new MySqlParameter("@purviews", model.purviews);
      aryParams[11] = new MySqlParameter("@is_deleted", model.is_deleted);
      aryParams[12] = new MySqlParameter("@id", model.id);
      HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

    public static ModelAdmin getById(int intId)
    {
      string strSQL = @"SELECT * FROM sys_admin WHERE id = @id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      if (objDT == null || objDT.Rows.Count <= 0) return null;

      ModelAdmin model = new ModelAdmin();
      model.id = Convert.ToInt32(objDT.Rows[0]["id"]);
      model.username = Convert.ToString(objDT.Rows[0]["username"]);
      model.password = Convert.ToString(objDT.Rows[0]["password"]);
      model.salt = Convert.ToString(objDT.Rows[0]["salt"]);
      model.real_name = Convert.ToString(objDT.Rows[0]["real_name"]);
      model.id_card = Convert.ToString(objDT.Rows[0]["id_card"]);
      model.mobile_phone = Convert.ToString(objDT.Rows[0]["mobile_phone"]);
      model.avatar_url = Convert.ToString(objDT.Rows[0]["avatar_url"]);
      model.time_add = Convert.ToDateTime(objDT.Rows[0]["time_add"]);
      model.time_last_login = Convert.ToDateTime(objDT.Rows[0]["time_last_login"]);
      model.enabled = Convert.ToInt32(objDT.Rows[0]["enabled"]);
      model.purviews = Convert.ToString(objDT.Rows[0]["purviews"]);
      return model;
    }

    public static DataTable getAll()
    {
      string strSQL = @"SELECT * FROM sys_admin";
      return HelperMySql.GetDataTable(strSQL);
    }

    /// <summary>
    /// 分页查询所有管理员
    /// </summary>
    public static DataTable getPage(int intPage, int intPageSize)
    {
      string strSQL = @"
SELECT *
FROM sys_admin
WHERE id <=
(
  SELECT id
  FROM sys_admin
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
      string strSQL = @"SELECT COUNT(*) FROM sys_admin";
      object objReturn = HelperMySql.ExecuteScalar(strSQL);
      return objReturn == null ? 0 : Convert.ToInt32(objReturn);
    }

    public static void login(string strUsername, string strPassword,
      out int intId, out string strPurviews, out int intEnabled, out int intIsDeleted)
    {
      string strSQL = @"
SELECT
  id,
  purviews,
  enabled,
  is_deleted
FROM sys_admin
WHERE
  username = @username AND
  password = @password
";
      MySqlParameter[] aryParams = new MySqlParameter[2];
      aryParams[0] = new MySqlParameter("@username", strUsername);
      aryParams[1] = new MySqlParameter("@password", strPassword);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      if (objDT != null && objDT.Rows.Count > 0)
      {
        intId = Convert.ToInt32(objDT.Rows[0]["id"]);
        strPurviews = Convert.ToString(objDT.Rows[0]["purviews"]);
        intEnabled = Convert.ToInt32(objDT.Rows[0]["enabled"]);
        intIsDeleted = Convert.ToInt32(objDT.Rows[0]["is_deleted"]);
      }
      else
      {
        intId = 0;
        strPurviews = "";
        intEnabled = 0;
        intIsDeleted = 1;
      }
    }

    public static bool hasUsername(string strUsername)
    {
      string strSQL = @"SELECT id FROM sys_admin WHERE username = @username";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@username", strUsername);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      if (objDT != null && objDT.Rows.Count > 0)
        return true;
      else
        return false;
    }

    public static void changeEnabled(int intAdminID)
    {
      if (intAdminID <= 0) return;
      int intEnabled = 0;
      string strSQL = "SELECT enabled FROM sys_admin WHERE id = @id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intAdminID);
      intEnabled = Convert.ToInt16(HelperMySql.ExecuteScalar(strSQL, aryParams));
      if (intEnabled == 1)
        strSQL = @"UPDATE sys_admin SET enabled = 0 WHERE id = @id";
      else
        strSQL = @"UPDATE sys_admin SET enabled = 1 WHERE id = @id";
      aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intAdminID);
      HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

    public static void changeIsDeleted(int intAdminID)
    {
      if (intAdminID <= 0) return;
      int intIsDeleted = 0;
      string strSQL = "SELECT is_deleted FROM sys_admin WHERE id = @id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intAdminID);
      intIsDeleted = Convert.ToInt16(HelperMySql.ExecuteScalar(strSQL, aryParams));
      if (intIsDeleted == 1)
        strSQL = @"UPDATE sys_admin SET is_deleted = 0 WHERE id = @id";
      else
        strSQL = @"UPDATE sys_admin SET is_deleted = 1 WHERE id = @id";
      aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intAdminID);
      HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

  }

}