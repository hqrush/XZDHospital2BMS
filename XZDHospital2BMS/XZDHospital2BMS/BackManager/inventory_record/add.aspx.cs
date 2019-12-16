using Bll;
using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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

    protected void btnShowList_Click(object sender, EventArgs e)
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

        Label lblGoodsId = (Label)e.Row.FindControl("lblGoodsId");
        int intGoodsId = Convert.ToInt32(lblGoodsId.Text);
        HyperLink hlProductName = (HyperLink)e.Row.FindControl("hlProductName");
        hlProductName.NavigateUrl = "/BackManager/sales_goods/show.aspx?id=" + intGoodsId;
        // 显示入库量、出库量、库存量要用到的Label控件
        Label lblAmountIn = (Label)e.Row.FindControl("lblAmountIn");
        Label lblAmountOut = (Label)e.Row.FindControl("lblAmountOut");
        Label lblStock = (Label)e.Row.FindControl("lblStock");
        // 得到入库的货品总量
        decimal intIn = Convert.ToDecimal(lblAmountIn.Text);
        // 得到出库的货品总量
        decimal intOut = BllCheckoutRecord.getAmountByGoodsId(intGoodsId);
        lblAmountOut.Text = intOut.ToString("N");
        // 计算并显示库存量
        lblStock.Text = (intIn - intOut).ToString("N");
      }
    }

    protected void gvShow_RowEditing(object sender, GridViewEditEventArgs e)
    {
      gvShow.EditIndex = e.NewEditIndex;
      LoadData();
    }

    protected void gvShow_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      string strId = gvShow.DataKeys[e.RowIndex].Values[0].ToString();
      TextBox tbInventoryAmount = (TextBox)gvShow.Rows[e.RowIndex].FindControl("tbInventoryAmount");
      if ("".Equals(tbInventoryAmount.Text)) tbInventoryAmount.Text = "0";
      decimal dcmInventoryAmount = Convert.ToDecimal(tbInventoryAmount.Text);
      gvShow.EditIndex = -1;
      LoadData();
    }

    protected void gvShow_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
      gvShow.EditIndex = -1;
      LoadData();
    }

    public void LoadData()
    {
      string strNameProduct = ViewState["NameProduct"].ToString();
      string strNameFactory = ViewState["NameFactory"].ToString();
      DataTable objDT = BllSalesGoods.getInventoryDTByName(strNameProduct, strNameFactory);
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