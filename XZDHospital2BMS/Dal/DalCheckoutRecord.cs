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
      if (HelperMySql.ExecuteNonQuery(strSQL, aryParams) > 0)
      {
        strSQL = "SELECT MAX(id) FROM checkout_record";
        object objReturn = HelperMySql.ExecuteScalar(strSQL);
        return objReturn == null ? 0 : Convert.ToInt32(objReturn);
      }
      else return 0;
    }

    public static void deleteById(int intId)
    {
      string strSQL = @"DELETE FROM checkout_record WHERE id=@id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      HelperMySql.ExecuteNonQuery(strSQL, aryParams);
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
      return HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

    public static int updateAmountById(decimal dcmAmount, int intId)
    {
      string strSQL = @"
UPDATE checkout_record
SET
  amount = @amount
WHERE
  id = @id
";
      MySqlParameter[] aryParams = new MySqlParameter[2];
      aryParams[0] = new MySqlParameter("@amount", dcmAmount);
      aryParams[1] = new MySqlParameter("@id", intId);
      return HelperMySql.ExecuteNonQuery(strSQL, aryParams);
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
        model.amount = Convert.ToDecimal(objDT.Rows[0]["amount"]);
        return model;
      }
      else return null;
    }

    // 得到某个出库单下所有的出库货品的记录
    public static DataTable getAll(int intContractId)
    {
      string strSQL = @"
SELECT
  record.id,
  record.id_goods,
  record.amount,
  contract.time_sign,
  goods.name_product,
  goods.name_factory,
  goods.unit,
  goods.price_unit,
  goods.batch_number,
  goods.validity_period,
  goods.type
FROM checkout_record record
INNER JOIN sales_goods goods
ON record.id_goods = goods.id
INNER JOIN sales_contract contract
ON goods.id_contract = contract.id
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
FROM checkout_record
WHERE id <=
(
  SELECT id
  FROM checkout_record
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
      string strSQL = @"SELECT COUNT(*) FROM checkout_record WHERE id_contract = @id_contract";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id_contract", intContractId);
      object objReturn = HelperMySql.ExecuteScalar(strSQL, aryParams);
      return objReturn == null ? 0 : Convert.ToInt32(objReturn);
    }

    /// <summary>
    /// 得到某个出库单下所有货品的总价
    /// </summary>
    /// <param name="intContractId">出库单id</param>
    /// <returns>某个出库单下所有货品的总价</returns>
    public static decimal getPriceTotal(int intContractId)
    {
      string strSQL = @"
SELECT id_goods, amount
FROM checkout_record
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
        dcmAmount = Convert.ToDecimal(objDT.Rows[i]["amount"]);
        dcmPriceUnit = DalSalesGoods.getById(intGoodsId).price_unit;
        dcmPriceTotal += dcmPriceUnit * dcmAmount;
      }
      return dcmPriceTotal;
    }

    /// <summary>
    /// 得到某个货品的总出库数
    /// </summary>
    /// <param name="intGoodsId">出库货品id</param>
    /// <returns>出库记录表中某个货品的总出库数</returns>
    public static decimal getAmountByGoodsId(int intGoodsId)
    {
      string strSQL = @"
SELECT SUM(amount)
FROM checkout_record
WHERE id_goods = @id_goods";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id_goods", intGoodsId);
      object objTotal = HelperMySql.ExecuteScalar(strSQL, aryParams);
      return objTotal == null ? 0 : Convert.ToDecimal(objTotal);
    }

    /// <summary>
    /// 得到某个货品的某个时间点之后的总出库数
    /// </summary>
    /// <param name="intGoodsId">出库货品id</param>
    /// <param name="timeStart">某个时间点之后</param>
    /// <returns>出库记录表中某个货品某个时间点之后的总出库数</returns>
    public static decimal getAmountByGoodsIdAndTime(int intGoodsId, DateTime timeStart)
    {
      string strSQL = @"
SELECT SUM(amount)
FROM checkout_record record
INNER JOIN checkout_contract contract
ON
  record.id_contract = contract.id
WHERE
  record.id_goods = @id_goods AND
  contract.time_create > @time_create
";
      MySqlParameter[] aryParams = new MySqlParameter[2];
      aryParams[0] = new MySqlParameter("@id_goods", intGoodsId);
      aryParams[1] = new MySqlParameter("@time_create", timeStart);
      object objTotal = HelperMySql.ExecuteScalar(strSQL, aryParams);
      return objTotal == null ? 0 : Convert.ToDecimal(objTotal);
    }

  }

}