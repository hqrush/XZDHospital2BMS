using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XZDHospital2BMS.BackManager.handler
{

  public class CheckAdminHandler : IHttpHandler
  {

    public void ProcessRequest(HttpContext context)
    {
      context.Response.ContentType = "text/plain";
      context.Response.Write("Hello World");
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