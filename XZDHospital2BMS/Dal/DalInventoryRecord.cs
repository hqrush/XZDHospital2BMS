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
        object objReturn = HelperMySql.ExecuteScalar(strSQL);
        return objReturn == null ? 0 : Convert.ToInt32(objReturn);
      }
      else return 0;
    }

    public static void deleteById(int intId)
    {
      string strSQL = @"DELETE FROM inventory_record WHERE id = @id";
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
      return HelperMySql.ExecuteNonQuery(strSQL, aryParams);
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

    public static DataTable getAll(int intContractId)
    {
      string strSQL = @"
SELECT
  record.id,
  record.id_goods,
  record.amount_real,
  contract.time_sign,
  goods.name_product,
  goods.name_factory,
  goods.price_unit,
  goods.batch_number,
  goods.validity_period,
  goods.type
FROM inventory_record record
INNER JOIN sales_contract contract
ON
  record.id_contract = contract.id
INNER JOIN sales_goods goods
ON
  record.id_goods = goods.id
WHERE
  record.id_contract = @id_contract
";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id_contract", intContractId);
      return HelperMySql.GetDataTable(strSQL, aryParams);
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    public static DataTable getPage(int intContractId, int intPage, int intPageSize)
    {
      string strSQL = @"
SELECT *
FROM inventory_record
WHERE id <=
(
  SELECT id
  FROM inventory_record
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
      string strSQL = @"SELECT COUNT(*) FROM inventory_record WHERE id_contract = @id_contract";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id_contract", intContractId);
      object objReturn = HelperMySql.ExecuteScalar(strSQL, aryParams);
      return objReturn == null ? 0 : Convert.ToInt32(objReturn);
    }

    /// <summary>
    /// 得到某个盘点单下所有货品的总价
    /// </summary>
    /// <param name="intContractId">盘点单id</param>
    /// <returns>某个盘点单下所有货品的总价</returns>
    public static decimal getPriceTotal(int intContractId)
    {
      string strSQL = @"
SELECT id_goods, amount_real
FROM inventory_record
WHERE id_contract = @id_contract";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id_contract", intContractId);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      if (objDT == null || objDT.Rows.Count <= 0) return 0;
      int intGoodsId;
      decimal dcmAmount;
      decimal dcmPriceUnit;
      decimal dcmPriceTotal = 0;
      for (int i = 0; i < objDT.Rows.Count; i++)
      {
        intGoodsId = Convert.ToInt32(objDT.Rows[i]["id_goods"]);
        dcmAmount = Convert.ToDecimal(objDT.Rows[i]["amount_real"]);
        dcmPriceUnit = DalSalesGoods.getById(intGoodsId).price_unit;
        dcmPriceTotal += dcmPriceUnit * dcmAmount;
      }
      return dcmPriceTotal;
    }

    /// <summary>
    /// 得到某个某个货品的盘点数
    /// </summary>
    /// <param name="intGoodsId">货品id</param>
    /// <returns>某个盘点单下所有货品的总数量</returns>
    public static decimal getAmountByGoodsId(int intGoodsId)
    {
      string strSQL = @"
SELECT SUM(amount_real)
FROM inventory_record
WHERE id_goods = @id_goods";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id_goods", intGoodsId);
      object objTotal = HelperMySql.ExecuteScalar(strSQL, aryParams);
      return objTotal == null ? 0 : Convert.ToDecimal(objTotal);
    }

  }

}