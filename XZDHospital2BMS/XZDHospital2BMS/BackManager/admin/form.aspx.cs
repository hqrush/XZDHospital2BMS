using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XZDHospital2BMS.BackManager.admin
{
  public partial class form : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      string strUsername = tbUsername.Value.ToString().Trim();
      string strPassword = tbPassword.Value.ToString().Trim();
      string strRealName = tbRealName.Value.ToString().Trim();
      string strIdCard = tbIdCard.Value.ToString().Trim();
      string strMobilePhone = tbMobilePhone.Value.ToString().Trim();

    }
  }
}