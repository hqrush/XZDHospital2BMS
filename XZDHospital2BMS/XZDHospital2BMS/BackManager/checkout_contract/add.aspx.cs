using Bll;
using Helper;
using Model;
using System;

namespace XZDHospital2BMS.BackManager.checkout_contract
{

  public partial class add : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.hasPurviewPage("CheckoutContract_add");
        ViewState["AdminId"] = intAdminId;
        BllDepartment.bindRPT(rptName);
        if (HelperUtility.hasPurviewPage("SUPERADMIN") > 0)
          cbFlag.Visible = true;
      }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
      if (!HelperUtility.hasPurviewOP("CheckoutContract_add"))
      {
        string strUrl = "/BackManager/home.aspx";
        HelperUtility.showAlert("没有操作权限", strUrl);
      }
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
      int intAdminId = (int)ViewState["AdminId"];
      ModelCheckoutContract model = new ModelCheckoutContract();
      model.id_admin = intAdminId;
      model.time_create = DateTime.Now;
      model.name_unit = strUnitName;
      model.name_department = strDepartmentName;
      model.name_sign = strSignName;
      model.photo_urls = strPhotoUrls;
      model.comment = strComment;
      if (cbFlag.Checked) model.flag = 1; else model.flag = 0;
      int intId = BllCheckoutContract.add(model);
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