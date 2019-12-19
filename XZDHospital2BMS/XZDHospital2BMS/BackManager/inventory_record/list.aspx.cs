using Bll;
using Helper;
using Model;
using System;
using System.Data;
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
        LoadDataPage();
        hlBackContract.NavigateUrl = "../inventory_contract/list.aspx?page=" + ViewState["ContractPage"];
        // hlAddNew.NavigateUrl = "add.aspx?cid=" + intContractId;
        // 设置显示库存总金额的Label控件值，以货币形式显示 2.5.ToString("C")
        decimal dcmPriceTotalStock = BllInventoryRecord.getPriceTotalStock(intContractId);
        lblPriceTotalStock.Text = dcmPriceTotalStock.ToString("C");

        // 判断是否显示修改数列
        if (Session["Purviews"].ToString().Contains("SUPERADMIN"))
          gvShow.Columns[9].Visible = true;
        else
          gvShow.Columns[9].Visible = false;
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
          BllInventoryRecord.deleteById(intId);
        else
          HelperUtility.showAlert("没有操作权限", "list.aspx" + strUrlBack);
      }
      LoadDataPage();
    }

    protected void gvShow_RowEditing(object sender, GridViewEditEventArgs e)
    {
      gvShow.EditIndex = e.NewEditIndex;
      LoadDataPage();
    }

    protected void gvShow_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      int intId, intGoodsId;
      // string strId = gvShow.DataKeys[e.RowIndex].Values[0].ToString();
      intId = Convert.ToInt32(gvShow.DataKeys[e.RowIndex].Values["id"].ToString());
      Label lblGoodsId = (Label)gvShow.Rows[e.RowIndex].FindControl("lblGoodsId");
      if (lblGoodsId != null) intGoodsId = Convert.ToInt32(lblGoodsId.Text);
      else intGoodsId = 0;
      // 更新盘点数
      TextBox tbInventoryAmountReal = (TextBox)gvShow.Rows[e.RowIndex].FindControl("tbInventoryAmountReal");
      if ("".Equals(tbInventoryAmountReal.Text)) tbInventoryAmountReal.Text = "0";
      decimal dcmInventoryAmountReal = Convert.ToDecimal(tbInventoryAmountReal.Text);
      BllInventoryRecord.updateRealById(dcmInventoryAmountReal, intId);
      // 因为已盘点，因此更新货品的库存量为盘点量
      BllSalesGoods.updateAmountStockByInventory(dcmInventoryAmountReal, intGoodsId);
      // 更新修改数
      TextBox tbInventoryAmountShow = (TextBox)gvShow.Rows[e.RowIndex].FindControl("tbInventoryAmountShow");
      if (tbInventoryAmountShow != null)
      {
        if ("".Equals(tbInventoryAmountShow.Text)) tbInventoryAmountShow.Text = "0";
        decimal dcmInventoryAmountShow = Convert.ToDecimal(tbInventoryAmountShow.Text);
        BllInventoryRecord.updateShowById(dcmInventoryAmountShow, intId);
        // 因为已盘点，因此更新货品的库存量为盘点修改量
        BllSalesGoods.updateAmountStockByInventory(dcmInventoryAmountShow, intGoodsId);
      }
      gvShow.EditIndex = -1;
      LoadDataPage();
    }

    protected void gvShow_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
      gvShow.EditIndex = -1;
      LoadDataPage();
    }

    private int intCurrentPage = 1; //当前页
    private int intPageSize = 20; //每页记录数
    private int intPageCount = 0; //总页数
    private int intRecordCount = 0; //总记录数

    public void LoadDataPage()
    {
      int intContractId = Convert.ToInt32(ViewState["ContractId"]);
      DataTable objDT;
      if ("".Equals(lblCurentPage.Text.Trim())) lblCurentPage.Text = "1";
      intCurrentPage = Convert.ToInt32(lblCurentPage.Text.Trim());
      if (intCurrentPage <= 0) intCurrentPage = 1;
      // 得到总记录数
      intRecordCount = BllInventoryRecord.getRecordsAmount(intContractId);
      // 计算总页数
      intPageCount = (intRecordCount + intPageSize - 1) / intPageSize;
      if (intCurrentPage > intPageCount) intCurrentPage = intPageCount;
      lblPageCount.Text = intPageCount.ToString();
      // 根据当前页获取当前页的分页记录DataTable
      if (intRecordCount > 0)
        objDT = BllInventoryRecord.getPage(intContractId, intCurrentPage, intPageSize);
      else
      {
        lblCurentPage.Text = "1";
        objDT = null;
      }
      if (objDT != null && objDT.Rows.Count > 0)
      {
        lbtnFirst.Enabled = true;
        lbtnPrev.Enabled = true;
        lbtnNext.Enabled = true;
        lbtnLast.Enabled = true;
        if (intCurrentPage == 1)
        {
          lbtnFirst.Enabled = false;
          lbtnPrev.Enabled = false;
        }
        if (intCurrentPage == intPageCount)
        {
          lbtnNext.Enabled = false;
          lbtnLast.Enabled = false;
        }
      }
      else
      {
        lbtnFirst.Enabled = false;
        lbtnPrev.Enabled = false;
        lbtnNext.Enabled = false;
        lbtnLast.Enabled = false;
      }
      gvShow.DataSource = objDT;
      gvShow.DataBind();
      lblRecordCount.Text = intRecordCount.ToString();
      lblCurentPage.Text = intCurrentPage.ToString();
      tbPageNum.Text = intCurrentPage.ToString();
      ViewState["page"] = intCurrentPage;
      // 设置显示盘点总金额的Label控件值，以货币形式显示 2.5.ToString("C")
      decimal[] aryPriceTotalInventory = BllInventoryRecord.getPriceTotalInventory(intContractId);
      decimal dcmPriceTotalReal = aryPriceTotalInventory[0];
      decimal dcmPriceTotalShow = aryPriceTotalInventory[1];
      lblPriceTotalInventory.Text = dcmPriceTotalReal.ToString("C");
    }

    protected void lbtnFirst_Click(object sender, EventArgs e)
    {
      lblCurentPage.Text = "1";
      LoadDataPage();
    }
    protected void lbtnPrev_Click(object sender, EventArgs e)
    {
      lblCurentPage.Text = Convert.ToString(Convert.ToInt32(lblCurentPage.Text) - 1);
      LoadDataPage();
    }
    protected void lbtnNext_Click(object sender, EventArgs e)
    {
      lblCurentPage.Text = Convert.ToString(Convert.ToInt32(lblCurentPage.Text) + 1);
      LoadDataPage();
    }
    protected void lbtnLast_Click(object sender, EventArgs e)
    {
      lblCurentPage.Text = lblPageCount.Text;
      LoadDataPage();
    }
    protected void btnJumpTo_Click(object sender, EventArgs e)
    {
      lblCurentPage.Text = tbPageNum.Text;
      LoadDataPage();
    }

    // 导出Excel文件
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
      int intContractId = Convert.ToInt32(ViewState["ContractId"]);
      string strExcelPath = BllInventoryContract.setExcel(intContractId);
      hlDownloadExcel.NavigateUrl = strExcelPath;
      hlDownloadExcel.Visible = true;
    }

  }

}