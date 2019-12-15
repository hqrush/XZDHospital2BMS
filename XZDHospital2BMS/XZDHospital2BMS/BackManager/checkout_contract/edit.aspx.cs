using Bll;
using Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace XZDHospital2BMS.BackManager.checkout_contract
{

  public partial class edit : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.hasPurviewPage("CheckoutContract_update");
        ViewState["AdminId"] = intAdminId;
        // 本页只能从list.aspx的编辑页转过来
        // 因此要得到要修改的id值和页面的page值用于修改成功后返回
        int intId = HelperUtility.getQueryInt("id");
        ViewState["id"] = intId;
        int intPage = HelperUtility.getQueryInt("page");
        ViewState["page"] = intPage;
        // 根据入库单id查询得到入库单model
        ModelCheckoutContract model = BllCheckoutContract.getById(intId);
        List<string> listUnitName = model.name_unit.Split(',').ToList();
        if (!"".Equals(listUnitName[0])) cbUnitName1.Checked = true;
        if (!"".Equals(listUnitName[1])) cbUnitName2.Checked = true;
        tbDepartmentName.Value = model.name_department;
        tbSignName.Value = model.name_sign;
        tbComment.Text = model.comment;
        BllDepartment.bindRPT(rptName);
      }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
      if (!HelperUtility.hasPurviewOP("CheckoutContract_update"))
        HelperUtility.showAlert("没有操作权限", "/BackManager/home.aspx");
      int intAdminId = (int)ViewState["AdminId"];
      int intId = (int)ViewState["id"];
      int intPage = (int)ViewState["page"];
      // 验证输入
      string strMsgError = "";
      string strUnitName, strUnitName1, strUnitName2;
      strUnitName1 = cbUnitName1.Checked ? cbUnitName1.Value : "";
      strUnitName2 = cbUnitName2.Checked ? cbUnitName2.Value : "";
      strUnitName = strUnitName1 + "," + strUnitName2;
      if (",".Equals(strUnitName)) strMsgError += "申请单位至少选一个！";
      string strDepartmentName = tbDepartmentName.Value.Trim();
      if ("".Equals(strDepartmentName)) strMsgError += "申请部门/科室不能为空！";
      string strSignName = tbSignName.Value.Trim();
      if ("".Equals(strSignName)) strMsgError += "申请人姓名不能为空！";
      string strComment = tbComment.Text.Trim();
      if (strComment.Length > 500) strMsgError += "备注信息不能超过500个字数！";
      if (!"".Equals(strMsgError))
      {
        HelperUtility.showAlert(strMsgError, "add.aspx");
        return;
      }
      string strPhotoUrls = "";
      // 验证完毕，提交数据
      ModelCheckoutContract model = BllCheckoutContract.getById(intId);
      model.id_admin = intAdminId;
      model.name_unit = strUnitName;
      model.name_department = strDepartmentName;
      model.name_sign = strSignName;
      model.photo_urls = strPhotoUrls;
      model.comment = strComment;
      // 更新数据库记录
      BllCheckoutContract.update(model);
      // 跳转回列表页
      Response.Redirect("/BackManager/checkout_contract/list.aspx?page=" + intPage);
    }

  }

}