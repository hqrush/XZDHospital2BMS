using Bll;
using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XZDHospital2BMS.BackManager.inventory_record
{

  public partial class list : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.hasPurviewPage("InventoryRecord_show");
        ViewState["AdminId"] = intAdminId;
        // 本页只能从list.aspx的编辑页转过来
        // 因此要得到要显示哪个出库单的cid值和页面的cpage值用于返回
        int intContractId = HelperUtility.getQueryInt("cid");
        if (intContractId == 0) HelperUtility.showAlert("", "/BackManager/login.aspx");
        ViewState["ContractId"] = intContractId;
        ViewState["ContractPage"] = HelperUtility.getQueryInt("cpage");
        LoadData();
        // 设置其他控件值，以货币形式显示 2.5.ToString("C")
        lblPriceTotal.Text = BllInventoryRecord.getPriceTotal(intContractId).ToString("C");
        hlBackContract.NavigateUrl = "../inventory_contract/list.aspx?page=" + ViewState["ContractPage"];
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

    public void OP_Command(object sender, CommandEventArgs e)
    {
      int intId = Convert.ToInt32(e.CommandArgument);
      string strUrlBack = "?cid=" + ViewState["ContractId"] + "&cpage=" + ViewState["ContractPage"];
      if (e.CommandName == "del")
      {
        if (HelperUtility.hasPurviewOP("InventoryRecord_del"))
          BllCheckoutRecord.deleteById(intId);
        else
          HelperUtility.showAlert("没有操作权限", "list.aspx" + strUrlBack);
      }
      LoadData();
    }

    public void LoadData()
    {
      int intContractId = Convert.ToInt32(ViewState["ContractId"]);
      DataTable objDT = BllInventoryRecord.getAll(intContractId);
      gvShow.DataSource = objDT;
      gvShow.DataBind();
      // 设置其他控件值，以货币形式显示 2.5.ToString("C")
      lblPriceTotal.Text = BllInventoryRecord.getPriceTotal(intContractId).ToString("C");
    }

  }

}