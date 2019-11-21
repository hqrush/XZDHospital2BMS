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

    private int intAdminId;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        // 判断权限及得到登录的AdminId
        intAdminId = HelperUtility.checkPurview("SysAdmin_edit");
        ViewState["AdminId"] = intAdminId;

        // 得到要修改的id值
        if (Request.QueryString["id"] == null ||
          Request.QueryString["id"].ToString() == "" ||
          Convert.ToInt32(Request.QueryString["id"]) <= 0
          ) Response.Redirect("/BackManager/login.aspx");
        int intId = Convert.ToInt32(Request.QueryString["id"]);
        ViewState["id"] = intId;

        // 更新表单数据
        ModelAdmin model = BllAdmin.getById(intId);
        tbUsername.Value = model.username;
        tbRealName.Value = model.real_name;
        tbIdCard.Value = model.id_card;
        tbMobilePhone.Value = model.mobile_phone;
        setPurviewCheckBox(model.purviews);
      }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      if (ViewState["AdminId"] == null || (int)ViewState["AdminId"] <= 0)
        Response.Redirect("/BackManager/login.aspx");
      intAdminId = (int)ViewState["AdminId"];

      if (ViewState["id"] == null || (int)ViewState["id"] <= 0)
        Response.Redirect("/BackManager/login.aspx");
      int intId = (int)ViewState["id"];

      string strPassword = tbPassword.Value.ToString().Trim();
      strPassword = HelperCrypto.encode(strPassword, "DES");
      string strRealName = tbRealName.Value.ToString().Trim();
      string strIdCard = tbIdCard.Value.ToString().Trim();
      string strMobilePhone = tbMobilePhone.Value.ToString().Trim();

      ModelAdmin model = BllAdmin.getById(intId);
      model.real_name = strRealName;
      model.password = strPassword;
      model.id_card = strIdCard;
      model.mobile_phone = strMobilePhone;
      model.purviews = getSelectedCheckBox();
      if (BllAdmin.update(model) > 0)
        Response.Redirect("/BackManager/admin/list.aspx");
      else
      {
        string strOPMsg = "<script>";
        strOPMsg += "alert('修改失败！');location='edit.aspx?id=" + intId + "';";
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