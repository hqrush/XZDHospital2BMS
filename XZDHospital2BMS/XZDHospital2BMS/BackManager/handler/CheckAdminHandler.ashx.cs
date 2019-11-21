using Bll;
using System.Web;

namespace XZDHospital2BMS.BackManager.handler
{

  public class CheckAdminHandler : IHttpHandler
  {

    public void ProcessRequest(HttpContext context)
    {
      context.Response.ContentType = "text/plain";
      if (context.Request.Form["username"] == null || "".Equals(context.Request.Form["username"].ToString()))
        context.Response.Write("POST提交的username不能为空！");
      string strUsername = context.Request.Form["username"].ToString();
      if (BllAdmin.hasUsername(strUsername))
        context.Response.Write("用户名已注册！");
      else
        context.Response.Write("OK");
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