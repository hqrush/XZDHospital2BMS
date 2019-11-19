using System;
using System.Data;
using Model;
using MySql.Data.MySqlClient;
using XZDHospital2BMS.Helper;

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
  comment
) VALUES (
  @id_company,
  @id_admin,
  @time_sign,
  @time_create,
  @photo_urls,
  @comment
)";
      MySqlParameter[] aryParams = new MySqlParameter[6];
      aryParams[0] = new MySqlParameter("@id_company", model.id_company);
      aryParams[1] = new MySqlParameter("@id_admin", model.id_admin);
      aryParams[2] = new MySqlParameter("@time_sign", model.time_sign);
      aryParams[3] = new MySqlParameter("@time_create", model.time_create);
      aryParams[4] = new MySqlParameter("@photo_urls", model.photo_urls);
      aryParams[5] = new MySqlParameter("@comment", model.comment);
      if (HelperMySql.ExcuteNoQuery(strSQL, aryParams) > 0)
      {
        strSQL = "SELECT MAX(id) FROM sales_contract";
        return Convert.ToInt32(HelperMySql.ExecuteScalar(strSQL));
      }
      else return 0;
    }

    public static void deleteById(int intId)
    {
      string strSQL = @"DELETE FROM sales_contract WHERE id=@id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      HelperMySql.ExcuteNoQuery(strSQL, aryParams);
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
  comment = @comment
WHERE
  id = @id
";
      MySqlParameter[] aryParams = new MySqlParameter[7];
      aryParams[0] = new MySqlParameter("@id_company", model.id_company);
      aryParams[1] = new MySqlParameter("@id_admin", model.id_admin);
      aryParams[2] = new MySqlParameter("@time_sign", model.time_sign);
      aryParams[3] = new MySqlParameter("@time_create", model.time_create);
      aryParams[4] = new MySqlParameter("@photo_urls", model.photo_urls);
      aryParams[5] = new MySqlParameter("@comment", model.comment);
      aryParams[6] = new MySqlParameter("@id", model.id);
      return HelperMySql.ExecuteScalar(strSQL, aryParams);
    }

    public static ModelSalesContract getById(int intId)
    {
      string strSQL = @"SELECT * FROM sales_contract WHERE id = @id";
      MySqlParameter[] aryParams = new MySqlParameter[1];
      aryParams[0] = new MySqlParameter("@id", intId);
      DataTable objDT = HelperMySql.GetDataTable(strSQL, aryParams);
      if (objDT != null && objDT.Rows.Count > 0)
      {
        ModelSalesContract model = new ModelSalesContract();
        model.id = Convert.ToInt32(objDT.Rows[0]["id"]);
        model.id_company = Convert.ToInt32(objDT.Rows[0]["id_company"]);
        model.id_admin = Convert.ToInt32(objDT.Rows[0]["id_admin"]);
        model.time_sign = Convert.ToDateTime(objDT.Rows[0]["time_sign"]);
        model.time_create = Convert.ToDateTime(objDT.Rows[0]["time_create"]);
        model.photo_urls = Convert.ToString(objDT.Rows[0]["photo_urls"]);
        model.comment = Convert.ToString(objDT.Rows[0]["comment"]);
        return model;
      }
      else return null;
    }

    public static DataTable getDataTableAll()
    {
      string strSQL = @"SELECT * FROM sales_contract";
      return HelperMySql.GetDataTable(strSQL);
    }

  }

}