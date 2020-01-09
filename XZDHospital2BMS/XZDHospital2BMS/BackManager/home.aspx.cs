using Bll;
using Helper;
using Model;
using System;

namespace XZDHospital2BMS.BackManager
{
  public partial class _home : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.hasPurviewPage("HOME");
        ModelAdmin model = BllAdmin.getById(intAdminId);
        lblAdminName.Text = model.real_name;
      }
    }

  }

}