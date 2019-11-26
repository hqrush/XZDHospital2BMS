using System;
using System.Web;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Web.Services.Description;
using System.Text;
using System.Diagnostics;
using Model;
using Helper;

namespace XZDHospital2BMS.Handler
{

  public class UploadFileHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
  {

    public void ProcessRequest(HttpContext context)
    {
      context.Response.ContentType = "text/plain";
      string responseText = "OK";
      HttpFileCollection files = context.Request.Files;
      if (files != null || files.Count > 0)
      {
        context.Response.Write(responseText);
      }
      if (context.Request.Files["photo_file"] != null)
      {
        HttpPostedFile objHPF = context.Request.Files["photo_file"];
        //context.Server.UrlDecode(context.Request.Form.ToString());
        //HttpFileCollection files = HttpContext.Current.Request.Files;
        ModelUploadFileConfig objConfig = new ModelUploadFileConfig();
        objConfig.AllowUploadFileExt = "jpg,jpeg,png";
        objConfig.AllowUploadImageFileExt = "jpg,jpeg,png";
        objConfig.AllowUploadImageFileSize = 40;
        HelperUploadFile.SaveULFileFromHPF(objHPF, objConfig);
        if (objConfig.OPFlag)
        {
          context.Response.Write(responseText);
          //string strRootPath = MapPath("/").Replace("//", "/");
          //string strImgPath = objConfig.ServerFileFullPath;
          //strImgPath = strImgPath.Substring(strRootPath.Length);
          //ViewState["PhotoPath"] = strImgPath;
          //PhotoUrls += objConfig.ServerFileFullPath + ",";
          //fuPhoto.Visible = false;
          //btnUploadPhoto.Visible = false;
          //imgPhoto.Visible = true;
          //imgPhoto.ImageUrl = strImgPath;
          //btnDelPhoto.Visible = true;
        }
        else
        {
          responseText = "error";
          context.Response.Write(responseText);
          //ViewState["PhotoPath"] = "";
          //string strOPMsg = objConfig.OPMessage;
          //string strJS = "<script>alert('【" + strOPMsg + "】，上传头像失败！请重新上传！');";
          //strJS += "</script>";
          //Response.Write(strJS);
          //return;
        }
        context.Response.Write(responseText);
      }
      else
      {
        context.Response.Write(responseText);
      }
    }

    public bool IsReusable
    {
      get
      {
        return false;
      }
    }

  }

}