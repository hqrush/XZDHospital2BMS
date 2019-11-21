using Bll;
using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XZDHospital2BMS.BackManager.admin
{
  public partial class list : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.checkPurview("SysAdmin_del,SysAdmin_show");
        LoadDataAll();
      }
    }

    protected void gvShow_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        // e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#F0F0F0'");
        // e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        // int intArticleID = Convert.ToInt32(((Label)e.Row.FindControl("lblArticleID")).Text);
      }
    }

    public void OP_Command(object sender, CommandEventArgs e)
    {
      int intId = Convert.ToInt32(e.CommandArgument);
      if (e.CommandName == "Edit")
        Response.Redirect("edit.aspx?id=" + intId.ToString());
      else if (e.CommandName == "Del")
      {
        BllAdmin.deleteById(intId);
        LoadDataPage();
      }
    }

    public void LoadDataAll()
    {
      gvShow.DataSource = Bll.BllAdmin.getAll();
      gvShow.DataBind();
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
      intCurrentPage = Convert.ToInt32(lblCurentPage.Text.Trim());
      // 得到总记录数
      intRecordCount = BllAdmin.getRecordsAmount();
      // 计算总页数
      intPageCount = (intRecordCount + intPageSize - 1) / intPageSize;
      lblPageCount.Text = intPageCount.ToString();
      // 根据当前页获取当前页的分页记录DataTable
      if (intRecordCount > 0)
      {
        objDT = BllAdmin.getPage(intCurrentPage, intPageSize);
      }
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

  }

}