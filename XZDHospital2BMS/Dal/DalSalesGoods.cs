using System;
using System.Data;
using Model;
using MySql.Data.MySqlClient;
using Helper;

namespace Dal
{
  public class DalSalesGoods
  {

    public static int add(ModelSalesGoods model)
    {
      string strSQL = @"
INSERT INTO sales_goods (
  id_contract,
  name_product,
  type,
  name_factory,
  unit,
  amount,
  price_unit,
  price_total,
  batch_number,
  validity_period,
  approval_number,
  comment,
  photo_urls,
  id_admin,
  time_add
) VALUES (
  @id_contract,
  @name_product,
  @type,
  @name_factory,
  @unit,
  @amount,
  @price_unit,
  @price_total,
  @batch_number,
  @validity_period,
  @approval_number,
  @comment,
  @photo_urls,
  @id_admin,
  @time_add
)";
      MySqlParameter[] aryParams = new MySqlParameter[15];
      aryParams[0] = new MySqlParameter("@id_contract", model.id_contract);
      aryParams[1] = new MySqlParameter("@name_product", model.name_product);
      aryParams[2] = new MySqlParameter("@type", model.type);
      aryParams[3] = new MySqlParameter("@name_factory", model.name_factory);
      aryParams[4] = new MySqlParameter("@unit", model.unit);
      aryParams[5] = new MySqlParameter("@amount", model.amount);
      aryParams[6] = new MySqlParameter("@price_unit", model.price_unit);
      aryParams[7] = new MySqlParameter("@price_total", model.price_total);
      aryParams[8] = new MySqlParameter("@batch_number", model.batch_number);
      aryParams[9] = new MySqlParameter("@validity_period", model.validity_period);
      aryParams[10] = new MySqlParameter("@approval_number", model.approval_number);
      aryParams[11] = new MySqlParameter("@comment", model.comment);
      aryParams[12] = new MySqlParameter("@photo_urls", model.photo_urls);
      aryParams[13] = new MySqlParameter("@id_admin", model.id_admin);
      aryParams[14] = new MySqlParameter("@time_add", model.time_add);
      if (HelperMySql.ExcuteNoQuery(strSQL, aryParams) > 0)
      {
        strSQL = "SELECT MAX(id) FROM sales_goods";
        return Convert.ToInt32(HelperMySql.ExecuteScalar(strSQL));
      }
      else return 0;
    }

    public static void deleteById(int intId)
    {
      string strSQL = @"DELETE FROM sales_goods WHERE id=@id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      HelperMySql.ExcuteNoQuery(strSQL, aryParams);
    }

    public static int update(ModelSalesGoods model)
    {
      string strSQL = @"
UPDATE sales_goods
SET
  id_contract = @id_contract,
  name_product = @name_product,
  type = @type,
  name_factory = @name_factory,
  unit = @unit,
  amount = @amount,
  price_unit = @price_unit,
  price_total = @price_total,
  batch_number = @batch_number,
  validity_period = @validity_period,
  approval_number = @approval_number,
  comment = @comment,
  photo_urls = @photo_urls,
  id_admin = @id_admin,
  time_add = @time_add
WHERE
  id = @id
";
      MySqlParameter[] aryParams = new MySqlParameter[16];
      aryParams[0] = new MySqlParameter("@id_contract", model.id_contract);
      aryParams[1] = new MySqlParameter("@name_product", model.name_product);
      aryParams[2] = new MySqlParameter("@type", model.type);
      aryParams[3] = new MySqlParameter("@name_factory", model.name_factory);
      aryParams[4] = new MySqlParameter("@unit", model.unit);
      aryParams[5] = new MySqlParameter("@amount", model.amount);
      aryParams[6] = new MySqlParameter("@price_unit", model.price_unit);
      aryParams[7] = new MySqlParameter("@price_total", model.price_total);
      aryParams[8] = new MySqlParameter("@batch_number", model.batch_number);
      aryParams[9] = new MySqlParameter("@validity_period", model.validity_period);
      aryParams[10] = new MySqlParameter("@approval_number", model.approval_number);
      aryParams[11] = new MySqlParameter("@comment", model.comment);
      aryParams[12] = new MySqlParameter("@photo_urls", model.photo_urls);
      aryParams[13] = new MySqlParameter("@id_admin", model.id_admin);
      aryParams[14] = new MySqlParameter("@time_add", model.time_add);
      aryParams[15] = new MySqlParameter("@id", model.id);
      return HelperMySql.ExecuteScalar(strSQL, aryParams);
    }

    public static ModelSalesGoods getById(int intId)
    {
      string strSQL = @"SELECT * FROM sales_goods WHERE id = @id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      if (objDT == null || objDT.Rows.Count <= 0) return null;
      ModelSalesGoods model = new ModelSalesGoods();
      model.id = Convert.ToInt32(objDT.Rows[0]["id"]);
      model.id_contract = Convert.ToInt32(objDT.Rows[0]["id_contract"]);
      model.name_product = Convert.ToString(objDT.Rows[0]["name_product"]);
      model.type = Convert.ToString(objDT.Rows[0]["type"]);
      model.name_factory = Convert.ToString(objDT.Rows[0]["name_factory"]);
      model.unit = Convert.ToString(objDT.Rows[0]["unit"]);
      model.amount = Convert.ToDecimal(objDT.Rows[0]["amount"]);
      model.price_unit = Convert.ToDecimal(objDT.Rows[0]["price_unit"]);
      model.price_total = Convert.ToDecimal(objDT.Rows[0]["price_total"]);
      model.batch_number = Convert.ToString(objDT.Rows[0]["batch_number"]);
      model.validity_period = Convert.ToDateTime(objDT.Rows[0]["validity_period"]);
      model.approval_number = Convert.ToString(objDT.Rows[0]["approval_number"]);
      model.comment = Convert.ToString(objDT.Rows[0]["comment"]);
      model.photo_urls = Convert.ToString(objDT.Rows[0]["photo_urls"]);
      model.id_contract = Convert.ToInt32(objDT.Rows[0]["id_admin"]);
      model.validity_period = Convert.ToDateTime(objDT.Rows[0]["time_add"]);
      return model;
    }

    public static DataTable getAll(int intContractId)
    {
      string strSQL = @"SELECT * FROM sales_goods WHERE id_contract = @id_contract";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id_contract", intContractId);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      if (objDT == null || objDT.Rows.Count <= 0) return null;
      return HelperMySql.GetDataTable(strSQL);
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    public static DataTable getPage(int intContractId, int intPage, int intPageSize)
    {
      string strSQL = @"
SELECT *
FROM sales_goods
WHERE id <=
(
  SELECT id
  FROM sales_goods
  WHERE id_contract = @id_contract
  ORDER BY id DESC
  LIMIT " + (intPage - 1) * intPageSize + @" , 1
) AND id_contract = @id_contract
ORDER BY id DESC
LIMIT @PageSize
";
      MySqlParameter[] aryParams = new MySqlParameter[2];
      aryParams[0] = new MySqlParameter("@id_contract", intContractId);
      aryParams[1] = new MySqlParameter("@PageSize", intPageSize);
      return HelperMySql.GetDataTable(strSQL, aryParams);
    }

    /// <summary>
    /// 得到记录总数
    /// </summary>
    public static int getRecordsAmount(int intContractId)
    {
      string strSQL = @"SELECT COUNT(*) FROM sales_goods WHERE id_contract = @id_contract";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id_contract", intContractId);
      return Convert.ToInt32(HelperMySql.ExecuteScalar(strSQL, aryParams));
    }

  }

}