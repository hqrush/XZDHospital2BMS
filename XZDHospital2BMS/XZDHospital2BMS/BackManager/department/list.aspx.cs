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

    protected void Page_Load(object sender, EventArgs e)
    {
      tbDepartmentName.InnerText = ReadData("department.txt");
    }

    public string ReadData(string strFileName)
    {
      Encoding objEncoding = Encoding.GetEncoding("utf-8");
      string strFilePath = Server.MapPath(strFileName);
      FileStream objFS = new FileStream(strFilePath, FileMode.Open, FileAccess.Read);
      StreamReader objSR = new StreamReader(objFS, objEncoding);
      objSR.BaseStream.Seek(0, SeekOrigin.Begin);
      string strReturn = objSR.ReadToEnd();
      objSR.Close();
      objFS.Close();
      return strReturn;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }

  }

}