using Helper;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XZDHospital2BMS.BackManager
{

  public partial class WUCHeader : System.Web.UI.UserControl
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        string strPurviewsSession = Session["Purviews"].ToString();
        List<string> listPurviewsSession = new List<string>(strPurviewsSession.Split(','));
        if (listPurviewsSession.Contains("SUPERADMIN"))
        {
          ltrSuperAdmin.Visible = true;
          ltrSuperAdmin.Text = "<li><a href='/BackManager/inventory_contract/list2.aspx'>管理盘点单2</a></li>";
        }
        if (
          listPurviewsSession.Contains("SUPERADMIN") ||
          listPurviewsSession.Contains("SysAdmin_add") ||
          listPurviewsSession.Contains("SysAdmin_del") ||
          listPurviewsSession.Contains("SysAdmin_update") ||
          listPurviewsSession.Contains("SysAdmin_show"))
        {
          ltrAdmin.Visible = true;
          ltrAdmin.Text = "<li><a href='/BackManager/admin/list.aspx'>管理管理员</a></li>";
        }

      }
    }

  }

}