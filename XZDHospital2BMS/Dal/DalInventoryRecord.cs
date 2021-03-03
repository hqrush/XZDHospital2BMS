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
  amount_stock,
  amount_fill
) VALUES (
  @id_contract,
  @id_goods,
  @amount_real,
  @amount_stock,
  @amount_fill
)";
      MySqlParameter[] aryParams = new MySqlParameter[5];
      aryParams[0] = new MySqlParameter("@id_contract", model.id_contract);
      aryParams[1] = new MySqlParameter("@id_goods", model.id_goods);
      aryParams[2] = new MySqlParameter("@amount_real", model.amount_real);
      aryParams[3] = new MySqlParameter("@amount_stock", model.amount_stock);
      aryParams[4] = new MySqlParameter("@amount_fill", model.amount_fill);
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
  amount_stock = @amount_stock,
  amount_fill = @amount_fill
WHERE
  id = @id
";
      MySqlParameter[] aryParams = new MySqlParameter[6];
      aryParams[0] = new MySqlParameter("@id_contract", model.id_contract);
      aryParams[1] = new MySqlParameter("@id_goods", model.id_goods);
      aryParams[2] = new MySqlParameter("@amount_real", model.amount_real);
      aryParams[3] = new MySqlParameter("@amount_stock", model.amount_stock);
      aryParams[4] = new MySqlParameter("@amount_fill", model.amount_fill);
      aryParams[5] = new MySqlParameter("@id", model.id);
      HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

    /// <summary>
    /// 修改盘点记录的真实计算库存数
    /// </summary>
    public static void updateRealById(decimal dcmAmountReal, int intId)
    {
      string strSQL = @"
UPDATE inventory_record
SET
  amount_real = @amount_real
WHERE
  id = @id
";
      MySqlParameter[] aryParams = new MySqlParameter[2];
      aryParams[0] = new MySqlParameter("@amount_real", dcmAmountReal);
      aryParams[1] = new MySqlParameter("@id", intId);
      HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

    /// <summary>
    /// 修改盘点记录的实时库存数
    /// </summary>
    public static void updateStockById(decimal dcmAmountStock, int intId)
    {
      string strSQL = @"
UPDATE inventory_record
SET
  amount_stock = @amount_stock
WHERE
  id = @id
";
      MySqlParameter[] aryParams = new MySqlParameter[2];
      aryParams[0] = new MySqlParameter("@amount_stock", dcmAmountStock);
      aryParams[1] = new MySqlParameter("@id", intId);
      HelperMySql.ExecuteNonQuery(strSQL, aryParams);
    }

    /// <summary>
    /// 修改盘点记录的库存盘点数
    /// </summary>
    public static void updateFillById(decimal dcmAmountFill, int intId)
    {
      string strSQL = @"
UPDATE inventory_record
SET
  amount_fill = @amount_fill
WHERE
  id = @id
";
      MySqlParameter[] aryParams = new MySqlParameter[2];
      aryParams[0] = new MySqlParameter("@amount_fill", dcmAmountFill);
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
        model.amount_stock = Convert.ToDecimal(objDT.Rows[0]["amount_stock"]);
        model.amount_fill = Convert.ToDecimal(objDT.Rows[0]["amount_fill"]);
        return model;
      }
      else return null;
    }

    /// <summary>
    /// 得到某盘点单下所有盘点货品记录，用于生成excel表的数据
    /// </summary>
    /// <param name="intContractId"></param>
    /// <returns></returns>
    public static DataTable getAll(int intContractId)
    {
      string strSQL = @"
SELECT
  record.id,
  record.id_goods,
  record.amount_real,
  record.amount_stock,
  record.amount_fill,
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
  record.amount_stock,
  record.amount_fill,
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
  record.amount_stock,
  record.amount_fill,
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
    /// 得到某个盘点单下所有货品的盘点总价数组，第一个数字是真实库存数，第二个数字是实时库存数，第三个数字是盘点库存数
    /// </summary>
    /// <param name="intContractId">盘点单id</param>
    /// <returns>某个盘点单下所有货品的盘点总价</returns>
    public static decimal[] getPriceTotalInventory(int intContractId)
    {
      decimal[] aryReturn = { 0, 0, 0 };
      string strSQL = @"
SELECT
  record.amount_real,
  record.amount_stock,
  record.amount_fill,
  goods.price_unit
FROM inventory_record record
INNER JOIN sales_goods goods
ON record.id_goods = goods.id
WHERE record.id_contract = @id_contract";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id_contract", intContractId);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      if (objDT == null || objDT.Rows.Count <= 0) return aryReturn;
      decimal dcmAmountReal, dcmAmountStock, dcmAmountFill;
      decimal dcmPriceUnit;
      decimal dcmPriceTotalReal = 0;
      decimal dcmPriceTotalStock = 0;
      decimal dcmPriceTotalFill = 0;
      for (int i = 0; i < objDT.Rows.Count; i++)
      {
        if (objDT.Rows[i]["amount_real"] is DBNull) dcmAmountReal = 0;
        else dcmAmountReal = Convert.ToDecimal(objDT.Rows[i]["amount_real"]);
        if (objDT.Rows[i]["amount_stock"] is DBNull) dcmAmountStock = 0;
        else dcmAmountStock = Convert.ToDecimal(objDT.Rows[i]["amount_stock"]);
        if (objDT.Rows[i]["amount_fill"] is DBNull) dcmAmountFill = 0;
        else dcmAmountFill = Convert.ToDecimal(objDT.Rows[i]["amount_fill"]);
        dcmPriceUnit = Convert.ToDecimal(objDT.Rows[i]["price_unit"]);
        dcmPriceTotalReal += dcmPriceUnit * dcmAmountReal;
        dcmPriceTotalStock += dcmPriceUnit * dcmAmountStock;
        dcmPriceTotalFill += dcmPriceUnit * dcmAmountFill;
      }
      aryReturn[0] = dcmPriceTotalReal;
      aryReturn[1] = dcmPriceTotalStock;
      aryReturn[2] = dcmPriceTotalFill;
      return aryReturn;
    }

    /// <summary>
    /// 新建某盘点单时，计算某货品的真实库存量，然后将所有真实计算库存量大于0的货品加到盘点货品表里
    /// </summary>
    /// <param name="intContractId">盘点单id</param>
    public static void setInventoryRecord(int intContractId)
    {
      int intGoodsId;
      decimal dcmAmount, dcmAmountStock, dcmAmountStockReal;
      ModelInventoryRecord model;
      // 查询所有货品
      string strSQL = @"
SELECT id, amount, amount_stock
FROM sales_goods
ORDER BY time_add DESC
";
      DataTable objDT = HelperMySql.GetDataTable(strSQL);
      if (objDT == null || objDT.Rows.Count <= 0) return;
      // 循环遍历所有货品
      for (int i = 0; i < objDT.Rows.Count; i++)
      {
        intGoodsId = Convert.ToInt32(objDT.Rows[i]["id"]);
        dcmAmount = Convert.ToDecimal(objDT.Rows[i]["amount"]);
        dcmAmountStock = Convert.ToDecimal(objDT.Rows[i]["amount_stock"]);
        // 真实库存量
        dcmAmountStockReal = dcmAmount - DalCheckoutRecord.getAmountByGoodsId(intGoodsId);
        // 如果记录的库存量或者计算的真实库存量大于0，就将该货品加到盘点表里
        if (dcmAmountStock > 0 || dcmAmountStockReal > 0)
        {
          model = new ModelInventoryRecord();
          model.id_contract = intContractId;
          model.id_goods = intGoodsId;
          model.amount_real = dcmAmountStockReal;
          model.amount_stock = dcmAmountStock;
          model.amount_fill = 0;
          add(model);
          // 将某货品的库存清零
          // DalSalesGoods.clearAmountStock(intGoodsId);
        }
      }
    }

    // 手动添加盘点货品时，根据货品名称或者货品生产厂家查找货品时，要先判断此货品是否已经在盘点单中
    public static bool isRecordAdded(int intContractId, int intGoodsId)
    {
      string strSQL = @"
SELECT id
FROM inventory_record
WHERE id_contract = @id_contract
AND id_goods = @id_goods
";
      MySqlParameter[] aryParams = new MySqlParameter[2];
      aryParams[0] = new MySqlParameter("@id_contract", intContractId);
      aryParams[1] = new MySqlParameter("@id_goods", intGoodsId);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      return (objDT != null && objDT.Rows.Count > 0);
    }

    /// <summary>
    /// 将某盘点单中所有货品的库存量全部清0
    /// </summary>
    /// <param name="intContractId">盘点单id</param>
    public static void clearZero(int intContractId)
    {
      int intGoodsId;
      // 找出所有在此盘点单中的货品
      string strSQL = @"
SELECT goods.id, goods.amount_stock
FROM sales_goods goods
LEFT JOIN inventory_record inventory
ON inventory.id_goods = goods.id
WHERE inventory.id_contract = @id_contract
";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id_contract", intContractId);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      if (objDT == null || objDT.Rows.Count <= 0) return;
      for (int i = 0; i < objDT.Rows.Count; i++)
      {
        intGoodsId = Convert.ToInt32(objDT.Rows[i]["id"]);
        // 执行清零操作
        DalSalesGoods.clearAmountStock(intGoodsId);
      }
    }

  }

}