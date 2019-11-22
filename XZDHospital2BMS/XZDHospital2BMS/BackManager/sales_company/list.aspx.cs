using Bll;
using Helper;
using Model;
using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

namespace XZDHospital2BMS.BackManager.sales_company
{
  public partial class list : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.hasPurviewPage("SalesCompany_show");
        ViewState["AdminId"] = intAdminId;
        int intCurrentPage = HelperUtility.getQueryInt("page");
        if (intCurrentPage <= 0) intCurrentPage = 1;
        lblCurentPage.Text = intCurrentPage.ToString();
        LoadDataPage();
      }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
      if (HelperUtility.hasPurviewOP("SalesCompany_add"))
      {
        ModelSalesCompany model;
        StringBuilder objSB = new StringBuilder();
        if ("".Equals(tbCompanyName.Value.Trim())) return;
        objSB.Append(tbCompanyName.Value.Trim());
        // 获取文本框文本根据换行符分割成字符串数组
        string[] aryRows = objSB.ToString().Split(Environment.NewLine.ToCharArray());
        aryRows = HelperUtility.removeArrayBlankRow(aryRows);
        if (aryRows.Length <= 0) return;
        for (int i = 0; i < aryRows.Length; i++)
        {
          model = new ModelSalesCompany();
          model.name = aryRows[i];
          model.id_admin = (int)ViewState["AdminId"];
          BllSalesCompany.add(model);
        }
        HelperUtility.showAlert("添加成功！", "list.aspx");
      }
      else
      {
        string strUrl = "list.aspx?page=" + ViewState["page"];
        HelperUtility.showAlert("没有添加权限", strUrl);
      }
    }

    protected void gvShow_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#e1f2e9'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        Label lblAdminInfo = ((Label)e.Row.FindControl("lblAdminId"));
        int intAdminId = Convert.ToInt32(lblAdminInfo.Text);
        ModelAdmin admin = BllAdmin.getById(intAdminId);
        lblAdminInfo.Text = admin.real_name;
      }
    }

    public void OP_Command(object sender, CommandEventArgs e)
    {
      int intId = Convert.ToInt32(e.CommandArgument);
      if (e.CommandName == "Delete")
      {
        if (HelperUtility.hasPurviewOP("SalesCompany_del"))
        {
          BllSalesCompany.deleteById(intId);
        }
        else
        {
          string strUrl = "list.aspx?page=" + ViewState["page"];
          HelperUtility.showAlert("没有删除权限", strUrl);
        }
      }
      LoadDataPage();
    }

    protected void gvShow_RowEditing(object sender, GridViewEditEventArgs e)
    {
      if (HelperUtility.hasPurviewOP("SalesCompany_update"))
      {
        gvShow.EditIndex = e.NewEditIndex;
        LoadDataPage();
      }
      else
      {
        string strUrl = "list.aspx?page=" + ViewState["page"];
        HelperUtility.showAlert("没有编辑权限", strUrl);
      }
    }

    protected void gvShow_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      string strName = ((TextBox)gvShow.Rows[e.RowIndex].Cells[0].Controls[1]).Text;
      strName = HelperUtility.removeStringSpace(strName.Trim());
      if (!"".Equals(strName))
      {
        int intId = Convert.ToInt32(gvShow.DataKeys[e.RowIndex].Value);
        ModelSalesCompany model = BllSalesCompany.getById(intId);
        model.name = strName;
        BllSalesCompany.update(model);
        gvShow.EditIndex = -1;
        LoadDataPage();
      }
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
      DataTable objDT;
      // “/”相当于整数除法中的除号，“%”相当于余号
      // 5 / 2 = 2，2/2=1,1/2=0
      // 5 % 2 = 1
      if ("".Equals(lblCurentPage.Text.Trim())) lblCurentPage.Text = "1";
      intCurrentPage = Convert.ToInt32(lblCurentPage.Text.Trim());
      if (intCurrentPage <= 0) intCurrentPage = 1;
      // 得到总记录数
      intRecordCount = BllSalesCompany.getRecordsAmount();
      // 计算总页数
      intPageCount = (intRecordCount + intPageSize - 1) / intPageSize;
      if (intCurrentPage > intPageCount) intCurrentPage = intPageCount;
      lblPageCount.Text = intPageCount.ToString();
      // 根据当前页获取当前页的分页记录DataTable
      if (intRecordCount > 0)
        objDT = BllSalesCompany.getPage(intCurrentPage, intPageSize);
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