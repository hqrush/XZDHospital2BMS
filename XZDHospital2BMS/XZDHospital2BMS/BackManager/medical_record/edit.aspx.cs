using Bll;
using Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace XZDHospital2BMS.BackManager.medical_record
{

  public partial class edit : System.Web.UI.Page
  {

    private int intPhotoAmounts = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.hasPurviewPage("MedicalRecord_update");
        ViewState["AdminId"] = intAdminId;
        // 本页只能从list.aspx的编辑页转过来
        // 因此要得到要修改的id值和页面的page值用于修改成功后返回
        int intId = HelperUtility.getQueryInt("id");
        ViewState["id"] = intId;
        int intPage = HelperUtility.getQueryInt("page");
        ViewState["page"] = intPage;
        // 根据入库单id查询得到入库单model
        ModelMedicalRecord model = BllMedicalRecord.getById(intId);

        tbSickbedNumber.Value = model.sickbed_number;
        tbNameReal.Value = model.name_real;
        rblSex.SelectedValue = model.sex;
        tbBirthday.Value = model.birthday.ToString("yyyy-MM-dd");
        tbDepartment.Value = model.department;
        tbNameDisease.Value = model.name_disease;
        tbTimeIn.Value = model.time_in.ToString("yyyy-MM-dd");
        tbTimeOut.Value = model.time_out.ToString("yyyy-MM-dd");
        cblSituationOut.SelectedValue = model.situation_out;
        tbComment.Text = model.comment;
        string strPhotoUrls = model.photo_urls;
        if (!"".Equals(strPhotoUrls))
        {
          string strImgUrl, strJS;
          List<string> listPhotoUrls = strPhotoUrls.Split(',').ToList();
          intPhotoAmounts = listPhotoUrls.Count;
          for (int i = 0; i < intPhotoAmounts; i++)
          {
            strImgUrl = listPhotoUrls[i];
            strJS = "<div id=\"img-" + i + "\" class=\"wrapper-photo-show\">";
            strJS += "<img width=\"100\" height=\"100\" src=\"" + strImgUrl + "\" /><br />";
            strJS += "<input type=\"button\" id=\"btnDelPhoto\" class=\"btn btn-sm btn-warning\"" +
              " onclick=\"delPhoto(" + i + ")\" value=\"删除\" /></div>";
            ltrShowPhoto.Text += strJS;
          }
        }
        tbPhotoUrls.Value = strPhotoUrls;
      }
    }

    // 从数据库里读取入库单凭证照片的数量，设置js变量的值
    public int setPhotoAmount()
    {
      return intPhotoAmounts;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
      if (!HelperUtility.hasPurviewOP("MedicalRecord_update"))
        HelperUtility.showAlert("没有操作权限", "/BackManager/home.aspx");
      int intAdminId = Convert.ToInt32(ViewState["AdminId"]);
      int intId = Convert.ToInt32(ViewState["id"]);
      int intPage = Convert.ToInt32(ViewState["page"]);
      string strThisPageUrl = "edit.aspx?id=" + intId + "&page=" + intPage;
      string strMsgError = "";
      string strSickbedNumber = tbSickbedNumber.Value.Trim();
      string strNameReal = tbNameReal.Value.Trim();
      string strBirthday = tbBirthday.Value.Trim();
      string strDepartment = tbDepartment.Value.Trim();
      string strNameDisease = tbNameDisease.Value.Trim();
      string strTimeIn = tbTimeIn.Value.Trim();
      string strTimeOut = tbTimeOut.Value.Trim();
      string strPhotoUrls = tbPhotoUrls.Value.Trim();
      string strComment = tbComment.Text.Trim();

      if ("".Equals(strNameReal)) strMsgError += "姓名不能为空！\n";
      if ("".Equals(strBirthday)) strMsgError += "出生年月不能为空！\n";
      if ("".Equals(strNameDisease)) strMsgError += "疾病名称不能为空！\n";
      if ("".Equals(strTimeIn)) strMsgError += "入院日期不能为空！\n";
      if ("".Equals(strTimeOut)) strMsgError += "出院日期不能为空！\n";
      if (!HelperUtility.isDateType(strBirthday)) strMsgError += "出生年月格式不正确！\n";
      if (!HelperUtility.isDateType(strTimeIn)) strMsgError += "入院日期格式不正确！\n";
      if (!HelperUtility.isDateType(strTimeOut)) strMsgError += "出院日期格式不正确！\n";
      if (!"".Equals(strMsgError)) HelperUtility.showAlert(strMsgError, strThisPageUrl);
      // 验证完毕，提交数据
      ModelMedicalRecord model = BllMedicalRecord.getById(intId);
      model.sickbed_number = strSickbedNumber;
      model.name_real = strNameReal;
      model.sex = rblSex.SelectedValue.Trim();
      model.birthday = Convert.ToDateTime(strBirthday);
      model.department = strDepartment;
      model.name_disease = strNameDisease;
      model.time_in = Convert.ToDateTime(strTimeIn);
      model.time_out = Convert.ToDateTime(strTimeOut);
      model.situation_out = cblSituationOut.SelectedValue;
      model.photo_urls = strPhotoUrls;
      model.comment = strComment;
      // 更新数据库记录
      BllMedicalRecord.update(model);
      // 跳转回列表页
      Response.Redirect("/BackManager/medical_record/list.aspx?page=" + intPage);
    }

  }

}