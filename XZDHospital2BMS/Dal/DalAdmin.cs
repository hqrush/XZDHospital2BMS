using System;
using System.Data;
using Model;
using MySql.Data.MySqlClient;
using XZDHospital2BMS.Helper;

namespace Dal
{
  public class DalAdmin
  {

    public static ModelAdmin getById(int intId)
    {
      string strSQL = @"SELECT * FROM sys_admin WHERE id = @id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      if (objDT != null && objDT.Rows.Count > 0)
      {
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
      else return null;
    }

    public static int add(ModelAdmin model)
    {
      string strSQL = @"
INSERT INTO sys_admin (
  id,
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
  @id,
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
      MySqlParameter[] aryParams = new MySqlParameter[13];
      aryParams[0] = new MySqlParameter("@id", model.id);
      aryParams[1] = new MySqlParameter("@username", model.username);
      aryParams[2] = new MySqlParameter("@password", model.password);
      aryParams[3] = new MySqlParameter("@salt", model.salt);
      aryParams[4] = new MySqlParameter("@real_name", model.real_name);
      aryParams[5] = new MySqlParameter("@id_card", model.id_card);
      aryParams[6] = new MySqlParameter("@mobile_phone", model.mobile_phone);
      aryParams[7] = new MySqlParameter("@avatar_url", model.avatar_url);
      aryParams[8] = new MySqlParameter("@time_add", model.time_add);
      aryParams[9] = new MySqlParameter("@time_last_login", model.time_last_login);
      aryParams[10] = new MySqlParameter("@enabled", model.enabled);
      aryParams[11] = new MySqlParameter("@purviews", model.purviews);
      aryParams[12] = new MySqlParameter("@is_deleted", model.is_deleted);
      if (HelperMySql.ExcuteNoQuery(strSQL, aryParams) > 0)
      {
        strSQL = "SELECT MAX(id) FROM sys_admin";
        return Convert.ToInt32(HelperMySql.ExecuteScalar(strSQL));
      }
      else return 0;
    }

    public static int update(ModelAdmin model)
    {
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
      MySqlParameter[] aryParams = new MySqlParameter[14];
      aryParams[0] = new MySqlParameter("@id", model.id);
      aryParams[1] = new MySqlParameter("@username", model.username);
      aryParams[2] = new MySqlParameter("@password", model.password);
      aryParams[3] = new MySqlParameter("@salt", model.salt);
      aryParams[4] = new MySqlParameter("@real_name", model.real_name);
      aryParams[5] = new MySqlParameter("@id_card", model.id_card);
      aryParams[6] = new MySqlParameter("@mobile_phone", model.mobile_phone);
      aryParams[7] = new MySqlParameter("@avatar_url", model.avatar_url);
      aryParams[8] = new MySqlParameter("@time_add", model.time_add);
      aryParams[9] = new MySqlParameter("@time_last_login", model.time_last_login);
      aryParams[10] = new MySqlParameter("@enabled", model.enabled);
      aryParams[11] = new MySqlParameter("@purviews", model.purviews);
      aryParams[12] = new MySqlParameter("@is_deleted", model.is_deleted);
      aryParams[13] = new MySqlParameter("@aid", model.id);
      return HelperMySql.ExecuteScalar(strSQL, aryParams);
    }

    public static void deleteById(int intId)
    {
      string strSQL = @"DELETE FROM sys_admin WHERE id=@id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      HelperMySql.ExcuteNoQuery(strSQL, aryParams);
    }

    public static int login(string strUsername, string strPassword)
    {
      string strSQL = @"SELECT id FROM sys_admin WHERE username = @username AND password = @password";
      MySqlParameter[] aryParams = new MySqlParameter[2];
      aryParams[0] = new MySqlParameter("@username", strUsername);
      aryParams[1] = new MySqlParameter("@password", strPassword);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      if (objDT != null && objDT.Rows.Count > 0)
        return Convert.ToInt32(objDT.Rows[0][0]);
      else
        return 0;
    }

  }

}