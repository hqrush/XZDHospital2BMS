using System;
using System.Data;
using Model;
using MySql.Data.MySqlClient;
using Helper;

namespace Dal
{
  public class DalInventoryContract
  {

    public static int add(ModelInventoryContract model)
    {
      string strSQL = @"
INSERT INTO inventory_contract (
  id_admin,
  name_sign,
  time_create,
  photo_urls,
  comment,
  time_start,
  time_end
) VALUES (
  @id_admin,
  @name_sign,
  @time_create,
  @photo_urls,
  @comment,
  @time_start,
  @time_end
)";
      MySqlParameter[] aryParams = new MySqlParameter[7];
      aryParams[0] = new MySqlParameter("@id_admin", model.id_admin);
      aryParams[1] = new MySqlParameter("@name_sign", model.name_sign);
      aryParams[2] = new MySqlParameter("@time_create", model.time_create);
      aryParams[3] = new MySqlParameter("@photo_urls", model.photo_urls);
      aryParams[4] = new MySqlParameter("@comment", model.comment);
      aryParams[5] = new MySqlParameter("@time_start", model.time_start);
      aryParams[6] = new MySqlParameter("@time_end", model.time_end);
      if (HelperMySql.ExecuteNonQuery(strSQL, aryParams) > 0)
      {
        strSQL = "SELECT MAX(id) FROM inventory_contract";
        object objReturn = HelperMySql.ExecuteScalar(strSQL);
        return objReturn == null ? 0 : Convert.ToInt32(objReturn);
      }
      else return 0;
    }

    public static void deleteById(int intId)
    {
      string strSQL = @"DELETE FROM inventory_contract WHERE id = @id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

    public static void update(ModelInventoryContract model)
    {
      string strSQL = @"
UPDATE inventory_contract
SET
  id_admin = @id_admin,
  name_sign = @name_sign,
  time_create = @time_create,
  photo_urls = @photo_urls,
  comment = @comment,
  time_start = @time_start,
  time_end = @time_end
WHERE
  id = @id
";
      MySqlParameter[] aryParams = new MySqlParameter[8];
      aryParams[0] = new MySqlParameter("@id_admin", model.id_admin);
      aryParams[1] = new MySqlParameter("@name_sign", model.name_sign);
      aryParams[2] = new MySqlParameter("@time_create", model.time_create);
      aryParams[3] = new MySqlParameter("@photo_urls", model.photo_urls);
      aryParams[4] = new MySqlParameter("@comment", model.comment);
      aryParams[5] = new MySqlParameter("@time_start", model.time_start);
      aryParams[6] = new MySqlParameter("@time_end", model.time_end);
      aryParams[7] = new MySqlParameter("@id", model.id);
      HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

    public static ModelInventoryContract getById(int intId)
    {
      string strSQL = @"SELECT * FROM inventory_contract WHERE id = @id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      if (objDT == null || objDT.Rows.Count <= 0) return null;
      ModelInventoryContract model = new ModelInventoryContract();
      model.id = Convert.ToInt32(objDT.Rows[0]["id"]);
      model.id_admin = Convert.ToInt32(objDT.Rows[0]["id_admin"]);
      model.name_sign = Convert.ToString(objDT.Rows[0]["name_sign"]);
      model.time_create = Convert.ToDateTime(objDT.Rows[0]["time_create"]);
      model.photo_urls = Convert.ToString(objDT.Rows[0]["photo_urls"]);
      model.comment = Convert.ToString(objDT.Rows[0]["comment"]);
      model.time_start = Convert.ToDateTime(objDT.Rows[0]["time_start"]);
      model.time_end = Convert.ToDateTime(objDT.Rows[0]["time_end"]);
      return model;
    }

    public static DataTable getAll()
    {
      string strSQL = @"SELECT * FROM inventory_contract";
      return HelperMySql.GetDataTable(strSQL);
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    public static DataTable getPage(int intPage, int intPageSize)
    {
      string strSQL = @"
SELECT *
FROM inventory_contract
WHERE id <=
(
  SELECT id
  FROM inventory_contract
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
      string strSQL = @"SELECT COUNT(*) FROM inventory_contract";
      object objReturn = HelperMySql.ExecuteScalar(strSQL);
      return objReturn == null ? 0 : Convert.ToInt32(objReturn);
    }

  }

}