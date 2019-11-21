using Bll;
using Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace XZDHospital2BMS.BackManager.admin
{

  public partial class add : Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.checkPurview("SysAdmin_add");
        Response.Write(intAdminId.ToString());
      }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      return;
      string strUsername = tbUsername.Value.ToString().Trim();
      if (BllAdmin.hasUsername(strUsername))
      {
        string strOPMsg = "<script>";
        strOPMsg += "alert('用户名已存在，请取过！');location='add.aspx';";
        strOPMsg += "</script>";
        Response.Write(strOPMsg);
      }
      string strPassword = tbPassword.Value.ToString().Trim();
      strPassword = HelperCrypto.encode(strPassword, "DES");
      string strRealName = tbRealName.Value.ToString().Trim();
      string strMobilePhone = tbMobilePhone.Value.ToString().Trim();

      ModelAdmin model = new ModelAdmin();
      model.username = strUsername;
      model.password = strPassword;
      model.real_name = strRealName;
      model.mobile_phone = strMobilePhone;
      model.purviews = getSelectedCheckBox();
      int intId = BllAdmin.add(model);
      if (intId > 0)
      {
        string strOPMsg = "<script>";
        strOPMsg += "alert('添加成功！');location='list.aspx';";
        strOPMsg += "</script>";
        Response.Write(strOPMsg);
      }
      else
      {
        string strOPMsg = "<script>";
        strOPMsg += "alert('添加失败！');location='add.aspx';";
        strOPMsg += "</script>";
        Response.Write(strOPMsg);
      }
    }

    private string getSelectedCheckBox()
    {
      ControlCollection controls = pnlPurviews.Controls;
      string strCId;
      HtmlInputCheckBox checkBox;
      List<string> listPurviews = new List<string>();
      for (int i = 0; i < controls.Count; i++)
      {
        strCId = controls[i].ID;
        if (strCId != null)
        {
          if (strCId.EndsWith("Add") ||
            strCId.EndsWith("Del") ||
            strCId.EndsWith("Update") ||
            strCId.EndsWith("Show"))
          {
            checkBox = (HtmlInputCheckBox)pnlPurviews.FindControl(strCId);
            if (checkBox.Checked)
              listPurviews.Add(checkBox.Value);
          }
        }
      }
      if (listPurviews != null && listPurviews.Count > 0)
      {
        string strPurviews = "";
        for (int i = 0; i < listPurviews.Count; i++)
        {
          strPurviews += listPurviews[i];
          strPurviews += ",";
        }
        return strPurviews.Substring(0, strPurviews.Length - 1);
      }
      else
        return "";
    }

  }

}