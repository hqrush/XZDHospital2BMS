using Bll;
using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace XZDHospital2BMS.BackManager.inventory_contract
{

  public partial class list : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.hasPurviewPage("InventoryContract_show");
        int intCurrentPage = HelperUtility.getQueryInt("page");
        if (intCurrentPage <= 0) intCurrentPage = 1;
        lblCurentPage.Text = intCurrentPage.ToString();
        LoadDataPage();
      }
    }

    protected void gvShow_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#e1f2e9'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        // 将销售公司id转换成名称，将adminid转换成管理员姓名，显示缩略图
        Label lblAdminId = (Label)e.Row.FindControl("lblAdminId");
        Label lblPhotoUrls = (Label)e.Row.FindControl("lblPhotoUrls");
        int intAdminId = Convert.ToInt32(lblAdminId.Text);
        lblAdminId.Text = (BllAdmin.getById(intAdminId)).real_name;
        string strPhotoUrls = lblPhotoUrls.Text;
        List<string> listPhotoUrls = strPhotoUrls.Split(',').ToList();
        HyperLink hl;
        for (int i = 0; i < listPhotoUrls.Count; i++)
        {
          hl = new HyperLink();
          hl.ImageUrl = listPhotoUrls[i];
          hl.ImageWidth = 60;
          hl.ImageHeight = 60;
          hl.NavigateUrl = listPhotoUrls[i];
          hl.Target = "_blank";
          lblPhotoUrls.Parent.Controls.Add(hl);
        }
        lblPhotoUrls.Visible = false;
      }
    }

    public void OP_Command(object sender, CommandEventArgs e)
    {
      int intId = Convert.ToInt32(e.CommandArgument);
      string strUrl = "";
      if (e.CommandName == "edit")
      {
        if (HelperUtility.hasPurviewOP("InventoryContract_update"))
          Response.Redirect("edit.aspx?id=" + intId.ToString() + "&page=" + ViewState["page"]);
        else
          HelperUtility.showAlert("没有操作权限", "list.aspx?page=" + ViewState["page"]);
      }
      else if (e.CommandName == "del")
      {
        if (HelperUtility.hasPurviewOP("InventoryContract_del"))
          BllSalesContract.deleteById(intId);
        else
          HelperUtility.showAlert("没有操作权限", "list.aspx?page=" + ViewState["page"]);
      }
      else if (e.CommandName == "ShowGoodsList")
      {
        if (HelperUtility.hasPurviewOP("InventoryRecord_show"))
        {
          // 跳到添加货品清单页面，传过去合同cid和合同分页的页面值以便添加完成后返回此页
          strUrl = "../inventory_contract/list.aspx?cid=" + intId.ToString() + "&cpage=" + ViewState["page"];
          Response.Redirect(strUrl);
        }
        else
          HelperUtility.showAlert("没有操作权限", "list.aspx?page=" + ViewState["page"]);
      }
      else if (e.CommandName == "AddGoods")
      {
        if (HelperUtility.hasPurviewOP("InventoryRecord_add"))
        {
          // 跳到添加货品清单页面，传过去合同cid和合同分页的页面值以便添加完成后返回此页
          strUrl = "../inventory_record/add.aspx?cid=" + intId.ToString() + "&cpage=" + ViewState["page"];
          Response.Redirect(strUrl);
        }
        else
          HelperUtility.showAlert("没有操作权限", "list.aspx?page=" + ViewState["page"]);
      }
      LoadDataPage();
    }

    private int intCurrentPage = 1; //当前页
    private int intPageSize = 20; //每页记录数
    private int intPageCount = 0; //总页数
    private int intRecordCount = 0; //总记录数

    public void LoadDataPage()
    {
      DataTable objDT;
      if ("".Equals(lblCurentPage.Text.Trim())) lblCurentPage.Text = "1";
      intCurrentPage = Convert.ToInt32(lblCurentPage.Text.Trim());
      if (intCurrentPage <= 0) intCurrentPage = 1;
      // 得到总记录数
      intRecordCount = BllInventoryContract.getRecordsAmount();
      // 计算总页数
      intPageCount = (intRecordCount + intPageSize - 1) / intPageSize;
      if (intCurrentPage > intPageCount) intCurrentPage = intPageCount;
      lblPageCount.Text = intPageCount.ToString();
      // 根据当前页获取当前页的分页记录DataTable
      if (intRecordCount > 0)
        objDT = BllInventoryContract.getPage(intCurrentPage, intPageSize);
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

  }

}