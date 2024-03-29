﻿using Bll;
using Helper;
using Model;
using System;
using System.Web;

namespace XZDHospital2BMS.Handler
{

  public class AddCheckoutGoods : IHttpHandler
  {

    // 添加出库货品记录，并更新货品的库存量
    public void ProcessRequest(HttpContext context)
    {
      context.Response.ContentType = "application/json";
      if (context.Request.Params["CheckoutContractId"] == null ||
        !HelperUtility.isDecimal(context.Request.Params["CheckoutContractId"].ToString()))
      {
        context.Response.Write(HelperUtility.setReturnJson("500", "需要指明出库单ID！", ""));
        return;
      }
      if (context.Request.Params["GoodsId"] == null ||
        !HelperUtility.isDecimal(context.Request.Params["GoodsId"].ToString()))
      {
        context.Response.Write(HelperUtility.setReturnJson("500", "需要指明出库单ID！", ""));
        return;
      }
      if (context.Request.Params["Amount"] == null ||
        !HelperUtility.isDecimal(context.Request.Params["Amount"].ToString()))
      {
        context.Response.Write(HelperUtility.setReturnJson("500", "需要指明数量！", ""));
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
      ModelCheckoutRecord model = new ModelCheckoutRecord();
      model.id_contract = intCheckoutContractId;
      model.id_goods = intGoodsId;
      model.amount = dcmAmount;
      int intId = BllCheckoutRecord.add(model);
      if (intId > 0)
      {
        // 出库货品表里添加完了一条记录后，更新此货品的库存量
        BllSalesGoods.updateAmountStockByCheckOut(dcmAmount, intGoodsId);
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