using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace Helper
{

  public class HelperUtility
  {

    private static char[] chrConstant = {
        '0','1','2','3','4','5','6','7','8','9',
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n',
      'o','p','q','r','s','t','u','v','w','x','y','z',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N',
      'O','P','Q','R','S','T','U','V','W','X','Y','Z'
    };

    private const string PAGE_LOGIN = "~/BackManager/login.aspx";

    public HelperUtility()
    {
    }

    /// <summary>
    /// 检查登录用户是否有访问某个页面的权限，如果无权限则跳到登录页面
    /// </summary>
    /// <param name="strPurviewsNeed">访问该页面需要的权限</param>
    /// <returns>返回登录用户的AdminId</returns>
    public static int hasPurviewPage(string strPurviewsNeed)
    {
      int intAdminId = checkIsLogin();
      // 下面开始验证权限
      if (HttpContext.Current.Session["Purviews"] == null)
        HttpContext.Current.Response.Redirect(PAGE_LOGIN);
      string strPurviewsSession = HttpContext.Current.Session["Purviews"].ToString();
      if ("".Equals(strPurviewsSession))
        HttpContext.Current.Response.Redirect(PAGE_LOGIN);
      List<string> listPurviewsSession = new List<string>(strPurviewsSession.Split(','));
      // 如果是 SUPERADMIN 或者该页面不需要权限即可访问，就直接结束验证
      if (listPurviewsSession.Contains("SUPERADMIN") || "".Equals(strPurviewsNeed))
        return intAdminId;
      // 验证 session 里的用户所拥有的权限是否包含该页面所需要的权限
      List<string> listPurviewsNeed = new List<string>(strPurviewsNeed.Split(','));
      for (int i = 0; i < listPurviewsNeed.Count; i++)
      {
        // 遍历登录后得到的权限和某页面或操作所需要的权限是否想等
        if (listPurviewsSession.Contains(listPurviewsNeed[i]))
          return intAdminId;
      }
      // 如果上面的验证循环没有返回，执行到这里了，说明没有权限匹配，那么跳转到登录页重新登录
      HttpContext.Current.Response.Redirect(PAGE_LOGIN);
      return 0;
    }

    /// <summary>
    /// 检查登录用户是否有执行某个方法的权限，如果无权限则返回false
    /// </summary>
    /// <param name="strPurviewsNeed">访问该方法需要的权限</param>
    /// <returns>返回登录用户的AdminId</returns>
    public static bool hasPurviewOP(string strPurviewsNeed)
    {
      // 先检查是否有Session["Purviews"]，没有就说明丢失了session，要重新登录
      if (HttpContext.Current.Session["Purviews"] == null)
        HttpContext.Current.Response.Redirect(PAGE_LOGIN);
      string strPurviewsSession = HttpContext.Current.Session["Purviews"].ToString();
      if ("".Equals(strPurviewsSession))
        HttpContext.Current.Response.Redirect(PAGE_LOGIN);
      List<string> listPurviewsSession = new List<string>(strPurviewsSession.Split(','));
      // 如果是 SUPERADMIN 或者该页面不需要权限即可访问，就直接结束验证
      if (listPurviewsSession.Contains("SUPERADMIN") || "".Equals(strPurviewsNeed))
        return true;
      // 验证 session 里的用户所拥有的权限是否包含有允许执行该操作的权限
      List<string> listPurviewsNeed = new List<string>(strPurviewsNeed.Split(','));
      for (int i = 0; i < listPurviewsNeed.Count; i++)
      {
        if (listPurviewsSession.Contains(listPurviewsNeed[i]))
          return true;
      }
      return false;
    }

    private static int checkIsLogin()
    {
      // 根据 session 中是否有 AdminId 来判断是否登录，如果没有则重新登录
      if (HttpContext.Current.Session["AdminId"] == null)
        HttpContext.Current.Response.Redirect(PAGE_LOGIN);
      int intAdminId = Convert.ToInt32(HttpContext.Current.Session["AdminId"]);
      if (intAdminId <= 0) HttpContext.Current.Response.Redirect(PAGE_LOGIN);
      return intAdminId;
    }

    public static void showAlert(string strMsg, string strUrl)
    {
      string strScript = "<script>";
      if (!"".Equals(strMsg) && !"".Equals(strUrl))
      {
        strScript += "alert('" + strMsg + "');";
        strScript += "location='" + strUrl + "';</script>";
      }
      else if (!"".Equals(strMsg) && "".Equals(strUrl))
        strScript += "alert('" + strMsg + "');</script>";
      else if ("".Equals(strMsg) && !"".Equals(strUrl))
        strScript += "location='" + strUrl + "';</script>";
      else return;
      HttpContext.Current.Response.Write(strScript);
    }

    public static string getQueryString(string strKey)
    {
      if (HttpContext.Current.Request.QueryString[strKey] == null ||
          "".Equals(HttpContext.Current.Request.QueryString[strKey].ToString()))
        return "";
      else
        return HttpContext.Current.Request.QueryString[strKey];
    }

    public static int getQueryInt(string strKey)
    {
      if (HttpContext.Current.Request.QueryString[strKey] == null ||
          "".Equals(HttpContext.Current.Request.QueryString[strKey].ToString()))
        return 0;
      try
      {
        int intReturn = Convert.ToInt32(HttpContext.Current.Request.QueryString[strKey]);
        if (intReturn > 0) return intReturn;
        else return 0;
      }
      catch
      {
        return 0;
      }
    }

    // 根据时间下拉列表设置起始时间
    public static string[] getDateByDDL(DropDownList ddlYear, DropDownList ddlMonth)
    {
      string[] aryDate = new string[] { "2017", "5" };
      int intYear = Convert.ToInt32(ddlYear.SelectedValue);
      int intMonth = Convert.ToInt32(ddlMonth.SelectedValue);
      string strDateStart = intYear.ToString() + "/" + intMonth.ToString() + "/1 00:00:00";
      string strDateEnd;
      if (intMonth == 12)
      {
        strDateEnd = (intYear + 1).ToString() + "/1/1 00:00:00";
      }
      else
      {
        strDateEnd = intYear.ToString() + "/" + (intMonth + 1).ToString() + "/1 00:00:00";
      }
      aryDate[0] = strDateStart;
      aryDate[1] = strDateEnd;
      return aryDate;
    }

    /// <summary>
    /// 时间戳格式转成DateTime格式
    /// </summary>
    /// <param name="strTimeStamp"></param>
    /// <returns></returns>
    public static DateTime convertInt2DateTime(string strTimeStamp)
    {
      DateTime dtTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
      long lngTime = long.Parse(strTimeStamp + "0000000");
      TimeSpan tsNow = new TimeSpan(lngTime);
      return dtTimeStart.Add(tsNow);
    }

    /// <summary>
    /// DateTime格式转成时间戳格式
    /// </summary>
    /// <param name="dtTime"></param>
    /// <returns></returns>
    public static long convertDateTime2Int(DateTime dtTime)
    {
      //double intResult = 0;
      DateTime dtTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
      //intResult = (dtTime- startTime).TotalMilliseconds;
      //除10000调整为10位
      long longTime = (dtTime.Ticks - dtTimeStart.Ticks) / 10000000;
      return longTime;
    }

    // 两表合并方法有两个：一是表1加到新表中，然后再加表2，二是先判断哪个表的行数多，就先添加哪个表，然后再添加行少的表
    public static DataTable merge2TableNotSame(DataTable dt1, DataTable dt2)
    {
      //克隆DataTable1的结构
      DataTable dtNew = dt1.Clone();
      //再向新表中加入DataTable2的列结构
      for (int i = 0; i < dt2.Columns.Count; i++)
      {
        dtNew.Columns.Add(dt2.Columns[i].ColumnName);
      }
      // 新建一个数组，长度是新表的总列数
      object[] obj = new object[dtNew.Columns.Count];
      // 添加DataTable1的数据
      for (int i = 0; i < dt1.Rows.Count; i++)
      {
        // 这里添加数据的方法是将每行数据存到一个数组里，然后新表的每行直接添加这个数组就可以了
        dt1.Rows[i].ItemArray.CopyTo(obj, 0);
        dtNew.Rows.Add(obj);
      }
      // 添加DataTable2的数据
      for (int i = 0; i < dt2.Rows.Count; i++)
      {
        // 循环读取表二每行每列的值
        for (int j = 0; j < dt2.Columns.Count; j++)
        {
          // 新表从第一行的第二个表的第一列名称开始改写数据
          dtNew.Rows[i][j + dt1.Columns.Count] = dt2.Rows[i][j].ToString();
        }
      }
      return dtNew;
    }

    public static DataTable creatBigTable(DataTable dtWXCount, DataTable dtWXScore)
    {
      // 创建一个合并数据的新表
      DataTable dtNew = dtWXScore.Clone();
      dtNew.Columns.Remove("ifcheck");
      dtNew.Columns.Remove("send_date");
      dtNew.Columns.Remove("writer_name");
      //再向新表中加入dtWXScore的列结构，在添加之前，先去除一些不需要的列
      DataTable dtTemp = dtWXCount.Clone();
      dtTemp.Columns.Remove("ifcheck");
      dtTemp.Columns.Remove("countdate");
      dtTemp.Columns.Remove("publish_date_type");
      for (int i = 0; i < dtTemp.Columns.Count; i++)
      {
        dtNew.Columns.Add(dtTemp.Columns[i].ColumnName);
      }
      return dtNew;
      //ifcheck,
      //article_id, send_date, score_text,
      //score_pic, score_video, score_typeset,
      //score_read_num, score_good, score_leader1_good,
      //score_leader2_good, writer_name

      //ifcheck,
      //title,senddate,sort_in,
      //writer_text,writer_pic,writer_video,
      //editor_typeset,editor_publish,count_read,
      //count_good,is_leader1_good,is_leader2_good,
      //countdate,publish_date_type,content_url,
      //content_type
    }

    // 计算两个日期的时间差
    public static TimeSpan countDateDiff(DateTime dtStart, DateTime dtEnd)
    {
      dtStart = Convert.ToDateTime(dtStart.ToShortDateString());
      dtEnd = Convert.ToDateTime(dtEnd.ToShortDateString());
      TimeSpan tsReturn = dtEnd.Subtract(dtStart);
      return tsReturn;
    }

    /// <summary>
    /// 生成指定范围内的随机整数
    /// </summary>
    /// <param name="intValueMin">最小值（包含）</param>
    /// <param name="intValueMax">最大值（不包含）</param>
    /// <returns></returns>
    public static int getRandom(int intValueMin, int intValueMax)
    {
      //这样产生0 ~ 100的强随机数（不含100）
      int intM = intValueMax - intValueMin;
      int intRnd = int.MinValue;
      decimal dcmBase = long.MaxValue;
      byte[] aryRndSeries = new byte[8];
      System.Security.Cryptography.RNGCryptoServiceProvider objRNG =
        new System.Security.Cryptography.RNGCryptoServiceProvider();
      objRNG.GetBytes(aryRndSeries);
      long lngL = BitConverter.ToInt64(aryRndSeries, 0);
      intRnd = (int)(Math.Abs(lngL) / dcmBase * intM);
      return intValueMin + intRnd;
    }

    /// <summary>
    /// 产生随机的颜色
    /// </summary>
    public static Color SetRandomColor()
    {
      Random objRandom = new Random(Convert.ToInt32(Guid.NewGuid().GetHashCode()));
      Color objColor = Color.FromArgb(255,
        objRandom.Next(0, 100),
        objRandom.Next(0, 100),
        objRandom.Next(0, 100));
      return objColor;
    }

    /// <summary>
    /// 根据指定长度产生随机的字符串，可用来随机名
    /// </summary>
    public static string getRandomNumber(int Length)
    {
      StringBuilder sbRandom = new StringBuilder(62);
      Random objRandom = new Random();
      for (int i = 0; i < Length; i++)
      {
        sbRandom.Append(chrConstant[objRandom.Next(62)]);
      }
      return sbRandom.ToString();
    }

    /// <summary>
    /// 清除数组里的空字符串
    /// </summary>
    /// <param name="aryRows"></param>
    /// <returns></returns>
    public static string[] removeArrayBlankRow(string[] aryRows)
    {
      List<string> list = new List<string>();
      foreach (string s in aryRows)
      {
        if (!string.IsNullOrEmpty(s))
        {
          list.Add(s);
        }
      }
      return list.ToArray();
    }

    // 删除字符串数组中的重复值
    public static string[] removeArrayRepeatedValue(string[] arySource)
    {
      ArrayList objAL = new ArrayList();
      for (int i = 0; i < arySource.Length; i++)
      {
        //判断数组值是否已经存在 
        if (objAL.Contains(arySource[i]) == false && arySource[i] != "")
        {
          objAL.Add(arySource[i]);
        }
      }
      //把ArrayList转换数组 
      //aryResult = new string[objAL.Count];
      //aryResult = (string[])objAL.ToArray(typeof(string));
      return (string[])objAL.ToArray(typeof(string));
    }

    public static string removeStringSpace(string strSource)
    {
      // string myString = "  this\n is\r a \ttest    ";
      return Regex.Replace(strSource, @"\s", "");
    }

    // 验证手机号码
    public static bool isMobilePhone(string strMobilePhone)
    {
      return System.Text.RegularExpressions.Regex.IsMatch(strMobilePhone, @"^[1]+[3,5]+\d{9}");
    }

    // 验证身份证号
    public static bool isIdCard(string strIdCard)
    {
      return System.Text.RegularExpressions.Regex.IsMatch(strIdCard, @"(^\d{18}$)|(^\d{15}$)");
    }

    // 验证是否是日期类型
    public static bool isDateType(string strDate)
    {
      try
      {
        DateTime dt = Convert.ToDateTime(strDate);
        return true;
      }
      catch
      {
        return false;
      }
    }

    // 验证是否是Int数字类型
    public static bool isInt(string strSource)
    {
      try
      {
        int intReturn = Convert.ToInt32(strSource);
        return true;
      }
      catch
      {
        return false;
      }
    }

    // 验证是否是decimal数字类型
    public static bool isDecimal(string strSource)
    {
      try
      {
        decimal dcmReturn = Convert.ToDecimal(strSource);
        return true;
      }
      catch
      {
        return false;
      }
    }

    public static string setReturnJson(string strCode, string strMsg, string strData)
    {
      ModelReturnJson model = new ModelReturnJson();
      model.StatusCode = strCode;
      model.Message = strMsg;
      model.Data = strData;
      return model.ToString();
    }

  }

}