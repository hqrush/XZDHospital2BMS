using Bll;
using Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XZDHospital2BMS.BackManager.sales_goods
{
  public partial class edit : System.Web.UI.Page
  {

    private int intPhotoAmounts = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int intAdminId = HelperUtility.hasPurviewPage("SalesGoods_update");
        ViewState["AdminId"] = intAdminId;
        ViewState["ContractId"] = HelperUtility.getQueryInt("cid");
        ViewState["ContractPage"] = HelperUtility.getQueryInt("cpage");
        ViewState["Page"] = HelperUtility.getQueryInt("page");
        int intId = HelperUtility.getQueryInt("id");
        ViewState["Id"] = intId;

        ModelSalesGoods model = BllSalesGoods.getById(intId);
        tbProductName.Value = model.name_product;
        tbType.Text = model.type;
        tbFactoryName.Value = model.name_factory;
        tbUnit.Text = model.unit;
        tbAmount.Text = model.amount.ToString();
        tbPriceUnit.Text = model.price_unit.ToString();
        tbBatchNumber.Value = model.batch_number;
        tbValidityPeriod.Value = model.validity_period.ToString();
        tbComment.Text = model.comment;
        // 设置照片
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
      string strUrl;
      // 验证权限
      if (!HelperUtility.hasPurviewOP("SalesGoods_update"))
        HelperUtility.showAlert("没有操作权限", "/BackManager/home.aspx");
      // 验证表单
      string strMsgError = "";

      int intId = Convert.ToInt32(ViewState["Id"]);
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
      if (!HelperUtility.isDateType(tbValidityPeriod.Value.Trim())) strMsgError += "有效期时间格式不正确！";
      DateTime dateValidityPeriod = Convert.ToDateTime(tbValidityPeriod.Value.Trim());
      string strApprovalNumber = "";
      string strComment = tbComment.Text.Trim();
      if (strComment.Length > 1000) strMsgError += "备注信息不能超过500个字数！";
      string strPhotoUrls = tbPhotoUrls.Value;
      if (strPhotoUrls.EndsWith(","))
        strPhotoUrls = strPhotoUrls.Substring(0, strPhotoUrls.Length - 1);

      if (!"".Equals(strMsgError))
      {
        HelperUtility.showAlert(strMsgError, "edit.aspx?id=" + intId);
        return;
      }
      // 验证完毕，提交数据
      ModelSalesGoods model = BllSalesGoods.getById(intId);
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
      BllSalesGoods.update(model);

      strUrl = "list.aspx" +
        "?cid=" + ViewState["ContractId"] +
        "&cpage=" + ViewState["ContractPage"] +
        "&page=" + ViewState["Page"];
      HelperUtility.showAlert("修改成功！", strUrl);
    }

  }

}