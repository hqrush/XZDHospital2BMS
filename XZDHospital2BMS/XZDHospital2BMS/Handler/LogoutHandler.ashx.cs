using Helper;
using System.Web;

namespace XZDHospital2BMS.Handler
{

  public class LogoutHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
  {

    public void ProcessRequest(HttpContext context)
    {
      // 从会话状态集合中移除所有的键和值
      context.Session.Clear();
      // 取消当前会话
      context.Session.Abandon();
      // 清除cookie里的登录信息
      HelperCookie.removeCookie("AdminLogin");
      context.Response.Redirect("/BackManager/login.aspx");  //跳转登录页
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