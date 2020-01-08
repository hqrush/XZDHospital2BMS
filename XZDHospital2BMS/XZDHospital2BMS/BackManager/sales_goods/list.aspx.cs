using Bll;
using Helper;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace XZDHospital2BMS.BackManager.sales_goods
{

  public partial class list : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.hasPurviewPage("SalesGoods_show");
        ViewState["AdminId"] = intAdminId;
        // 本页只能从list.aspx的编辑页转过来
        // 因此要得到要显示哪个入库单的cid值和页面的cpage值用于返回
        int intContractId = HelperUtility.getQueryInt("cid");
        if (intContractId == 0) HelperUtility.showAlert("", "/BackManager/login.aspx");
        ViewState["ContractId"] = intContractId;
        ViewState["ContractPage"] = HelperUtility.getQueryInt("cpage");
        // 得到现在的页面值
        int intCurrentPage = HelperUtility.getQueryInt("page");
        if (intCurrentPage <= 0) intCurrentPage = 1;
        lblCurentPage.Text = intCurrentPage.ToString();
        LoadDataPage();
        // 设置其他控件值，以货币形式显示 2.5.ToString("C")
        lblPriceTotal.Text = BllSalesGoods.getPriceTotal(intContractId).ToString("C");
        hlBackContract.NavigateUrl = "../sales_contract/list.aspx?page=" + ViewState["ContractPage"];
        hlAddNew.NavigateUrl = "add.aspx?cid=" + intContractId;
      }
    }

    protected void gvShow_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#e1f2e9'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");

        Label lblAdminId = (Label)e.Row.FindControl("lblAdminId");
        int intAdminId = Convert.ToInt32(lblAdminId.Text);
        lblAdminId.Text = (BllAdmin.getById(intAdminId)).real_name;
        Label lblId = (Label)e.Row.FindControl("lblId");
        HyperLink hlShow = (HyperLink)e.Row.FindControl("hlShow");
        hlShow.NavigateUrl = "show.aspx?id=" + lblId.Text;
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
      else if (e.CommandName == "del")
      {
        if (HelperUtility.hasPurviewOP("SalesGoods_del"))
          BllSalesGoods.deleteById(intId);
        else
          HelperUtility.showAlert("没有操作权限", "list.aspx" + strUrlBack + "&page=" + ViewState["page"]);
      }
      LoadDataPage();
    }

    private int intCurrentPage = 1; //当前页
    private int intPageSize = 20; //每页记录数
    private int intPageCount = 0; //总页数
    private int intRecordCount = 0; //总记录数

    public void LoadDataPage()
    {
      int intContractId = (int)ViewState["ContractId"];
      DataTable objDT;
      if ("".Equals(lblCurentPage.Text.Trim())) lblCurentPage.Text = "1";
      intCurrentPage = Convert.ToInt32(lblCurentPage.Text.Trim());
      if (intCurrentPage <= 0) intCurrentPage = 1;
      // 得到总记录数
      intRecordCount = BllSalesGoods.getRecordsAmount(intContractId);
      // 计算总页数
      intPageCount = (intRecordCount + intPageSize - 1) / intPageSize;
      if (intCurrentPage > intPageCount) intCurrentPage = intPageCount;
      lblPageCount.Text = intPageCount.ToString();
      // 根据当前页获取当前页的分页记录DataTable
      if (intRecordCount > 0)
        objDT = BllSalesGoods.getPage(intContractId, intCurrentPage, intPageSize);
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

    // 导出入库单货品清单以便打印的Excel文件
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
      int intContractId = Convert.ToInt32(ViewState["ContractId"]);
      string[] aryExcel = BllSalesContract.setExcel(intContractId);
      // 设置压缩文件的下载链接
      hlDownloadExcel.NavigateUrl = HelperExcel.SetExcelZip(aryExcel);
      hlDownloadExcel.Visible = true;
    }

  }

}