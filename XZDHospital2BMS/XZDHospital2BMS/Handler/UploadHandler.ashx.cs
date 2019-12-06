using System.Web;
using Model;
using Helper;
using System.IO;

namespace XZDHospital2BMS.Handler
{

  public class UploadHandler : IHttpHandler
  {

    public void ProcessRequest(HttpContext context)
    {
      // context.Response.ContentType = "text/plain";
      context.Response.ContentType = "application/json";
      string strOPFlag;
      if (context.Request.Params["op_flag"] == null ||
        "".Equals(context.Request.Params["op_flag"].ToString()))
      {
        context.Response.Write(HelperUtility.setReturnJson("500", "需要指明操作类型！", ""));
        return;
      }
      strOPFlag = context.Request.Params["op_flag"].ToString();
      switch (strOPFlag)
      {
        case "UploadFile":
          UploadFile(context);
          break;
        case "DelFile":
          DelFile(context);
          break;
        default:
          context.Response.Write(HelperUtility.setReturnJson("500", "需要指明操作类型！", ""));
          break;
      }
    }

    private void UploadFile(HttpContext context)
    {
      // HttpPostedFile objHPF = context.Request.Files[0];
      HttpPostedFile objHPF = context.Request.Files["photo_file"];
      if (objHPF == null)
      {
        context.Response.Write(HelperUtility.setReturnJson("500", "要上传的文件为空！", ""));
        return;
      }
      ModelUploadFileConfig objConfig = new ModelUploadFileConfig();
      objConfig.AllowUploadFileExt = "jpg,jpeg,png";
      objConfig.AllowUploadImageFileExt = "jpg,jpeg,png";
      HelperUploadFile.SaveULFileFromHPF(objHPF, objConfig);
      if (objConfig.OPFlag)
        context.Response.Write(HelperUtility.setReturnJson("200", "", objConfig.ServerFilePath));
      else
        context.Response.Write(HelperUtility.setReturnJson("500", objConfig.OPMessage, ""));
    }

    private void DelFile(HttpContext context)
    {
      if (context.Request.Params["photo_url"] == null ||
        "".Equals(context.Request.Params["photo_url"].ToString()))
      {
        context.Response.Write(HelperUtility.setReturnJson("500", "需要指明要删除的图片地址！", ""));
        return;
      }
      string strUrl = context.Request.Params["photo_url"].ToString();
      string strRootPath = HttpContext.Current.Request.PhysicalApplicationPath;
      string strFileFullPath = strRootPath + strUrl;
      if (File.Exists(strFileFullPath))
      {
        File.Delete(strFileFullPath);
        context.Response.Write(HelperUtility.setReturnJson("200", "", ""));
      }
      else
        context.Response.Write(HelperUtility.setReturnJson("500", "删除文件出错！", ""));
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