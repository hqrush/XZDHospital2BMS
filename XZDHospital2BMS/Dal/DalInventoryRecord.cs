using System;
using System.Data;
using Model;
using MySql.Data.MySqlClient;
using Helper;

namespace Dal
{
  public class DalInventoryRecord
  {

    public static int add(ModelInventoryRecord model)
    {
      string strSQL = @"
INSERT INTO inventory_record (
  id_contract,
  id_goods,
  amount_real,
  amount_show
) VALUES (
  @id_contract,
  @id_goods,
  @amount_real,
  @amount_show
)";
      MySqlParameter[] aryParams = new MySqlParameter[4];
      aryParams[0] = new MySqlParameter("@id_contract", model.id_contract);
      aryParams[1] = new MySqlParameter("@id_goods", model.id_goods);
      aryParams[2] = new MySqlParameter("@amount_real", model.amount_real);
      aryParams[3] = new MySqlParameter("@amount_show", model.amount_show);
      if (HelperMySql.ExecuteNonQuery(strSQL, aryParams) > 0)
      {
        strSQL = "SELECT MAX(id) FROM inventory_record";
        return Convert.ToInt32(HelperMySql.ExecuteScalar(strSQL));
      }
      else return 0;
    }

    public static void deleteById(int intId)
    {
      string strSQL = @"DELETE FROM inventory_record WHERE id=@id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

    public static int update(ModelInventoryRecord model)
    {
      string strSQL = @"
UPDATE inventory_record
SET
  id_contract = @id_contract,
  id_goods = @id_goods,
  amount_real = @amount_real,
  amount_show = @amount_show
WHERE
  id = @id
";
      MySqlParameter[] aryParams = new MySqlParameter[5];
      aryParams[0] = new MySqlParameter("@id_contract", model.id_contract);
      aryParams[1] = new MySqlParameter("@id_goods", model.id_goods);
      aryParams[2] = new MySqlParameter("@amount_real", model.amount_real);
      aryParams[3] = new MySqlParameter("@amount_show", model.amount_show);
      aryParams[4] = new MySqlParameter("@id", model.id);
      return (int)HelperMySql.ExecuteScalar(strSQL, aryParams);
    }

    public static ModelInventoryRecord getById(int intId)
    {
      string strSQL = @"SELECT * FROM inventory_record WHERE id = @id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      if (objDT != null && objDT.Rows.Count > 0)
      {
        ModelInventoryRecord model = new ModelInventoryRecord();
        model.id = Convert.ToInt32(objDT.Rows[0]["id"]);
        model.id_contract = Convert.ToInt32(objDT.Rows[0]["id_contract"]);
        model.id_goods = Convert.ToInt32(objDT.Rows[0]["id_goods"]);
        model.amount_real = Convert.ToInt32(objDT.Rows[0]["amount_real"]);
        model.amount_show = Convert.ToInt32(objDT.Rows[0]["amount_show"]);
        return model;
      }
      else return null;
    }

    public static DataTable getAll()
    {
      string strSQL = @"SELECT * FROM inventory_record";
      return HelperMySql.GetDataTable(strSQL);
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    public static DataTable getPage(int intPage, int intPageSize)
    {
      string strSQL = @"
SELECT *
FROM inventory_record
WHERE id <=
(
  SELECT id
  FROM inventory_record
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
      string strSQL = @"SELECT COUNT(*) FROM inventory_record";
      return Convert.ToInt32(HelperMySql.ExecuteScalar(strSQL));
    }

  }

}