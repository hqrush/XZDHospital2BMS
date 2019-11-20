using Helper;
using System;
using System.Web.Services;

namespace XZDHospital2BMS.BackManager
{
  public partial class _home : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.checkPurview("");
        lblAdminId.Text = intAdminId.ToString();
      }
    }


    [WebMethod]
    public static string SayHello()
    {
      return "Hello Ajax!";
    }

  }

}