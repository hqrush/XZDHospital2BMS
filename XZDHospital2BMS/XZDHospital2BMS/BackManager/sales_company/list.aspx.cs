using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XZDHospital2BMS.BackManager.sales_company
{
  public partial class list : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        LoadData();
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

    protected void gvShow_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gvShow.PageIndex = e.NewPageIndex;
      // LoadDataByName((string)ViewState["Name"], (string)ViewState["DateStart"], (string)ViewState["DateEnd"]);
    }

    protected void gvShow_RowEditing(object sender, GridViewEditEventArgs e)
    {
      gvShow.EditIndex = e.NewEditIndex;
      // LoadDataByName((string)ViewState["Name"], (string)ViewState["DateStart"], (string)ViewState["DateEnd"]);
    }

    protected void gvShow_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void gvShow_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
    }

    protected void gvShow_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
      gvShow.EditIndex = -1;
      // LoadDataByName((string)ViewState["Name"], (string)ViewState["DateStart"], (string)ViewState["DateEnd"]);
    }

    public void LoadData()
    {
      DataTable objDT = Bll.BllSalesCompany.getDataTableAll();
      if (objDT != null)
      {
        divAlert.Visible = false;
        gvShow.DataSource = objDT;
        gvShow.DataBind();
      }
      else
      {
        divAlert.Visible = true;
      }
    }

  }

}