using System;
using System.Collections.Generic;

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
        int intAdminId = 0;
        if (Session["AdminId"] != null)
          intAdminId = Convert.ToInt32(Session["AdminId"]);
        else
          Response.Redirect("/BackManager/login.aspx");
        if (intAdminId > 0)
          LoadData();
        else
          Response.Redirect("/BackManager/login.aspx");
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
      gvShow.DataSource = Bll.BllAdmin.getAll();
      gvShow.DataBind();
    }

  }

}