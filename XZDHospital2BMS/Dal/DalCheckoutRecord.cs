using System;
using System.Data;
using Helper;
using Model;
using MySql.Data.MySqlClient;

namespace Dal
{
  public class DalCheckoutRecord
  {

    public static int add(ModelCheckoutRecord model)
    {
      string strSQL = @"
INSERT INTO checkout_record (
  id_contract,
  id_goods,
  amount
) VALUES (
  @id_contract,
  @id_goods,
  @amount
)";
      MySqlParameter[] aryParams = new MySqlParameter[3];
      aryParams[0] = new MySqlParameter("@id_contract", model.id_contract);
      aryParams[1] = new MySqlParameter("@id_goods", model.id_goods);
      aryParams[2] = new MySqlParameter("@amount", model.amount);
      if (HelperMySql.ExcuteNoQuery(strSQL, aryParams) > 0)
      {
        strSQL = "SELECT MAX(id) FROM checkout_record";
        return Convert.ToInt32(HelperMySql.ExecuteScalar(strSQL));
      }
      else return 0;
    }

    public static void deleteById(int intId)
    {
      string strSQL = @"DELETE FROM checkout_record WHERE id=@id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      HelperMySql.ExcuteNoQuery(strSQL, aryParams);
    }

    public static int update(ModelCheckoutRecord model)
    {
      string strSQL = @"
UPDATE checkout_record
SET
  id_contract = @id_contract,
  id_goods = @id_goods,
  amount = @amount
WHERE
  id = @id
";
      MySqlParameter[] aryParams = new MySqlParameter[4];
      aryParams[0] = new MySqlParameter("@id_contract", model.id_contract);
      aryParams[1] = new MySqlParameter("@id_goods", model.id_goods);
      aryParams[2] = new MySqlParameter("@amount", model.amount);
      aryParams[3] = new MySqlParameter("@id", model.id);
      return HelperMySql.ExecuteScalar(strSQL, aryParams);
    }

    public static ModelCheckoutRecord getById(int intId)
    {
      string strSQL = @"SELECT * FROM checkout_record WHERE id = @id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      if (objDT != null && objDT.Rows.Count > 0)
      {
        ModelCheckoutRecord model = new ModelCheckoutRecord();
        model.id = Convert.ToInt32(objDT.Rows[0]["id"]);
        model.id_contract = Convert.ToInt32(objDT.Rows[0]["id_contract"]);
        model.id_goods = Convert.ToInt32(objDT.Rows[0]["id_goods"]);
        model.amount = Convert.ToInt32(objDT.Rows[0]["amount"]);
        return model;
      }
      else return null;
    }

    public static DataTable getAll()
    {
      string strSQL = @"SELECT * FROM checkout_record";
      return HelperMySql.GetDataTable(strSQL);
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    public static DataTable getPage(int intPage, int intPageSize)
    {
      string strSQL = @"
SELECT *
FROM checkout_record
WHERE id <=
(
  SELECT id
  FROM checkout_record
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
      string strSQL = @"SELECT COUNT(*) FROM checkout_record";
      return Convert.ToInt32(HelperMySql.ExecuteScalar(strSQL));
    }

  }

}