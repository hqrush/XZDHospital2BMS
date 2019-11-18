using Bll;
using System;

namespace XZDHospital2BMS.BackManager
{
  public partial class login : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
      string strUsername = tbUsername.Value.Trim();
      string strPassword = tbPassword.Value.Trim();
      Boolean boolIsRememberMe = cbRememberMe.Checked;

      int intId = BllAdmin.login(strUsername, strPassword);

      if (intId > 0)
      {
        Session["AdminId"] = intId;
        Response.Redirect("home.aspx");
      }
      else
      {

      }

    }
  }
}