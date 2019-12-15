using Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XZDHospital2BMS.BackManager.department
{

  public partial class list : System.Web.UI.Page
  {

    private string strFileName = "/BackManager/department/department.txt";

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        HelperUtility.hasPurviewPage("Department_add");
        tbDepartmentName.InnerText = HelperFile.ReadTxt(strFileName);
      }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      HelperUtility.hasPurviewPage("Department_add");
      string strContent = tbDepartmentName.InnerText;
      HelperFile.WriteTxt(strContent, strFileName);
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
      string strFileName = "/BackManager/department/department_bak.txt";
      tbDepartmentName.InnerText = HelperFile.ReadTxt(strFileName);
    }

  }

}