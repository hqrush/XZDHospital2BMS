using Bll;
using Helper;
using Model;
using System;

namespace XZDHospital2BMS.BackManager.inventory_contract
{

  public partial class edit : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.hasPurviewPage("InventoryContract_update");
        ViewState["AdminId"] = intAdminId;
        // 本页只能从list.aspx的编辑页转过来
        // 因此要得到要修改的id值和页面的page值用于修改成功后返回
        int intId = HelperUtility.getQueryInt("id");
        ViewState["id"] = intId;
        int intPage = HelperUtility.getQueryInt("page");
        ViewState["page"] = intPage;
        // 根据入库单id查询得到入库单model
        ModelInventoryContract model = BllInventoryContract.getById(intId);
        tbNameSign.Text = model.name_sign;
        tbComment.Text = model.comment;
      }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
      if (!HelperUtility.hasPurviewOP("InventoryContract_update"))
        HelperUtility.showAlert("没有操作权限", "/BackManager/home.aspx");
      int intAdminId = Convert.ToInt32(ViewState["AdminId"]);
      int intId = Convert.ToInt32(ViewState["id"]);
      int intPage = Convert.ToInt32(ViewState["page"]);
      string strThisPageUrl = "edit.aspx?id=" + intId + "&page=" + intPage;
      string strMsgError = "";
      // 验证输入
      string strNameSign = tbNameSign.Text.Trim();
      if ("".Equals(strNameSign)) strMsgError += "公司名不能为空！";
      string strComment = tbComment.Text.Trim();
      if (strComment.Length > 500) strMsgError += "备注信息不能超过500个字数！";
      if (!"".Equals(strMsgError))
      {
        HelperUtility.showAlert(strMsgError, strThisPageUrl);
        return;
      }
      // 验证完毕，提交数据
      ModelInventoryContract model = BllInventoryContract.getById(intId);
      model.name_sign = strNameSign;
      model.comment = strComment;
      // 更新数据库记录
      BllInventoryContract.update(model);
      // 跳转回列表页
      Response.Redirect("/BackManager/inventory_contract/list.aspx?page=" + intPage);
    }

  }

}