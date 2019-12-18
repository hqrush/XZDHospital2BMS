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
      // string strId = gvShow.DataKeys[e.RowIndex].Values[0].ToString();
      string strId = gvShow.DataKeys[e.RowIndex].Values["id"].ToString();
      TextBox tbInventoryAmount = (TextBox)gvShow.Rows[e.RowIndex].FindControl("tbInventoryAmount");
      if ("".Equals(tbInventoryAmount.Text)) tbInventoryAmount.Text = "0";
      decimal dcmInventoryAmount = Convert.ToDecimal(tbInventoryAmount.Text);
      BllInventoryRecord.updateRealById(dcmInventoryAmount, Convert.ToInt32(strId));
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
      ModelInventoryContract model = BllInventoryContract.getById(intContractId);
      string strFileName = "Inventory[" +
        model.time_start.ToString("yyMMdd") +
        "-" +
        model.time_end.ToString("yyMMdd") +
        "].xlsx";
      DataTable objDT = BllInventoryRecord.getAll(intContractId);
      objDT.TableName = "TableInventory";
      DataRow objDR;
      objDT.Columns.Add(new DataColumn("id_row", typeof(int)));
      objDT.Columns.Add(new DataColumn("temp1", typeof(string)));
      objDT.Columns.Add(new DataColumn("temp2", typeof(string)));
      for (int i = 0; i < objDT.Rows.Count; i++)
      {
        objDR = objDT.Rows[i];
        objDR["id_row"] = i + 1;
        objDR["temp1"] = Convert.ToDateTime(objDR["time_sign"]).ToString("yyyy-MM-dd");
        objDR["temp2"] = Convert.ToDateTime(objDR["validity_period"]).ToString("yyyy-MM-dd");
      }
      objDT.Columns.Remove(objDT.Columns["time_sign"]);
      objDT.Columns.Remove(objDT.Columns["validity_period"]);
      objDT.Columns["temp1"].ColumnName = "time_sign";
      objDT.Columns["temp2"].ColumnName = "validity_period";
      string strExcelTemplateFileName = "/Excel/Template/Inventory.xlsx";
      string strExcelOutFileName = "/Excel/Export/" + strFileName;
      HelperExcel.ExportExcel(objDT, strExcelTemplateFileName, strExcelOutFileName);
    }

  }

}