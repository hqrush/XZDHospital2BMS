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

    /* 货品出库时
     * 一、出库表里新加一条记录
     * 二、更新某个货品的库存数量字段（amount_stock）（更新方法是amount_stock = amount_stock - amountOut）
     * 添加出库货品时显示的是某个货品的库存量（amount_stock），
     * 货品表里每个货品有一个进货数量（amount）和库存数量（amount_stock）
     * 盘点时
     * 一、新建盘点单，然后搜索所有库存量大于0的货品添加到此盘点单对应的盘点货品单里。
     * 二、盘点时，修改盘点货品单里每个货品的盘点数量和修改（显示用）数量。
     * 三、在修改盘点货品表时，同时将某次盘点货品单里所有货品的库存量（amount_stock）都修改为盘点量（还是修改量？？？）。
     * 显示某盘点单下所有盘点货品时，显示的是该货品的库存数量（amount_stock）字段，盘点货品单下的盘点数量和修改数量。
     */

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.hasPurviewPage("InventoryRecord_show");
        ViewState["AdminId"] = intAdminId;
        // 因此要得到要显示哪个盘点单的cid值和页面的cpage值用于返回
        int intContractId = HelperUtility.getQueryInt("cid");
        if (intContractId == 0) HelperUtility.showAlert("", "/BackManager/login.aspx");
        ViewState["ContractId"] = intContractId;
        ViewState["ContractPage"] = HelperUtility.getQueryInt("cpage");
        LoadDataPage();
        hlBackContract.NavigateUrl = "../inventory_contract/list.aspx?page=" + ViewState["ContractPage"];
        // hlAddNew.NavigateUrl = "add.aspx?cid=" + intContractId;
        // 设置显示库存总金额的Label控件值，以货币形式显示 2.5.ToString("C")
        decimal[] aryPriceTotal = BllInventoryRecord.getPriceTotalInventory(intContractId);
        lblPriceTotalReal.Text = aryPriceTotal[0].ToString("C");
        lblPriceTotalStock.Text = aryPriceTotal[1].ToString("C");
        lblPriceTotalFill.Text = aryPriceTotal[2].ToString("C");
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
      TextBox tbInventoryAmountFill = (TextBox)gvShow.Rows[e.RowIndex].FindControl("tbInventoryAmountFill");
      if (null == tbInventoryAmountFill) return;
      if ("".Equals(tbInventoryAmountFill.Text)) tbInventoryAmountFill.Text = "0";
      decimal dcmInventoryAmountFill = Convert.ToDecimal(tbInventoryAmountFill.Text);
      // 只有盘点数大于0的时候才去更新货品的盘点数
      if (dcmInventoryAmountFill >= 0)
      {
        // 更新盘点表里的盘点数
        BllInventoryRecord.updateFillById(dcmInventoryAmountFill, intId);
        // 同时更新货品的库存量为盘点量
        BllSalesGoods.updateAmountStockByInventory(dcmInventoryAmountFill, intGoodsId);
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

    // 读取所有的数据并分页
    public void LoadDataPage()
    {
      int intContractId = Convert.ToInt32(ViewState["ContractId"]);
      string strProductName = Convert.ToString(ViewState["NameProduct"]);
      if (!"".Equals(strProductName))
      {
        // 如果关键字词不为空，说明是根据关键字词查询得到的数据
        LoadDataQuery(intContractId, strProductName);
      }
      else
      {
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
        decimal[] aryPriceTotal = BllInventoryRecord.getPriceTotalInventory(intContractId);
        lblPriceTotalReal.Text = aryPriceTotal[0].ToString("C");
        lblPriceTotalStock.Text = aryPriceTotal[1].ToString("C");
        lblPriceTotalFill.Text = aryPriceTotal[2].ToString("C");
      }
    }

    // 导出Excel文件
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
      int intContractId = Convert.ToInt32(ViewState["ContractId"]);
      string[] aryExcel = BllInventoryContract.setExcel(intContractId);
      // 设置压缩文件的下载链接
      hlDownloadExcel.NavigateUrl = HelperExcel.SetExcelZip(aryExcel);
      hlDownloadExcel.Visible = true;
    }

    // 点击根据关键字词查询某货品按钮
    protected void btnQuery_Click(object sender, EventArgs e)
    {
      string strUrlBack = "?cid=" + ViewState["ContractId"] + "&cpage=" + ViewState["ContractPage"];
      string strProductName = tbProductName.Value.Trim();
      if ("".Equals(strProductName))
      {
        HelperUtility.showAlert("货品名称不能为空！", "list.aspx" + strUrlBack);
        return;
      }
      ViewState["NameProduct"] = strProductName;
      int intContractId = Convert.ToInt32(ViewState["ContractId"]);
      LoadDataQuery(intContractId, strProductName);
    }

    public void LoadDataQuery(int intContractId, string strProductName)
    {
      DataTable objDT = BllInventoryRecord.getByQuery(intContractId, strProductName);
      gvShow.DataSource = objDT;
      gvShow.DataBind();
      lbtnFirst.Enabled = false;
      lbtnPrev.Enabled = false;
      lbtnNext.Enabled = false;
      lbtnLast.Enabled = false;
      ViewState["page"] = "1";
      lblCurentPage.Text = "1";
      // 设置显示盘点总金额的Label控件值，以货币形式显示 2.5.ToString("C")
      decimal[] aryPriceTotal = BllInventoryRecord.getPriceTotalInventory(intContractId);
      lblPriceTotalReal.Text = aryPriceTotal[0].ToString("C");
      lblPriceTotalStock.Text = aryPriceTotal[1].ToString("C");
      lblPriceTotalFill.Text = aryPriceTotal[2].ToString("C");
    }

    // 点击显示所有数据按钮
    protected void btnShowList_Click(object sender, EventArgs e)
    {
      string strUrlBack = "?cid=" + ViewState["ContractId"] + "&cpage=" + ViewState["ContractPage"];
      Response.Redirect("list.aspx" + strUrlBack);
    }

    protected void btnClearZero_Click(object sender, EventArgs e)
    {
      int intContractId = Convert.ToInt32(ViewState["ContractId"]);
      BllInventoryRecord.clearZero(intContractId);
      string strUrlBack = "?cid=" + ViewState["ContractId"] + "&cpage=" + ViewState["ContractPage"];
      Response.Redirect("list.aspx" + strUrlBack);
    }

  }
}