using Bll;
using Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace XZDHospital2BMS.BackManager.admin
{

  public partial class edit : Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        HelperUtility.hasPurviewPage("SysAdmin_update");
        // 本页只能从list.aspx的编辑页转过来
        // 因此要得到要修改的id值和页面的page值用于修改成功后返回
        int intId = HelperUtility.getQueryInt("id");
        ViewState["id"] = intId;
        int intPage = HelperUtility.getQueryInt("page");
        ViewState["page"] = intPage;
        // 更新表单数据
        ModelAdmin model = BllAdmin.getById(intId);
        tbUsername.Value = model.username;
        tbRealName.Value = model.real_name;
        tbMobilePhone.Value = model.mobile_phone;
        setPurviewCheckBox(model.purviews);
      }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
      if (!HelperUtility.hasPurviewOP("SysAdmin_update"))
      {
        string strUrl = "edit.aspx?id=" + ViewState["id"] + "&page=" + ViewState["page"];
        HelperUtility.showAlert("没有操作权限", strUrl);
      }
      int intId = Convert.ToInt32(ViewState["id"]);
      int intPage = Convert.ToInt32(ViewState["page"]);

      string strMsgError = "";
      string strPassword = tbPassword.Value.ToString();
      if (!"".Equals(strPassword))
      {
        if (strPassword.Length < 4 || strPassword.Length > 12)
          strMsgError += "密码长度必须在4 ~ 12之间！\n";
        string strPassword2 = tbPassword2.Value.ToString();
        if ("".Equals(strPassword2)) strMsgError += "确认密码不能为空！\n";
        if (!strPassword.Equals(strPassword2)) strMsgError += "两次输入的密码必须相同！\n";
        strPassword = HelperCrypto.encode(strPassword, "DES");
      }
      string strRealName = tbRealName.Value.ToString().Trim();
      if (strRealName.Length > 6) strMsgError += "真实姓名长度不能大于6个字符！\n";
      string strMobilePhone = tbMobilePhone.Value.ToString().Trim();
      if (!HelperUtility.isMobilePhone(strMobilePhone)) strMsgError += "手机号码格式不正确！\n";

      if (!"".Equals(strMsgError))
        HelperUtility.showAlert(strMsgError, "edit.aspx?id=" + intId + "&page=" + intPage);
      // 开始更新
      ModelAdmin model = BllAdmin.getById(intId);
      if (!"".Equals(strPassword)) model.password = strPassword;
      model.real_name = strRealName;
      model.mobile_phone = strMobilePhone;
      if (!(model.username == "rush" || model.username == "wumin"))
        model.purviews = getSelectedCheckBox();
      BllAdmin.update(model);
      Response.Redirect("/BackManager/admin/list.aspx?page=" + intPage);
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

    private void setPurviewCheckBox(string strPurviews)
    {
      if ("".Equals(strPurviews)) return;
      ControlCollection controls = pnlPurviews.Controls;
      string strCId;
      HtmlInputCheckBox checkBox;
      List<string> listPurviews = new List<string>(strPurviews.Split(','));
      for (int i = 0; i < controls.Count; i++)
      {
        strCId = controls[i].ID;
        if (strCId != null)
        {
          checkBox = (HtmlInputCheckBox)pnlPurviews.FindControl(strCId);
          for (int j = 0; j < listPurviews.Count; j++)
          {
            if (checkBox.Value.Equals(listPurviews[j]))
              checkBox.Checked = true;
          }
        }
      }
    }

  }

}