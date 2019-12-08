using Bll;
using Helper;
using Model;
using System;

namespace XZDHospital2BMS.BackManager.medical_record
{

  public partial class add : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.hasPurviewPage("MedicalRecord_add");
        ViewState["AdminId"] = intAdminId;
      }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
      if (!HelperUtility.hasPurviewOP("MedicalRecord_add"))
      {
        string strUrl = "/BackManager/home.aspx";
        HelperUtility.showAlert("没有操作权限", strUrl);
      }
      string strMsgError = "";
      string strSickbedNumber = tbSickbedNumber.Value.Trim();
      string strNameReal = tbNameReal.Value.Trim();
      string strBirthday = tbBirthday.Value.Trim();
      string strDepartment = tbDepartment.Value.Trim();
      string strNameDisease = tbNameDisease.Value.Trim();
      string strTimeIn = tbTimeIn.Value.Trim();
      string strTimeOut = tbTimeOut.Value.Trim();
      string strSituationOut = cblSituationOut.SelectedValue;
      string strSituationIn = "";
      string strPhotoUrls = tbPhotoUrls.Value.Trim();
      string strComment = tbComment.Text.Trim();
      int intAdminId = Convert.ToInt32(ViewState["AdminId"]);

      if ("".Equals(strNameReal)) strMsgError += "姓名不能为空！\n";
      if ("".Equals(strBirthday)) strMsgError += "出生年月不能为空！\n";
      if ("".Equals(strNameDisease)) strMsgError += "疾病名称不能为空！\n";
      if ("".Equals(strTimeIn)) strMsgError += "入院日期不能为空！\n";
      if ("".Equals(strTimeOut)) strMsgError += "出院日期不能为空！\n";
      if (!HelperUtility.isDateType(strBirthday)) strMsgError += "出生年月格式不正确！\n";
      if (!HelperUtility.isDateType(strTimeIn)) strMsgError += "入院日期格式不正确！\n";
      if (!HelperUtility.isDateType(strTimeOut)) strMsgError += "出院日期格式不正确！\n";
      if (!"".Equals(strMsgError)) HelperUtility.showAlert(strMsgError, "add.aspx");
      // 验证完毕，提交数据
      ModelMedicalRecord model = new ModelMedicalRecord();
      model.sickbed_number = strSickbedNumber;
      model.name_real = strNameReal;
      model.sex = rblSex.SelectedValue.Trim();
      model.birthday = Convert.ToDateTime(strBirthday);
      model.department = strDepartment;
      model.name_disease = strNameDisease;
      model.time_in = Convert.ToDateTime(strTimeIn);
      model.time_out = Convert.ToDateTime(strTimeOut);
      model.situation_out = strSituationOut;
      model.situation_in = strSituationIn;
      model.photo_urls = strPhotoUrls;
      model.comment = strComment;
      model.time_create = DateTime.Now;
      model.id_admin = intAdminId;
      model.is_deleted = 0;

      int intId = BllMedicalRecord.add(model);
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

  }

}