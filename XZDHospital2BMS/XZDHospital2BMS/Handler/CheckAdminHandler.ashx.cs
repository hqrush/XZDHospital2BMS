using Bll;
using Helper;
using System.Web;

namespace XZDHospital2BMS.Handler
{

  public class CheckAdminHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
  {

    public void ProcessRequest(HttpContext context)
    {
      context.Response.ContentType = "text/plain";

      // 在 Handler 里获取 session 要实现 System.Web.SessionState.IRequiresSessionState 接口
      int intAdminID = HelperUtility.hasPurviewPage("");
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