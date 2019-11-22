using Helper;
using System;
using System.Diagnostics;
using System.Web.Services;

namespace XZDHospital2BMS.BackManager
{
  public partial class _home : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        Debug.WriteLine("用Debug.WriteLine输出到控制台测试");
        int intAdminId = HelperUtility.hasPurviewPage("");
        lblAdminId.Text = intAdminId.ToString();
        Response.Write(Session["Purviews"]);
      }
    }


    [WebMethod]
    public static string SayHello()
    {
      return "Hello Ajax!";
    }

  }

}