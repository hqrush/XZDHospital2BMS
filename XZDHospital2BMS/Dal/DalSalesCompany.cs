using System;
using System.Data;
using Model;
using MySql.Data.MySqlClient;
using Helper;

namespace Dal
{
  public class DalSalesCompany
  {

    public static int add(ModelSalesCompany model)
    {
      string strSQL = @"
INSERT INTO sales_company (
  name,
  id_admin,
  time_create,
  is_deleted
) VALUES (
  @name,
  @id_admin,
  @time_create,
  @is_deleted
)";
      MySqlParameter[] aryParams = new MySqlParameter[4];
      aryParams[0] = new MySqlParameter("@name", model.name);
      aryParams[1] = new MySqlParameter("@id_admin", model.id_admin);
      aryParams[2] = new MySqlParameter("@time_create", model.time_create);
      aryParams[3] = new MySqlParameter("@is_deleted", model.is_deleted);
      if (HelperMySql.ExecuteNonQuery(strSQL, aryParams) > 0)
      {
        strSQL = "SELECT MAX(id) FROM sales_company";
        object objReturn = HelperMySql.ExecuteScalar(strSQL);
        return objReturn == null ? 0 : Convert.ToInt32(objReturn);
      }
      else return 0;
    }

    public static void deleteById(int intId)
    {
      string strSQL = @"DELETE FROM sales_company WHERE id=@id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

    public static int update(ModelSalesCompany model)
    {
      string strSQL = @"
UPDATE sales_company
SET
  name = @name,
  id_admin = @id_admin,
  time_create = @time_create,
  is_deleted = @is_deleted
WHERE
    id = @id
";
      MySqlParameter[] aryParams = new MySqlParameter[5];
      aryParams[0] = new MySqlParameter("@name", model.name);
      aryParams[1] = new MySqlParameter("@id_admin", model.id_admin);
      aryParams[2] = new MySqlParameter("@time_create", model.time_create);
      aryParams[3] = new MySqlParameter("@is_deleted", model.is_deleted);
      aryParams[4] = new MySqlParameter("@id", model.id);
      return HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

    public static ModelSalesCompany getById(int intId)
    {
      string strSQL = @"SELECT * FROM sales_company WHERE id = @id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      if (objDT == null || objDT.Rows.Count <= 0) return null;
      ModelSalesCompany model = new ModelSalesCompany();
      model.id = Convert.ToInt32(objDT.Rows[0]["id"]);
      model.name = Convert.ToString(objDT.Rows[0]["name"]);
      model.id_admin = Convert.ToInt32(objDT.Rows[0]["id_admin"]);
      model.time_create = Convert.ToDateTime(objDT.Rows[0]["time_create"]);
      model.is_deleted = Convert.ToInt32(objDT.Rows[0]["is_deleted"]);
      return model;
    }

    public static DataTable getAll()
    {
      string strSQL = @"SELECT * FROM sales_company ORDER BY name";
      return HelperMySql.GetDataTable(strSQL);
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    public static DataTable getPage(int intPage, int intPageSize)
    {
      string strSQL = @"
SELECT *
FROM sales_company
WHERE id <=
(
  SELECT id
  FROM sales_company
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
      string strSQL = @"SELECT COUNT(*) FROM sales_company";
      object objReturn = HelperMySql.ExecuteScalar(strSQL);
      return objReturn == null ? 0 : Convert.ToInt32(objReturn);
    }

    public static int getIdByName(string strCompanyName)
    {
      string strSQL = @"SELECT id FROM sales_company WHERE name = @name";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@name", strCompanyName);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      if (objDT != null && objDT.Rows.Count > 0)
        return Convert.ToInt32(objDT.Rows[0]["id"]);
      else return 0;
    }

  }

}