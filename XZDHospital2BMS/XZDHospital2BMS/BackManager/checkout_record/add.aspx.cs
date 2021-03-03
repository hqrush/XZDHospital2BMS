using Bll;
using Helper;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace XZDHospital2BMS.BackManager.checkout_record
{

  public partial class add : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.hasPurviewPage("CheckoutRecord_add");
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
        hlProductName.NavigateUrl = "show.aspx?id=" + intGoodsId;
        // 记录进货总量的Label控件
        Label lblAmountIn = (Label)e.Row.FindControl("lblAmountIn");
        // 记录实时库存总量的Label控件
        Label lblInventory = (Label)e.Row.FindControl("lblInventory");
        // 记录盘后库存总量的Label控件
        Label lblAmountStock = (Label)e.Row.FindControl("lblAmountStock");
        // 得到某个货品的出库总量
        decimal dcmOut = BllCheckoutRecord.getAmountByGoodsId(intGoodsId);
        // 得到入库的货品总量
        if ("".Equals(lblAmountIn.Text)) lblAmountIn.Text = "0";
        decimal dcmIn = Convert.ToDecimal(lblAmountIn.Text);
        // 计算实时库存量
        decimal dcmInventory = dcmIn - dcmOut;
        lblInventory.Text = dcmInventory.ToString("N");
        // 根据出库单id，货品id，出库数量添加一条出库货品记录
        // 先得到输入出库数量的textbox控件id
        TextBox tbCheckoutAmount = (TextBox)e.Row.FindControl("tbCheckoutAmount");
        // 得到添加到出库单的按钮控件
        HtmlInputButton btnAddToList = (HtmlInputButton)e.Row.FindControl("btnAddToList");
        int intCheckoutContractId = Convert.ToInt32(ViewState["ContractId"]);
        if ("".Equals(tbCheckoutAmount.Text)) tbCheckoutAmount.Text = "0";
        string strClickHandler = "addGoods(" +
          intCheckoutContractId + "," +
          intGoodsId + ",\"" +
          lblInventory.ClientID + "\",\"" +
          lblAmountStock.ClientID + "\",\"" +
          tbCheckoutAmount.ClientID + "\")";
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
        if (HelperUtility.hasPurviewOP("SalesGoods_update"))
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