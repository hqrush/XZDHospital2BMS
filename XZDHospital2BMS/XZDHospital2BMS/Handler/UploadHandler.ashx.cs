using System.Web;
using Model;
using Helper;
using System.Diagnostics;

namespace XZDHospital2BMS.Handler
{

  public class UploadHandler : IHttpHandler
  {

    public void ProcessRequest(HttpContext context)
    {
      // context.Response.ContentType = "text/plain";
      context.Response.ContentType = "application/json";
      string strReturn = "";
      HttpPostedFile objHPF = context.Request.Files[0];
      Debug.WriteLine("在 UploadHandler 里得到的 objHPF 是" + objHPF);
      if (objHPF == null)
      {
        strReturn = "{\"chunked\" : true, ";
        strReturn += "\"hasError\" : true, ";
        strReturn += "\"message\" : \"要上传的文件为空！\", ";
        strReturn += "\"filePath\" : \"" + "" + "\"}";
        context.Response.Write(strReturn);
        return;
      }
      ModelUploadFileConfig objConfig = new ModelUploadFileConfig();
      objConfig.AllowUploadFileExt = "jpg,jpeg,png";
      objConfig.AllowUploadImageFileExt = "jpg,jpeg,png";
      HelperUploadFile.SaveULFileFromHPF(objHPF, objConfig);
      if (objConfig.OPFlag)
      {
        strReturn = "{\"chunked\" : true, ";
        strReturn += "\"hasError\" : false, ";
        strReturn += "\"message\" : \"" + "" + "\", ";
        strReturn += "\"filePath\" : \"" + objConfig.ServerFilePath + "\"}";
      }
      else
      {
        strReturn = "{\"chunked\" : true, ";
        strReturn += "\"hasError\" : true, ";
        strReturn += "\"message\" : \"" + objConfig.OPMessage + "\", ";
        strReturn += "\"filePath\" : \"" + "" + "\"}";
      }
      context.Response.Write(strReturn);
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