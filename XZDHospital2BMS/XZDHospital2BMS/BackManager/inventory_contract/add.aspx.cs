using Bll;
using Helper;
using Model;
using System;

namespace XZDHospital2BMS.BackManager.inventory_contract
{

  public partial class add : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.hasPurviewPage("InventoryContract_add");
        ViewState["AdminId"] = intAdminId;
      }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
      if (!HelperUtility.hasPurviewOP("InventoryContract_add"))
      {
        string strUrl = "/BackManager/home.aspx";
        HelperUtility.showAlert("没有操作权限", strUrl);
      }
      string strMsgError = "";
      string strNameSign = tbNameSign.Text.Trim();
      if ("".Equals(strNameSign)) strMsgError += "公司名不能为空！";
      string strComment = tbComment.Text.Trim();
      if (strComment.Length > 500) strMsgError += "备注信息不能超过500个字数！";
      if (!"".Equals(strMsgError))
      {
        HelperUtility.showAlert(strMsgError, "add.aspx");
        return;
      }
      string strPhotoUrls = "";
      // 验证完毕，提交数据
      int intAdminId = Convert.ToInt32(ViewState["AdminId"]);
      ModelInventoryContract model = new ModelInventoryContract();
      model.id_admin = intAdminId;
      model.name_sign = strNameSign;
      model.photo_urls = strPhotoUrls;
      model.comment = strComment;
      int intId = BllInventoryContract.add(model);
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