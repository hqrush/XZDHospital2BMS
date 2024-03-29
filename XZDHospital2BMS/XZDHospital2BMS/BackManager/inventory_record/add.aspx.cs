﻿using Bll;
using Helper;
using Model;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace XZDHospital2BMS.BackManager.inventory_record
{

  public partial class add : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.hasPurviewPage("InventoryRecord_add");
        ViewState["AdminId"] = intAdminId;
        int intCId = HelperUtility.getQueryInt("cid");
        ViewState["ContractId"] = intCId;
        int intCPage = HelperUtility.getQueryInt("cpage");
        ViewState["ContractPage"] = intCPage;
        pnlInfo.Visible = false;
        gvShow.Visible = false;
      }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
      string strUrlBack = "?cid=" + ViewState["ContractId"] + "&cpage=" + ViewState["ContractPage"];
      string strNameProduct, strNameFactory;
      strNameProduct = tbProductName.Value.Trim();
      strNameFactory = tbFactoryName.Value.Trim();
      if ("".Equals(strNameProduct) && "".Equals(strNameFactory))
      {
        HelperUtility.showAlert("货品名称和厂家名称不能都为空！", "add.aspx" + strUrlBack);
        return;
      }
      ViewState["NameProduct"] = strNameProduct;
      ViewState["NameFactory"] = strNameFactory;
      LoadData();
    }

    protected void btnShowListAll_Click(object sender, EventArgs e)
    {
      string strUrlBack = "?cid=" + ViewState["ContractId"] + "&cpage=" + ViewState["ContractPage"];
      Response.Redirect("list.aspx" + strUrlBack);
    }

    protected void gvShow_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#e1f2e9'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");

        int intContractId = Convert.ToInt32(ViewState["ContractId"]);
        Label lblGoodsId = (Label)e.Row.FindControl("lblGoodsId");
        int intGoodsId = Convert.ToInt32(lblGoodsId.Text);
        if (BllInventoryRecord.isRecordAdded(intContractId, intGoodsId))
        {
          Button btnAddGoods = (Button)e.Row.FindControl("btnAddGoods");
          btnAddGoods.Text = "已添加";
          btnAddGoods.Enabled = false;
          return;
        }
        HyperLink hlProductName = (HyperLink)e.Row.FindControl("hlProductName");
        hlProductName.NavigateUrl = "/BackManager/sales_goods/show.aspx?id=" + intGoodsId;
        // 显示入库量、计算库存、实时库存要用到的Label控件
        Label lblAmountIn = (Label)e.Row.FindControl("lblAmountIn");
        Label lblAmountReal = (Label)e.Row.FindControl("lblAmountReal");
        // 得到入库的货品总量
        decimal intIn = Convert.ToDecimal(lblAmountIn.Text);
        // 得到实际出库的货品总量
        decimal intOut = BllCheckoutRecord.getAmountByGoodsId(intGoodsId);
        // 显示计算库存量
        lblAmountReal.Text = (intIn - intOut).ToString("N");
      }
    }

    public void OP_Command(object sender, CommandEventArgs e)
    {
      int intIdGoods = Convert.ToInt32(e.CommandArgument);
      int intIdContract = Convert.ToInt32(ViewState["ContractId"]);
      string strUrl;
      if (e.CommandName == "AddGoods")
      {
        if (!HelperUtility.hasPurviewOP("InventoryContract_update"))
        {
          HelperUtility.showAlert("没有操作权限", "list.aspx?page=" + ViewState["page"]);
        }
        int rowIndex = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
        GridViewRow row = gvShow.Rows[rowIndex];
        TextBox tbAmountFill = (TextBox)row.FindControl("tbAmountFill");
        if (tbAmountFill is null) return;
        Label lblAmountReal = (Label)row.FindControl("lblAmountReal");
        Label lblAmountStock = (Label)row.FindControl("lblAmountStock");

        ModelInventoryRecord model = new ModelInventoryRecord();
        model.id_contract = intIdContract;
        model.id_goods = intIdGoods;
        model.amount_real = Convert.ToDecimal(lblAmountReal.Text);
        model.amount_stock = Convert.ToDecimal(lblAmountStock.Text);
        model.amount_fill = Convert.ToDecimal(tbAmountFill.Text);
        BllInventoryRecord.add(model);
      }
      LoadData();
    }

    public void LoadData()
    {
      string strNameProduct = ViewState["NameProduct"].ToString();
      string strNameFactory = ViewState["NameFactory"].ToString();
      DataTable objDT = BllSalesGoods.getDTByName(strNameProduct, strNameFactory);
      if (objDT == null || objDT.Rows.Count <= 0)
      {
        pnlInfo.Visible = true;
        gvShow.Visible = false;
      }
      else
      {
        pnlInfo.Visible = false;
        gvShow.Visible = true;
      }
      gvShow.DataSource = objDT;
      gvShow.DataBind();
    }

  }

}