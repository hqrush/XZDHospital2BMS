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

    public static void update(ModelInventoryRecord model)
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
      HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

    // 修改盘点记录的盘点数，修改真实记录时同时修改现实值
    public static void updateRealById(decimal dcmAmountReal, int intId)
    {
      string strSQL = @"
UPDATE inventory_record
SET
  amount_real = @amount_real,
  amount_show = @amount_real
WHERE
  id = @id
";
      MySqlParameter[] aryParams = new MySqlParameter[2];
      aryParams[0] = new MySqlParameter("@amount_real", dcmAmountReal);
      aryParams[1] = new MySqlParameter("@id", intId);
      HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

    public static void updateShowById(decimal dcmAmountShow, int intId)
    {
      string strSQL = @"
UPDATE inventory_record
SET
  amount_show = @amount_show
WHERE
  id = @id
";
      MySqlParameter[] aryParams = new MySqlParameter[2];
      aryParams[0] = new MySqlParameter("@amount_show", dcmAmountShow);
      aryParams[1] = new MySqlParameter("@id", intId);
      HelperMySql.ExecuteNonQuery(strSQL, aryParams);
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
        model.amount_real = Convert.ToDecimal(objDT.Rows[0]["amount_real"]);
        model.amount_show = Convert.ToDecimal(objDT.Rows[0]["amount_show"]);
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
  record.amount_show,
  contract.time_sign,
  goods.name_product,
  goods.name_factory,
  goods.price_unit,
  goods.batch_number,
  goods.validity_period,
  goods.type,
  goods.amount_stock
FROM inventory_record record
INNER JOIN sales_goods goods
ON record.id_goods = goods.id
INNER JOIN sales_contract contract
ON goods.id_contract = contract.id
WHERE record.id_contract = @id_contract
ORDER BY contract.time_sign ASC, goods.name_product ASC
";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id_contract", intContractId);
      return HelperMySql.GetDataTable(strSQL, aryParams);
    }

    /// <summary>
    /// 分页查询某个盘点单下所有盘点记录
    /// </summary>
    public static DataTable getPage(int intContractId, int intPage, int intPageSize)
    {
      string strSQL = @"
SELECT
  record.id,
  record.id_goods,
  record.amount_real,
  record.amount_show,
  contract.time_sign,
  goods.name_product,
  goods.name_factory,
  goods.price_unit,
  goods.batch_number,
  goods.validity_period,
  goods.type,
  goods.amount_stock
FROM inventory_record record
INNER JOIN sales_goods goods
ON record.id_goods = goods.id
INNER JOIN sales_contract contract
ON goods.id_contract = contract.id
WHERE record.id <=
(
  SELECT id
  FROM inventory_record
  WHERE id_contract = @id_contract
  ORDER BY id DESC
  LIMIT " + (intPage - 1) * intPageSize + @" , 1
) AND record.id_contract = @id_contract
ORDER BY id DESC
LIMIT @PageSize
";
      MySqlParameter[] aryParams = new MySqlParameter[2];
      aryParams[0] = new MySqlParameter("@id_contract", intContractId);
      aryParams[1] = new MySqlParameter("@PageSize", intPageSize);
      return HelperMySql.GetDataTable(strSQL, aryParams);
    }

    /// <summary>
    /// 得到某个盘点单下所有盘点记录的总数
    /// </summary>
    public static int getRecordsAmount(int intContractId)
    {
      string strSQL = @"SELECT COUNT(*) FROM inventory_record WHERE id_contract = @id_contract";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id_contract", intContractId);
      object objReturn = HelperMySql.ExecuteScalar(strSQL, aryParams);
      return objReturn == null ? 0 : Convert.ToInt32(objReturn);
    }

    // 根据查询条件得到盘点记录
    public static DataTable getByQuery(int intContractId, string strProductName)
    {
      string strSQL = @"
SELECT
  record.id,
  record.id_goods,
  record.amount_real,
  record.amount_show,
  contract.time_sign,
  goods.name_product,
  goods.name_factory,
  goods.price_unit,
  goods.batch_number,
  goods.validity_period,
  goods.type,
  goods.amount_stock
FROM inventory_record record
INNER JOIN sales_goods goods
ON record.id_goods = goods.id
INNER JOIN sales_contract contract
ON goods.id_contract = contract.id
WHERE
  record.id_contract = @id_contract AND
  goods.name_product LIKE CONCAT('%', @name_product, '%')
ORDER BY contract.time_sign ASC, goods.name_product ASC
";
      MySqlParameter[] aryParams = new MySqlParameter[2];
      aryParams[0] = new MySqlParameter("@id_contract", intContractId);
      aryParams[1] = new MySqlParameter("@name_product", strProductName);
      return HelperMySql.GetDataTable(strSQL, aryParams);
    }

    /// <summary>
    /// 得到某个盘点单下所有货品的库存总价
    /// </summary>
    /// <param name="intContractId">盘点单id</param>
    /// <returns>某个盘点单下所有货品的库存总价</returns>
    public static decimal getPriceTotalStock(int intContractId)
    {
      string strSQL = @"
SELECT
  goods.price_unit,
  goods.amount_stock
FROM inventory_record record
INNER JOIN sales_goods goods
ON record.id_goods = goods.id
WHERE record.id_contract = @id_contract";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id_contract", intContractId);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      if (objDT == null || objDT.Rows.Count <= 0) return 0;

      decimal dcmPriceUnit;
      decimal dcmAmountStock;
      decimal dcmPriceTotal = 0;
      for (int i = 0; i < objDT.Rows.Count; i++)
      {
        dcmPriceUnit = Convert.ToDecimal(objDT.Rows[i]["price_unit"]);
        dcmAmountStock = Convert.ToDecimal(objDT.Rows[i]["amount_stock"]);
        dcmPriceTotal += dcmPriceUnit * dcmAmountStock;
      }
      return dcmPriceTotal;
    }

    /// <summary>
    /// 得到某个盘点单下所有货品的盘点总价数组，第一个数字是真实盘点数，第二个数字是显示数
    /// </summary>
    /// <param name="intContractId">盘点单id</param>
    /// <returns>某个盘点单下所有货品的盘点总价</returns>
    public static decimal[] getPriceTotalInventory(int intContractId)
    {
      decimal[] aryReturn = { 0, 0 };
      string strSQL = @"
SELECT
  record.amount_real,
  record.amount_show,
  goods.price_unit
FROM inventory_record record
INNER JOIN sales_goods goods
ON record.id_goods = goods.id
WHERE record.id_contract = @id_contract";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id_contract", intContractId);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      if (objDT == null || objDT.Rows.Count <= 0) return aryReturn;

      decimal dcmAmountReal, dcmAmountShow;
      decimal dcmPriceUnit;
      decimal dcmPriceTotalReal = 0;
      decimal dcmPriceTotalShow = 0;
      for (int i = 0; i < objDT.Rows.Count; i++)
      {
        dcmAmountReal = Convert.ToDecimal(objDT.Rows[i]["amount_real"]);
        dcmAmountShow = Convert.ToDecimal(objDT.Rows[i]["amount_show"]);
        dcmPriceUnit = Convert.ToDecimal(objDT.Rows[i]["price_unit"]);
        dcmPriceTotalReal += dcmPriceUnit * dcmAmountReal;
        dcmPriceTotalShow += dcmPriceUnit * dcmAmountShow;
      }
      aryReturn[0] = dcmPriceTotalReal;
      aryReturn[1] = dcmPriceTotalShow;
      return aryReturn;
    }

    // 新建某盘点单时，将所有库存量大于0的货品加到盘点货品表里，盘点数默认为0
    public static void setRecord(int intContractId)
    {
      int intGoodsId;
      decimal dcmAmountStock;
      ModelInventoryRecord model;
      // 找出所有库存量大于0的货品id
      string strSQL = @"
SELECT id, amount_stock
FROM sales_goods
WHERE amount_stock > 0
ORDER BY time_add DESC
";
      DataTable objDT = HelperMySql.GetDataTable(strSQL);
      if (objDT == null || objDT.Rows.Count <= 0) return;
      for (int i = 0; i < objDT.Rows.Count; i++)
      {
        intGoodsId = Convert.ToInt32(objDT.Rows[i]["id"]);
        dcmAmountStock = Convert.ToDecimal(objDT.Rows[i]["amount_stock"]);
        if (dcmAmountStock > 0)
        {
          model = new ModelInventoryRecord();
          model.id_contract = intContractId;
          model.id_goods = intGoodsId;
          model.amount_real = 0;
          model.amount_show = 0;
          add(model);
        }
      }
    }

  }

}