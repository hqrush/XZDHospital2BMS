using Bll;
using Helper;
using System;

namespace XZDHospital2BMS.BackManager
{

  public partial class login : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        // 首先从cookie中读取保存的用户名密码
        string strCKName = "AdminLogin";
        string strUsername = HelperCookie.getCookie(strCKName, "username");
        string strPassword = HelperCookie.getCookie(strCKName, "password");
        if (!"".Equals(strUsername) && !"".Equals(strPassword))
        {
          int intAdminID = BllAdmin.login(strUsername, strPassword);
          if (intAdminID > 0)
          {
            Session["AdminID"] = intAdminID;
            Response.Redirect("home.aspx");
          }
        }
        // 到了这里，说明cookie中没有保存账号密码，或者保存的账号密码不能登录
        tbUsername.Value = "";
        tbPassword.Value = "";
        cbRememberMe.Checked = false;
      }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
      string strUsername = tbUsername.Value.ToString().Trim();
      string strPassword = tbPassword.Value.ToString().Trim();
      int intId = BllAdmin.login(strUsername, strPassword);
      if (intId > 0)
      {
        // 用户名密码验证正确，保存到cookie里
        string strCKName = "AdminLogin";
        if (cbRememberMe.Checked)
        {
          string strCKKey = "username";
          string strCKValue = strUsername;
          HelperCookie.setCookie(strCKName, strCKKey, strCKValue, 7 * 24 * 60);
          strCKKey = "password";
          strCKValue = strPassword;
          HelperCookie.setCookie(strCKName, strCKKey, strCKValue, 7 * 24 * 60);
        }
        else
        {
          // 如果没有勾选记住我复选框，就要清除cookie里的登录数据
          // 将过期时间设置为-1即可清除保存的值
          HelperCookie.removeCookie(strCKName);
        }
        // 保存到Session里
        Session["AdminId"] = intId;
        Response.Redirect("home.aspx");
      }
      else
      {
        string strOPMsg = "<script>";
        strOPMsg += "alert('登录失败！请检查您输入的账号和密码！');location='login.aspx';";
        strOPMsg += "</script>";
        Response.Write(strOPMsg);
      }
    }

  }

}
