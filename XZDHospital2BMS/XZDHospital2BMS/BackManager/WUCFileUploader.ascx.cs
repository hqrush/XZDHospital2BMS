using Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XZDHospital2BMS.BackManager
{
  public partial class WUCFileUploader : System.Web.UI.UserControl
  {

    private string photoUrls = "";

    public string PhotoUrls { get => photoUrls; set => photoUrls = value; }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnUploadPhoto_Click(object sender, EventArgs e)
    {
      string strScript = "<script>alert('服务器端的click');</script>";
      Response.Write(strScript);

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

          ModelUploadFileConfig objConfig = new ModelUploadFileConfig();
          objConfig.AllowUploadFileExt = "jpg,jpeg,png";
          objConfig.AllowUploadImageFileExt = "jpg,jpeg,png";
          objConfig.AllowUploadImageFileSize = 40;
          HelperUploadFile.SaveULFileFromHPF(files[ifile], objConfig);
          if (objConfig.OPFlag)
          {
            string strRootPath = MapPath("/").Replace("//", "/");
            string strImgPath = objConfig.ServerFileFullPath;
            strImgPath = strImgPath.Substring(strRootPath.Length);
            ViewState["PhotoPath"] = strImgPath;
            PhotoUrls += objConfig.ServerFileFullPath + ",";
            //fuPhoto.Visible = false;
            //btnUploadPhoto.Visible = false;
            //imgPhoto.Visible = true;
            //imgPhoto.ImageUrl = strImgPath;
            //btnDelPhoto.Visible = true;
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
      }
    }

    protected void btnDelPhoto_Click(object sender, EventArgs e)
    {
      //string strRootPath = MapPath("/").Replace("//", "/");
      //string strImgPath = strRootPath + PhotoUrls.ImageUrl;
      //HelperFile.DeleteFile(strImgPath);
      //fuPhoto.Visible = true;
      //btnUploadPhoto.Visible = true;
      //imgPhoto.Visible = false;
      //btnDelPhoto.Visible = false;
    }

  }
}