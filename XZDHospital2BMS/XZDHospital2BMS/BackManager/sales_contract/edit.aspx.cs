using Bll;
using Helper;
using Model;
using System;

namespace XZDHospital2BMS.BackManager.sales_contract
{

  public partial class edit : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.hasPurviewPage("SalesContract_update");
        ViewState["AdminId"] = intAdminId;
        // 本页只能从list.aspx的编辑页转过来
        // 因此要得到要修改的id值和页面的page值用于修改成功后返回
        int intId = HelperUtility.getQueryInt("id");
        ViewState["id"] = intId;
        int intPage = HelperUtility.getQueryInt("page");
        ViewState["page"] = intPage;
        // 绑定销售公司名称的下拉列表数据
        BllSalesCompany.bindRPT(rptName);
        // 根据入库单id查询得到入库单model
        ModelSalesContract model = BllSalesContract.getById(intId);
        int intCompanyId = model.id_company;
        if (intCompanyId > 0) tbCompanyName.Text = (BllSalesCompany.getById(intCompanyId)).name;
        else tbCompanyName.Text = "未知公司";
        tbTimeSign.Value = model.time_sign.ToString("yyyy-MM-dd");
        tbComment.Text = model.comment;
      }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
      if (!HelperUtility.hasPurviewOP("SalesContract_update"))
        HelperUtility.showAlert("没有操作权限", "/BackManager/home.aspx");
      int intAdminId = (int)ViewState["AdminId"];
      int intId = (int)ViewState["id"];
      int intPage = (int)ViewState["page"];
      string strThisPageUrl = "edit.aspx?id=" + intId + "&page=" + intPage;
      string strMsgError = "";
      // 验证输入
      string strCompanyName = tbCompanyName.Text.Trim();
      if ("".Equals(strCompanyName)) strMsgError += "公司名不能为空！";
      string strTimeSign = tbTimeSign.Value.ToString();
      if ("".Equals(strTimeSign)) strMsgError += "入库单签发时间不能为空！";
      if (!HelperUtility.isDateType(strTimeSign)) strMsgError += "入库单签发时间格式不正确！";
      string strComment = tbComment.Text.Trim();
      if (strComment.Length > 500) strMsgError += "备注信息不能超过500个字数！";
      if (!"".Equals(strMsgError))
      {
        HelperUtility.showAlert(strMsgError, strThisPageUrl);
        return;
      }
      string strPhotoUrls = "";
      // 验证完毕，提交数据
      ModelSalesContract model = BllSalesContract.getById(intId);
      if (strCompanyName.Contains("未知公司")) model.id_company = 0;
      else model.id_company = BllSalesCompany.getIdByName(strCompanyName, intAdminId);
      model.id_admin = intAdminId;
      model.photo_urls = strPhotoUrls;
      model.comment = strComment;
      model.time_sign = Convert.ToDateTime(strTimeSign);
      // 更新数据库记录
      BllSalesContract.update(model);
      // 跳转回列表页
      Response.Redirect("/BackManager/sales_contract/list.aspx?page=" + intPage);
    }

  }

}