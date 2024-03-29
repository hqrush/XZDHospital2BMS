﻿using Bll;
using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace XZDHospital2BMS.BackManager.checkout_contract
{

  public partial class list : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.hasPurviewPage("CheckoutContract_show");
        int intCurrentPage = HelperUtility.getQueryInt("page");
        if (intCurrentPage <= 0) intCurrentPage = 1;
        lblCurentPage.Text = intCurrentPage.ToString();
        // 读取数据
        LoadDataPage();
        // 判断是否显示第一列
        if (Session["Purviews"].ToString().Contains("SUPERADMIN"))
          gvShow.Columns[0].Visible = true;
        else
          gvShow.Columns[0].Visible = false;
      }
    }

    protected void gvShow_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#e1f2e9'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        // 设置标志
        Label lblFlag = (Label)e.Row.FindControl("lblFlag");
        if (lblFlag != null)
        {
          if (lblFlag.Text == "True") lblFlag.Text = "<span class='red'>*</span>";
          else lblFlag.Text = "";
        }
        // 设置申请单位，有两个单位，所以要做下显示处理
        Label lblUnitName = (Label)e.Row.FindControl("lblUnitName");
        List<string> listUnitName = lblUnitName.Text.Split(',').ToList();
        lblUnitName.Text = "";
        if (!"".Equals(listUnitName[0]) && "".Equals(listUnitName[1]))
          lblUnitName.Text = listUnitName[0];
        if (!"".Equals(listUnitName[0]) && !"".Equals(listUnitName[1]))
          lblUnitName.Text = listUnitName[0] + "<br />" + listUnitName[1];
        if ("".Equals(listUnitName[0]) && !"".Equals(listUnitName[1]))
          lblUnitName.Text = listUnitName[1];
        // 根据管理员Id显示管理员姓名
        Label lblAdminId = (Label)e.Row.FindControl("lblAdminId");
        int intAdminId = Convert.ToInt32(lblAdminId.Text);
        lblAdminId.Text = BllAdmin.getRealNameById(intAdminId);
        // 根据出库单id显示此出库单下所有货品总数
        Label lblId = (Label)e.Row.FindControl("lblId");
        Label lblAmount = (Label)e.Row.FindControl("lblAmount");
        int intContractId = Convert.ToInt32(lblId.Text);
        lblAmount.Text = BllCheckoutRecord.getRecordsAmount(intContractId).ToString();
      }
    }

    public void OP_Command(object sender, CommandEventArgs e)
    {
      int intId = Convert.ToInt32(e.CommandArgument);
      string strUrl = "";
      if (e.CommandName == "edit")
      {
        if (HelperUtility.hasPurviewOP("CheckoutContract_update"))
          Response.Redirect("edit.aspx?id=" + intId.ToString() + "&page=" + ViewState["page"]);
        else
          HelperUtility.showAlert("没有操作权限", "list.aspx?page=" + ViewState["page"]);
      }
      else if (e.CommandName == "del")
      {
        if (HelperUtility.hasPurviewOP("CheckoutContract_del"))
          BllCheckoutContract.deleteById(intId);
        else
          HelperUtility.showAlert("没有操作权限", "list.aspx?page=" + ViewState["page"]);
      }
      else if (e.CommandName == "ShowGoodsList")
      {
        if (HelperUtility.hasPurviewOP("CheckoutGoods_show"))
        {
          // 跳到添加货品清单页面，传过去合同cid和合同分页的页面值以便添加完成后返回此页
          strUrl = "../checkout_record/list.aspx?cid=" + intId.ToString() + "&cpage=" + ViewState["page"];
          Response.Redirect(strUrl);
        }
        else
          HelperUtility.showAlert("没有操作权限", "list.aspx?page=" + ViewState["page"]);
      }
      else if (e.CommandName == "AddGoods")
      {
        if (HelperUtility.hasPurviewOP("CheckoutGoods_add"))
        {
          // 跳到添加货品清单页面，传过去合同cid和合同分页的页面值以便添加完成后返回此页
          strUrl = "../checkout_record/add.aspx?cid=" + intId.ToString() + "&cpage=" + ViewState["page"];
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
      intRecordCount = BllCheckoutContract.getRecordsAmount();
      // 计算总页数
      intPageCount = (intRecordCount + intPageSize - 1) / intPageSize;
      if (intCurrentPage > intPageCount) intCurrentPage = intPageCount;
      lblPageCount.Text = intPageCount.ToString();
      // 根据当前页获取当前页的分页记录DataTable
      if (intRecordCount > 0)
        objDT = BllCheckoutContract.getPage(intCurrentPage, intPageSize);
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