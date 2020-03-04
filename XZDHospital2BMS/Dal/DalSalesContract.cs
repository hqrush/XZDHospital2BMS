using System;
using System.Data;
using Model;
using MySql.Data.MySqlClient;
using Helper;

namespace Dal
{
  public class DalSalesContract
  {

    public static int add(ModelSalesContract model)
    {
      string strSQL = @"
INSERT INTO sales_contract (
  id_company,
  id_admin,
  time_sign,
  time_create,
  photo_urls,
  comment,
  is_deleted
) VALUES (
  @id_company,
  @id_admin,
  @time_sign,
  @time_create,
  @photo_urls,
  @comment,
  @is_deleted
)";
      MySqlParameter[] aryParams = new MySqlParameter[7];
      aryParams[0] = new MySqlParameter("@id_company", model.id_company);
      aryParams[1] = new MySqlParameter("@id_admin", model.id_admin);
      aryParams[2] = new MySqlParameter("@time_sign", model.time_sign);
      aryParams[3] = new MySqlParameter("@time_create", model.time_create);
      aryParams[4] = new MySqlParameter("@photo_urls", model.photo_urls);
      aryParams[5] = new MySqlParameter("@comment", model.comment);
      aryParams[6] = new MySqlParameter("@is_deleted", model.is_deleted);
      if (HelperMySql.ExecuteNonQuery(strSQL, aryParams) > 0)
      {
        strSQL = "SELECT MAX(id) FROM sales_contract";
        object objReturn = HelperMySql.ExecuteScalar(strSQL);
        return objReturn == null ? 0 : Convert.ToInt32(objReturn);
      }
      else return 0;
    }

    public static void deleteById(int intId)
    {
      string strSQL = @"DELETE FROM sales_contract WHERE id=@id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

    public static int update(ModelSalesContract model)
    {
      string strSQL = @"
UPDATE sales_contract
SET
  id_company = @id_company,
  id_admin = @id_admin,
  time_sign = @time_sign,
  time_create = @time_create,
  photo_urls = @photo_urls,
  comment = @comment,
  is_deleted = @is_deleted
WHERE
  id = @id
";
      MySqlParameter[] aryParams = new MySqlParameter[8];
      aryParams[0] = new MySqlParameter("@id_company", model.id_company);
      aryParams[1] = new MySqlParameter("@id_admin", model.id_admin);
      aryParams[2] = new MySqlParameter("@time_sign", model.time_sign);
      aryParams[3] = new MySqlParameter("@time_create", model.time_create);
      aryParams[4] = new MySqlParameter("@photo_urls", model.photo_urls);
      aryParams[5] = new MySqlParameter("@comment", model.comment);
      aryParams[6] = new MySqlParameter("@is_deleted", model.is_deleted);
      aryParams[7] = new MySqlParameter("@id", model.id);
      return HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

    public static ModelSalesContract getById(int intId)
    {
      string strSQL = @"SELECT * FROM sales_contract WHERE id = @id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      if (objDT == null || objDT.Rows.Count <= 0) return null;
      ModelSalesContract model = new ModelSalesContract();
      model.id = Convert.ToInt32(objDT.Rows[0]["id"]);
      model.id_company = Convert.ToInt32(objDT.Rows[0]["id_company"]);
      model.id_admin = Convert.ToInt32(objDT.Rows[0]["id_admin"]);
      model.time_sign = Convert.ToDateTime(objDT.Rows[0]["time_sign"]);
      model.time_create = Convert.ToDateTime(objDT.Rows[0]["time_create"]);
      model.photo_urls = Convert.ToString(objDT.Rows[0]["photo_urls"]);
      model.comment = Convert.ToString(objDT.Rows[0]["comment"]);
      model.is_deleted = Convert.ToInt16(objDT.Rows[0]["is_deleted"]);
      return model;
    }

    public static DataTable getAll()
    {
      string strSQL = @"SELECT * FROM sales_contract";
      return HelperMySql.GetDataTable(strSQL);
    }

    public static DataTable getForDDL()
    {
      string strSQL = @"
SELECT 
  contract.id,
  contract.time_create,
  company.name,
  concat(DATE_FORMAT(contract.time_create, '%Y-%m-%d') , '-' , company.name) AS text_show
FROM sales_contract contract
  INNER JOIN sales_company company ON company.id = contract.id_company
WHERE id_company > 0
ORDER BY contract.id DESC
";
      return HelperMySql.GetDataTable(strSQL);
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    public static DataTable getPage(int intPage, int intPageSize)
    {
      // 注意选的是所有删除标记是0的记录
      string strSQL = @"
SELECT *
FROM sales_contract
WHERE id <=
(
  SELECT id
  FROM sales_contract
  WHERE is_deleted = 0
  ORDER BY id DESC
  LIMIT " + (intPage - 1) * intPageSize + @" , 1
) AND is_deleted = 0
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
      // 注意选的是所有删除标记是0的记录
      string strSQL = @"SELECT COUNT(*) FROM sales_contract WHERE is_deleted = 0";
      object objReturn = HelperMySql.ExecuteScalar(strSQL);
      return objReturn == null ? 0 : Convert.ToInt32(objReturn);
    }

    public static void changeIsDeleted(int intID)
    {
      string strSQL = @"UPDATE sales_contract SET is_deleted = 1 WHERE id = @id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intID);
      HelperMySql.ExecuteNonQuery(strSQL, aryParams);
      //if (intID <= 0) return;
      //int intIsDeleted = 0;
      //string strSQL = "SELECT is_deleted FROM sales_contract WHERE id = @id";
      //MySqlParameter[] aryParams = new MySqlParameter[1];
      //aryParams[0] = new MySqlParameter("@id", intID);
      //intIsDeleted = Convert.ToInt16(HelperMySql.ExecuteScalar(strSQL, aryParams));
      //if (intIsDeleted == 1)
      //  strSQL = @"UPDATE sales_contract SET is_deleted = 0 WHERE id = @id";
      //else
      //  strSQL = @"UPDATE sales_contract SET is_deleted = 1 WHERE id = @id";
      //aryParams = new MySqlParameter[1];
      //aryParams[0] = new MySqlParameter("@id", intID);
      //HelperMySql.ExcuteNoQuery(strSQL, aryParams);
    }

  }

}