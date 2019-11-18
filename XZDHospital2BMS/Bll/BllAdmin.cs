using Dal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bll
{
  public class BllAdmin
  {

    public static int login(string strUsername, string strPassword)
    {
      return DalAdmin.login(strUsername, strPassword);
    }

  }
}
