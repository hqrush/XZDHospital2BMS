using System.Web;
using Model;
using Helper;

namespace XZDHospital2BMS.Handler
{

  public class UploadHandler : IHttpHandler
  {

    public void ProcessRequest(HttpContext context)
    {
      // context.Response.ContentType = "text/plain";
      context.Response.ContentType = "application/json";
      string strReturn = "";
      // HttpPostedFile objHPF = context.Request.Files[0];
      HttpPostedFile objHPF = context.Request.Files["photo_file"];
      if (objHPF == null)
      {
        strReturn = "{\"Message\" : \"要上传的文件为空！\", ";
        strReturn += "\"ServerFilePath\" : \"" + "" + "\"}";
        context.Response.Write(strReturn);
        return;
      }
      ModelUploadFileConfig objConfig = new ModelUploadFileConfig();
      objConfig.AllowUploadFileExt = "jpg,jpeg,png";
      objConfig.AllowUploadImageFileExt = "jpg,jpeg,png";
      HelperUploadFile.SaveULFileFromHPF(objHPF, objConfig);
      if (objConfig.OPFlag)
      {
        strReturn = "{\"Message\" : \"" + "" + "\", ";
        strReturn += "\"ServerFilePath\" : \"" + objConfig.ServerFilePath + "\"}";
      }
      else
      {
        strReturn = "{\"Message\" : \"" + objConfig.OPMessage + "\", ";
        strReturn += "\"ServerFilePath\" : \"" + "" + "\"}";
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