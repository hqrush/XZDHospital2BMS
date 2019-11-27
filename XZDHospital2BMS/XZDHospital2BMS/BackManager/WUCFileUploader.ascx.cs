using Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XZDHospital2BMS.BackManager
{
  public partial class WUCFileUploader : System.Web.UI.UserControl
  {

    private string photoUrls = "";

    public string PhotoUrls { get => photoUrls; set => photoUrls = value; }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnDelPhoto_Click(object sender, EventArgs e)
    {
      //string strRootPath = MapPath("/").Replace("//", "/");
      //string strImgPath = strRootPath + PhotoUrls.ImageUrl;
      //HelperFile.DeleteFile(strImgPath);
      //fuPhoto.Visible = true;
      //btnUploadPhoto.Visible = true;
      //imgPhoto.Visible = false;
      //btnDelPhoto.Visible = false;
    }

  }
}