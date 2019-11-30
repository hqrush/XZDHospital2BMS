using Bll;
using Helper;
using Model;
using System;

namespace XZDHospital2BMS.BackManager.sales_contract
{
  public partial class add : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.hasPurviewPage("SalesContract_add");
        ViewState["AdminId"] = intAdminId;
        BllSalesCompany.bindRPT(rptName);
      }
    }

    protected void btnCompanyContractAdd_Click(object sender, EventArgs e)
    {
      if (!HelperUtility.hasPurviewOP("SalesContract_add"))
      {
        string strUrl = "/BackManager/home.aspx";
        HelperUtility.showAlert("没有操作权限", strUrl);
      }
      string strMsgError = "";
      string strCompanyName = tbCompanyName.Text.Trim();
      if ("".Equals(strCompanyName)) strMsgError += "公司名不能为空！";
      string strTimeSign = tbTimeSign.Value.ToString();
      if ("".Equals(strTimeSign)) strMsgError += "入库单签发时间不能为空！";
      string strComment = tbComment.Text.Trim();
      if (strComment.Length > 1000) strMsgError += "备注信息不能超过500个字数！";
      if (!"".Equals(strMsgError))
      {
        HelperUtility.showAlert(strMsgError, "add.aspx");
        return;
      }
      string strPhotoUrls = tbPhotoUrls.Value;
      if (strPhotoUrls.EndsWith(","))
        strPhotoUrls = strPhotoUrls.Substring(0, strPhotoUrls.Length - 1);
      // 验证完毕，提交数据
      int intAdminId = (int)ViewState["AdminId"];
      ModelSalesContract model = new ModelSalesContract();
      if (strCompanyName.Contains("未知公司")) model.id_company = 0;
      else model.id_company = BllSalesCompany.getIdByName(strCompanyName, intAdminId);
      model.id_admin = intAdminId;
      model.photo_urls = strPhotoUrls;
      model.comment = strComment;
      int intId = BllSalesContract.add(model);
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