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
  comment
) VALUES (
  @id_admin,
  @name_sign,
  @time_create,
  @photo_urls,
  @comment
)";
      MySqlParameter[] aryParams = new MySqlParameter[5];
      aryParams[0] = new MySqlParameter("@id_admin", model.id_admin);
      aryParams[1] = new MySqlParameter("@name_sign", model.name_sign);
      aryParams[2] = new MySqlParameter("@time_create", model.time_create);
      aryParams[3] = new MySqlParameter("@photo_urls", model.photo_urls);
      aryParams[4] = new MySqlParameter("@comment", model.comment);
      if (HelperMySql.ExecuteNonQuery(strSQL, aryParams) > 0)
      {
        strSQL = "SELECT MAX(id) FROM inventory_contract";
        return Convert.ToInt32(HelperMySql.ExecuteScalar(strSQL));
      }
      else return 0;
    }

    public static void deleteById(int intId)
    {
      string strSQL = @"DELETE FROM inventory_contract WHERE id=@id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

    public static int update(ModelInventoryContract model)
    {
      string strSQL = @"
UPDATE inventory_contract
SET
  id_admin = @id_admin,
  name_sign = @name_sign,
  time_create = @time_create,
  photo_urls = @photo_urls,
  comment = @comment
WHERE
  id = @id
";
      MySqlParameter[] aryParams = new MySqlParameter[6];
      aryParams[0] = new MySqlParameter("@id_admin", model.id_admin);
      aryParams[1] = new MySqlParameter("@name_sign", model.name_sign);
      aryParams[2] = new MySqlParameter("@time_create", model.time_create);
      aryParams[3] = new MySqlParameter("@photo_urls", model.photo_urls);
      aryParams[4] = new MySqlParameter("@comment", model.comment);
      aryParams[5] = new MySqlParameter("@id", model.id);
      return (int)HelperMySql.ExecuteScalar(strSQL, aryParams);
    }

    public static ModelInventoryContract getById(int intId)
    {
      string strSQL = @"SELECT * FROM inventory_contract WHERE id = @id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      if (objDT != null && objDT.Rows.Count > 0)
      {
        ModelInventoryContract model = new ModelInventoryContract();
        model.id = Convert.ToInt32(objDT.Rows[0]["id"]);
        model.id_admin = Convert.ToInt32(objDT.Rows[0]["id_admin"]);
        model.name_sign = Convert.ToString(objDT.Rows[0]["name_sign"]);
        model.time_create = Convert.ToDateTime(objDT.Rows[0]["time_create"]);
        model.photo_urls = Convert.ToString(objDT.Rows[0]["photo_urls"]);
        model.comment = Convert.ToString(objDT.Rows[0]["comment"]);
        return model;
      }
      else return null;
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
      return Convert.ToInt32(HelperMySql.ExecuteScalar(strSQL));
    }

  }

}