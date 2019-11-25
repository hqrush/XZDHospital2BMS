using Bll;
using Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

    protected void btnUploadPhoto_Click(object sender, EventArgs e)
    {

      HttpFileCollection files = HttpContext.Current.Request.Files;
      //获得图片描述的文本框字符串数组，为对应的图片的描述
      string[] rd = Request.Form[1].Split(',');
      Debug.WriteLine("rd:" + rd);
      int ifile;
      for (ifile = 0; ifile < files.Count; ifile++)
      {
        if (files[ifile].FileName.Length > 0)
        {
          //上传单个文件并保存相关信息
        }
      }

      ModelUploadFileConfig objConfig = new ModelUploadFileConfig();
      objConfig.AllowUploadFileExt = "jpg,jpeg,png";
      objConfig.AllowUploadImageFileExt = "jpg,jpeg,png";
      objConfig.AllowUploadImageFileSize = 40;
      HelperUploadFile.SaveULFileFromControl(fuPhoto, objConfig);
      if (objConfig.OPFlag)
      {
        string strRootPath = MapPath("/").Replace("//", "/");
        string strImgPath = objConfig.ServerFileFullPath;
        strImgPath = strImgPath.Substring(strRootPath.Length);
        ViewState["PhotoPath"] = strImgPath;

        fuPhoto.Visible = false;
        btnUploadPhoto.Visible = false;
        imgPhoto.Visible = true;
        imgPhoto.ImageUrl = strImgPath;
        btnDelPhoto.Visible = true;
      }
      else
      {
        ViewState["PhotoPath"] = "";
        string strOPMsg = objConfig.OPMessage;
        string strJS = "<script>alert('【" + strOPMsg + "】，上传头像失败！请重新上传！');";
        strJS += "</script>";
        Response.Write(strJS);
        return;
      }
    }

    protected void btnDelPhoto_Click(object sender, EventArgs e)
    {
      ViewState["PhotoPath"] = "";
      string strRootPath = MapPath("/").Replace("//", "/");
      string strImgPath = strRootPath + imgPhoto.ImageUrl;
      HelperFile.DeleteFile(strImgPath);
      fuPhoto.Visible = true;
      btnUploadPhoto.Visible = true;
      imgPhoto.Visible = false;
      btnDelPhoto.Visible = false;
    }

  }
}