using Bll;
using Helper;
using Model;
using System;

namespace XZDHospital2BMS.BackManager.sales_goods
{

  public partial class add : System.Web.UI.Page
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.hasPurviewPage("SalesGoods_add");
        ViewState["AdminId"] = intAdminId;
        // 本页只能从入库单的list.aspx的添加入库货品转过来
        // 因此要得到入库单的cid值和页面的cpage值用于修改成功后返回
        int intCId = HelperUtility.getQueryInt("cid");
        ViewState["ContractId"] = intCId;
        int intCPage = HelperUtility.getQueryInt("cpage");
        ViewState["Page"] = intCPage;
      }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
      string strUrl;
      // 验证权限
      if (!HelperUtility.hasPurviewOP("SalesGoods_add"))
        HelperUtility.showAlert("没有操作权限", "/BackManager/home.aspx");
      int intCPage = (int)ViewState["Page"];
      // 验证表单
      string strMsgError = "";

      int intContractId = (int)ViewState["ContractId"];
      string strProductName = tbProductName.Value.Trim();
      if ("".Equals(strProductName)) strMsgError += "货品名称不能为空！";
      string strType = tbType.Text.Trim();
      string strFactoryName = tbFactoryName.Value.Trim();
      string strUnit = tbUnit.Text.Trim();
      decimal dcmAmount = Convert.ToDecimal(tbAmount.Text.Trim());
      decimal dcmPriceUnit = Convert.ToDecimal(tbPriceUnit.Text.Trim());
      decimal dcmPriceTotal = dcmAmount * dcmPriceUnit;
      string strBatchNumber = tbBatchNumber.Value.Trim();
      if ("".Equals(tbValidityPeriod.Value.Trim())) strMsgError += "有效期时间不能为空！";
      if (HelperUtility.isDateType(tbValidityPeriod.Value.Trim())) strMsgError += "有效期时间格式不正确！";
      DateTime dateValidityPeriod = Convert.ToDateTime(tbValidityPeriod.Value.Trim());
      string strApprovalNumber = "";
      string strComment = tbComment.Text.Trim();
      if (strComment.Length > 1000) strMsgError += "备注信息不能超过500个字数！";
      string strPhotoUrls = tbPhotoUrls.Value;
      if (strPhotoUrls.EndsWith(","))
        strPhotoUrls = strPhotoUrls.Substring(0, strPhotoUrls.Length - 1);
      int intAdminId = (int)ViewState["AdminId"];
      DateTime dateTimeAdd = DateTime.Now;

      if (!"".Equals(strMsgError))
      {
        strUrl = "add.aspx?cid=" + intContractId + "&cpage=" + intCPage;
        HelperUtility.showAlert(strMsgError, strUrl);
        return;
      }
      // 验证完毕，提交数据
      ModelSalesGoods model = new ModelSalesGoods();

      model.id_contract = intContractId;
      model.name_product = strProductName;
      model.type = strType;
      model.name_factory = strFactoryName;
      model.unit = strUnit;
      model.amount = dcmAmount;
      model.price_unit = dcmPriceUnit;
      model.price_total = dcmPriceTotal;
      model.batch_number = strBatchNumber;
      model.validity_period = dateValidityPeriod;
      model.approval_number = strApprovalNumber;
      model.comment = strComment;
      model.photo_urls = strPhotoUrls;
      model.id_admin = intAdminId;
      model.time_add = dateTimeAdd;

      int intId = BllSalesGoods.add(model);
      if (intId > 0)
      {
        strUrl = "list.aspx";
        HelperUtility.showAlert("添加成功！", strUrl);
      }
      else
      {
        strUrl = "add.aspx?cid=" + intContractId + "&cpage=" + intCPage;
        HelperUtility.showAlert("添加失败，请联系管理员！", strUrl);
      }
    }

  }

}