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
        HelperUtility.hasPurviewPage("SysAdmin_add");
      }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
      if (!HelperUtility.hasPurviewOP("SysAdmin_add"))
      {
        string strUrl = "/BackManager/home.aspx";
        HelperUtility.showAlert("没有操作权限", strUrl);
      }
      string strMsgError = "";
      string strUsername = tbUsername.Value.ToString().Trim();
      if ("".Equals(strUsername)) strMsgError += "用户名不能为空！\n";
      if (strUsername.Length < 4 || strUsername.Length > 12)
        strMsgError += "用户名长度必须在4 ~ 12之间！\n";
      if (BllAdmin.hasUsername(strUsername)) strMsgError += "用户名已存在，请取过！\n";
      string strPassword = tbPassword.Value.ToString();
      if ("".Equals(strPassword)) strMsgError += "密码不能为空！\n";
      if (strPassword.Length < 4 || strPassword.Length > 12)
        strMsgError += "密码长度必须在4 ~ 12之间！\n";
      string strPassword2 = tbPassword2.Value.ToString();
      if ("".Equals(strPassword2)) strMsgError += "确认密码不能为空！\n";
      if (!strPassword.Equals(strPassword2)) strMsgError += "两次输入的密码必须相同！\n";
      strPassword = HelperCrypto.encode(strPassword, "DES");
      string strRealName = tbRealName.Value.ToString().Trim();
      if (strRealName.Length > 6) strMsgError += "真实姓名长度不能大于6个字符！\n";
      string strMobilePhone = tbMobilePhone.Value.ToString().Trim();
      if (!HelperUtility.isMobilePhone(strMobilePhone)) strMsgError += "手机号码格式不正确！\n";
      if (!"".Equals(strMsgError)) HelperUtility.showAlert(strMsgError, "add.aspx");
      // 验证完毕，提交数据
      ModelAdmin model = new ModelAdmin();
      model.username = strUsername;
      model.password = strPassword;
      model.real_name = strRealName;
      model.mobile_phone = strMobilePhone;
      model.purviews = getSelectedCheckBox();
      int intId = BllAdmin.add(model);
      if (intId > 0)
      {
        string strUrl = "list.aspx";
        HelperUtility.showAlert("添加成功！", strUrl);
      }
      else
      {
        string strUrl = "add.aspx";
        HelperUtility.showAlert("添加失败，请联系管理员！", strUrl);
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
      if (listPurviews == null || listPurviews.Count <= 0) return "";
      string strPurviews = "";
      for (int i = 0; i < listPurviews.Count; i++)
      {
        strPurviews += listPurviews[i];
        strPurviews += ",";
      }
      return strPurviews.Substring(0, strPurviews.Length - 1);
    }

  }

}