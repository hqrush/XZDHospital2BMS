using System;
using System.Data;
using Helper;
using Model;
using MySql.Data.MySqlClient;

namespace Dal
{
  public class DalCheckoutContract
  {

    public static int add(ModelCheckoutContract model)
    {
      string strSQL = @"
INSERT INTO checkout_contract (
  id_admin,
  time_create,
  name_unit,
  name_department,
  name_sign,
  photo_urls,
  comment
) VALUES (
  @id_admin,
  @time_create,
  @name_unit,
  @name_department,
  @name_sign,
  @photo_urls,
  @comment
)";
      MySqlParameter[] aryParams = new MySqlParameter[7];
      aryParams[0] = new MySqlParameter("@id_admin", model.id_admin);
      aryParams[1] = new MySqlParameter("@time_create", model.time_create);
      aryParams[2] = new MySqlParameter("@name_unit", model.name_unit);
      aryParams[3] = new MySqlParameter("@name_department", model.name_department);
      aryParams[4] = new MySqlParameter("@name_sign", model.name_sign);
      aryParams[5] = new MySqlParameter("@photo_urls", model.photo_urls);
      aryParams[6] = new MySqlParameter("@comment", model.comment);
      if (HelperMySql.ExecuteNonQuery(strSQL, aryParams) > 0)
      {
        strSQL = "SELECT MAX(id) FROM checkout_contract";
        object objReturn = HelperMySql.ExecuteScalar(strSQL);
        return objReturn == null ? 0 : Convert.ToInt32(objReturn);
      }
      else return 0;
    }

    public static void deleteById(int intId)
    {
      string strSQL = @"DELETE FROM checkout_contract WHERE id = @id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

    public static int update(ModelCheckoutContract model)
    {
      string strSQL = @"
UPDATE checkout_contract
SET
  id_admin = @id_admin,
  time_create = @time_create,
  name_unit = @name_unit,
  name_department = @name_department,
  name_sign = @name_sign,
  photo_urls = @photo_urls,
  comment = @comment
WHERE
  id = @id
";
      MySqlParameter[] aryParams = new MySqlParameter[8];
      aryParams[0] = new MySqlParameter("@id_admin", model.id_admin);
      aryParams[1] = new MySqlParameter("@time_create", model.time_create);
      aryParams[2] = new MySqlParameter("@name_unit", model.name_unit);
      aryParams[3] = new MySqlParameter("@name_department", model.name_department);
      aryParams[4] = new MySqlParameter("@name_sign", model.name_sign);
      aryParams[5] = new MySqlParameter("@photo_urls", model.photo_urls);
      aryParams[6] = new MySqlParameter("@comment", model.comment);
      aryParams[7] = new MySqlParameter("@id", model.id);
      return (int)HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

    public static ModelCheckoutContract getById(int intId)
    {
      string strSQL = @"SELECT * FROM checkout_contract WHERE id = @id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      if (objDT != null && objDT.Rows.Count > 0)
      {
        ModelCheckoutContract model = new ModelCheckoutContract();
        model.id = Convert.ToInt32(objDT.Rows[0]["id"]);
        model.id_admin = Convert.ToInt32(objDT.Rows[0]["id_admin"]);
        model.time_create = Convert.ToDateTime(objDT.Rows[0]["time_create"]);
        model.name_unit = Convert.ToString(objDT.Rows[0]["name_unit"]);
        model.name_department = Convert.ToString(objDT.Rows[0]["name_department"]);
        model.name_sign = Convert.ToString(objDT.Rows[0]["name_sign"]);
        model.photo_urls = Convert.ToString(objDT.Rows[0]["photo_urls"]);
        model.comment = Convert.ToString(objDT.Rows[0]["comment"]);
        return model;
      }
      else return null;
    }

    public static DataTable getAll()
    {
      string strSQL = @"SELECT * FROM checkout_contract";
      return HelperMySql.GetDataTable(strSQL);
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    public static DataTable getPage(int intPage, int intPageSize)
    {
      string strSQL = @"
SELECT *
FROM checkout_contract
WHERE id <=
(
  SELECT id
  FROM checkout_contract
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
      string strSQL = @"SELECT COUNT(*) FROM checkout_contract";
      object objReturn = HelperMySql.ExecuteScalar(strSQL);
      return objReturn == null ? 0 : Convert.ToInt32(objReturn);
    }

  }

}