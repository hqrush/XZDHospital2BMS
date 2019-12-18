using Bll;
using Helper;
using System;
using System.Data;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace XZDHospital2BMS.BackManager.checkout_record
{

  public partial class list : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.hasPurviewPage("CheckoutRecord_show");
        ViewState["AdminId"] = intAdminId;
        // 本页只能从出库单表list.aspx的编辑页转过来
        // 因此要得到要显示哪个出库单的cid值和页面的cpage值用于返回
        int intContractId = HelperUtility.getQueryInt("cid");
        if (intContractId == 0) HelperUtility.showAlert("", "/BackManager/login.aspx");
        ViewState["ContractId"] = intContractId;
        ViewState["ContractPage"] = HelperUtility.getQueryInt("cpage");
        LoadData();
        // 设置其他控件值，以货币形式显示 2.5.ToString("C")
        lblPriceTotal.Text = BllCheckoutRecord.getPriceTotal(intContractId).ToString("C");
        hlBackContract.NavigateUrl = "../checkout_contract/list.aspx?page=" + ViewState["ContractPage"];
        hlAddNew.NavigateUrl = "add.aspx?cid=" + intContractId;
      }
    }

    protected void gvShow_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#e1f2e9'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        Label lblGoodsId = (Label)e.Row.FindControl("lblGoodsId");
        HyperLink hlProductName = (HyperLink)e.Row.FindControl("hlProductName");
        hlProductName.NavigateUrl = "../sales_goods/show.aspx?id=" + lblGoodsId.Text;
      }
    }

    protected void gvShow_RowEditing(object sender, GridViewEditEventArgs e)
    {
      gvShow.EditIndex = e.NewEditIndex;
      LoadData();
    }

    protected void gvShow_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      string strMsg = "";
      string strId = gvShow.DataKeys[e.RowIndex].Values[0].ToString();
      Debug.WriteLine(strId);
      TextBox tbAmount = (TextBox)gvShow.Rows[e.RowIndex].FindControl("tbAmount");

      if ("".Equals(tbAmount.Text)) strMsg += "数量不能为空！";
      if (!HelperUtility.isDecimal(tbAmount.Text)) strMsg += "请输入正确的数字！";
      decimal dcmAmount = Convert.ToDecimal(tbAmount.Text);
      if (dcmAmount == 0) strMsg += "数量必须大于0！";
      if (!"".Equals(strMsg))
      {
        HelperUtility.showAlert(strMsg, "");
        return;
      }
      else
      {
        // BllCheckoutRecord.updateAmountById(dcmAmount, Convert.ToInt32(strId));
      }
      gvShow.EditIndex = -1;
      LoadData();
    }

    protected void gvShow_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
      gvShow.EditIndex = -1;
      LoadData();
    }

    public void OP_Command(object sender, CommandEventArgs e)
    {
      int intId = Convert.ToInt32(e.CommandArgument);
      string strUrlBack = "?cid=" + ViewState["ContractId"] + "&cpage=" + ViewState["ContractPage"];
      if (e.CommandName == "del")
      {
        if (HelperUtility.hasPurviewOP("CheckoutRecord_del"))
          BllCheckoutRecord.deleteById(intId);
        else
          HelperUtility.showAlert("没有操作权限", "list.aspx" + strUrlBack);
      }
      LoadData();
    }

    public void LoadData()
    {
      int intContractId = Convert.ToInt32(ViewState["ContractId"]);
      DataTable objDT = BllCheckoutRecord.getAll(intContractId);
      gvShow.DataSource = objDT;
      gvShow.DataBind();
      // 设置其他控件值，以货币形式显示 2.5.ToString("C")
      lblPriceTotal.Text = BllCheckoutRecord.getPriceTotal(intContractId).ToString("C");
    }

  }

}