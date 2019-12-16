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
      LoadData(strNameProduct, strNameFactory);
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
        // 库存量的显示及更新要用到的Label控件
        Label lblAmountIn = (Label)e.Row.FindControl("lblAmountIn");
        Label lblAmountOut = (Label)e.Row.FindControl("lblAmountOut");
        Label lblStock = (Label)e.Row.FindControl("lblStock");
        // 得到入库的货品总量
        if ("".Equals(lblAmountIn.Text)) lblAmountIn.Text = "0";
        decimal intIn = Convert.ToDecimal(lblAmountIn.Text);
        // 得到出库的货品总量
        decimal intOut = BllCheckoutRecord.getAmountByGoodsId(intGoodsId);
        // 计算并显示库存量
        lblStock.Text = (intIn - intOut).ToString("N");
        // 根据盘点单id，货品id，盘点数量添加一条盘点货品记录
        // 先得到输入盘点数量的textbox控件id
        TextBox tbInventoryAmount = (TextBox)e.Row.FindControl("tbInventoryAmount");
        HtmlInputButton btnAddToList = (HtmlInputButton)e.Row.FindControl("btnAddToList");
        if ("".Equals(tbInventoryAmount.Text)) tbInventoryAmount.Text = "0";
        int intInventoryContractId = Convert.ToInt32(ViewState["ContractId"]);
        string strClickHandler = "addGoods(" +
          intInventoryContractId + "," +
          intGoodsId + ",\"" +
          tbInventoryAmount.ClientID + "\")";
        // 将上述值绑定到按钮事件上
        btnAddToList.Attributes.Add("onclick", strClickHandler);
      }
    }

    public void OP_Command(object sender, CommandEventArgs e)
    {
      int intId = Convert.ToInt32(e.CommandArgument);
      string strUrlBack = "?cid=" + ViewState["ContractId"] + "&cpage=" + ViewState["ContractPage"];
      if (e.CommandName == "edit")
      {
        if (HelperUtility.hasPurviewOP("InventoryRecord_update"))
        {
          string strUrl = "edit.aspx" + strUrlBack;
          strUrl += "&page=" + ViewState["page"] + "&id=" + intId.ToString();
          Response.Redirect(strUrl);
        }
        else
          HelperUtility.showAlert("没有操作权限", "list.aspx" + strUrlBack + "&page=" + ViewState["page"]);
      }
      // LoadData();
    }

    public void LoadData(string strNameProduct, string strNameFactory)
    {
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