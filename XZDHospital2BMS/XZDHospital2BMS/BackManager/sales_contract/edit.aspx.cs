using Bll;
using Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace XZDHospital2BMS.BackManager.sales_contract
{

  public partial class edit : System.Web.UI.Page
  {

    private int intPhotoAmounts = 0;

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
        tbCompanyName.Text = (BllSalesCompany.getById(intCompanyId)).name;
        tbTimeSign.Value = model.time_sign.ToString("yyyy-MM-dd");
        tbComment.Text = model.comment;
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
      if (HelperUtility.isDateType(strTimeSign)) strMsgError += "入库单签发时间格式不正确！";
      string strComment = tbComment.Text.Trim();
      if (strComment.Length > 1000) strMsgError += "备注信息不能超过500个字数！";
      if (!"".Equals(strMsgError))
      {
        HelperUtility.showAlert(strMsgError, strThisPageUrl);
        return;
      }
      string strPhotoUrls = tbPhotoUrls.Value;
      if (strPhotoUrls.EndsWith(","))
        strPhotoUrls = strPhotoUrls.Substring(0, strPhotoUrls.Length - 1);
      // 验证完毕，提交数据
      ModelSalesContract model = new ModelSalesContract();
      if (strCompanyName.Contains("未知公司")) model.id_company = 0;
      else model.id_company = BllSalesCompany.getIdByName(strCompanyName, intAdminId);
      model.id_admin = intAdminId;
      model.photo_urls = strPhotoUrls;
      model.comment = strComment;
      // 更新数据库记录
      BllSalesContract.update(model);
      // 跳转回列表页
      Response.Redirect("/BackManager/sales_contract/list.aspx?page=" + intPage);
    }

  }

}