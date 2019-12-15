using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Bll
{

  public class BllDepartment
  {

    public static void bindRPT(Repeater rpt)
    {
      string strFileName = "/BackManager/department/department.txt";
      string strDepartment = HelperFile.ReadTxt(strFileName);
      List<string> listDepartment = strDepartment.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
      rpt.DataSource = listDepartment;
      rpt.DataBind();
    }

  }

}
