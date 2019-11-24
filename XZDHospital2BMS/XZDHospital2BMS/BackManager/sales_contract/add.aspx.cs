using Bll;
using Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
      if ("".Equals(strCompanyName)) strMsgError += "公司名不能为空！\n";
      string strTimeSign = tbTimeSign.Value.ToString();
      if ("".Equals(strTimeSign)) strMsgError += "入库单签发时间不能为空！\n";
      string strComment = tbComment.Text.Trim();
      if (strComment.Length > 1000) strMsgError += "备注信息不能超过500个字数！\n";
      if (!"".Equals(strMsgError)) HelperUtility.showAlert(strMsgError, "add.aspx");
      string strPhotoUrls = "";
      // 验证完毕，提交数据
      ModelSalesContract model = new ModelSalesContract();
      model.id_company = BllSalesCompany.getIdByName(strCompanyName);
      model.id_admin = (int)ViewState["AdminId"];
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