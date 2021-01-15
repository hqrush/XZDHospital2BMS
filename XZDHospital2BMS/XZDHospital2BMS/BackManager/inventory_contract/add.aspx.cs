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
        // 得到上次盘点的结束时间
        ModelInventoryContract model = BllInventoryContract.getLatestContract();
        lblTimeLast.Text = model.time_end.ToString("yyyy-MM-dd");
        tbTimeStart.Value = model.time_end.ToString("yyyy-MM-dd");
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

      string strTimeStart = tbTimeStart.Value.Trim();
      if ("".Equals(strTimeStart)) strMsgError += "开始时间不能为空！";
      if (!HelperUtility.isDateType(strTimeStart)) strMsgError += "开始时间格式不正确！";
      string strTimeEnd = tbTimeEnd.Value.Trim();
      if ("".Equals(strTimeEnd)) strMsgError += "结束时间不能为空！";
      if (!HelperUtility.isDateType(strTimeEnd)) strMsgError += "结束时间格式不正确！";
      if (Convert.ToDateTime(strTimeEnd) <= Convert.ToDateTime(strTimeStart)) strMsgError += "结束时间不能早于开始时间！";
      string strNameSign = tbNameSign.Text.Trim();
      if ("".Equals(strNameSign)) strMsgError += "盘点人员签名不能为空！";
      string strComment = tbComment.Text.Trim();
      if (strComment.Length > 500) strMsgError += "备注信息不能超过500个字数！";
      if (!"".Equals(strMsgError))
      {
        HelperUtility.showAlert(strMsgError, "add.aspx");
        return;
      }
      string strPhotoUrls = "";
      // 验证完毕，提交数据，盘点单表添加一条记录
      int intAdminId = Convert.ToInt32(ViewState["AdminId"]);
      ModelInventoryContract model = new ModelInventoryContract();
      model.id_admin = intAdminId;
      model.name_sign = strNameSign;
      model.photo_urls = strPhotoUrls;
      model.comment = strComment;
      model.time_start = Convert.ToDateTime(strTimeStart);
      model.time_end = Convert.ToDateTime(strTimeEnd);
      int intContractId = BllInventoryContract.add(model);
      if (intContractId > 0)
      {
        // 盘点单表添加完成后，在盘点物品表里添加所有库存量大于0的货品记录
        BllInventoryRecord.setRecord(intContractId);
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