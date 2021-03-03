using Bll;
using Helper;
using Model;
using System;
using System.Web;

namespace XZDHospital2BMS.Handler
{

  public class AddInventoryRecord : IHttpHandler
  {

    public void ProcessRequest(HttpContext context)
    {
      context.Response.ContentType = "application/json";
      if (context.Request.Params["InventoryContractId"] == null ||
        !HelperUtility.isDecimal(context.Request.Params["InventoryContractId"].ToString()))
      {
        context.Response.Write(HelperUtility.setReturnJson("500", "需要指明盘点单ID！", ""));
        return;
      }
      if (context.Request.Params["GoodsId"] == null ||
        !HelperUtility.isDecimal(context.Request.Params["GoodsId"].ToString()))
      {
        context.Response.Write(HelperUtility.setReturnJson("500", "需要指明盘点货品ID！", ""));
        return;
      }
      if (context.Request.Params["Amount"] == null ||
        !HelperUtility.isDecimal(context.Request.Params["Amount"].ToString()))
      {
        context.Response.Write(HelperUtility.setReturnJson("500", "需要指明货品盘点数量！", ""));
        return;
      }

      int intCheckoutContractId = Convert.ToInt32(context.Request.Params["CheckoutContractId"]);
      int intGoodsId = Convert.ToInt32(context.Request.Params["GoodsId"]);
      decimal dcmAmount = Convert.ToDecimal(context.Request.Params["Amount"]);
      if (!(intCheckoutContractId > 0 && intGoodsId > 0 && dcmAmount > 0))
      {
        context.Response.Write(HelperUtility.setReturnJson("500", "数字不对！", ""));
        return;
      }
      ModelInventoryRecord model = new ModelInventoryRecord();
      model.id_contract = intCheckoutContractId;
      model.id_goods = intGoodsId;
      model.amount_real = dcmAmount;
      model.amount_stock = dcmAmount;
      model.amount_fill = dcmAmount;
      int intId = BllInventoryRecord.add(model);
      if (intId > 0)
      {
        context.Response.Write(HelperUtility.setReturnJson("200", "", intId.ToString()));
        return;
      }
      else
      {
        context.Response.Write(HelperUtility.setReturnJson("500", "添加失败，请联系管理员！", ""));
        return;
      }
    }

    public bool IsReusable
    {
      get
      {
        return false;
      }
    }

  }

}