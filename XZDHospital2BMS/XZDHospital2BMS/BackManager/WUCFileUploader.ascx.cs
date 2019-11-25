using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XZDHospital2BMS.BackManager
{
  public partial class WUCFileUploader : System.Web.UI.UserControl
  {

    private string photoUrls;

    public string PhotoUrls { get => photoUrls; set => photoUrls = value; }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
  }
}