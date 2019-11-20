using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace Helper
{

  public class HelperUtility
  {
    public HelperUtility()
    {
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

    // 删除字符串数组中的重复值
    public static string[] delArrayRepeatedValue(string[] arySource)
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
    public static int GetRandom(int intValueMin, int intValueMax)
    {
      //这样产生0 ~ 100的强随机数（不含100）
      int intM = intValueMax - intValueMin;
      int intRnd = int.MinValue;
      decimal dcmBase = long.MaxValue;
      byte[] aryRndSeries = new byte[8];
      System.Security.Cryptography.RNGCryptoServiceProvider objRNG = new System.Security.Cryptography.RNGCryptoServiceProvider();
      objRNG.GetBytes(aryRndSeries);
      long lngL = BitConverter.ToInt64(aryRndSeries, 0);
      intRnd = (int)(Math.Abs(lngL) / dcmBase * intM);
      return intValueMin + intRnd;
    }

    /// <summary>
    /// 清除数组里的空字符串
    /// </summary>
    /// <param name="aryRows"></param>
    /// <returns></returns>
    public static string[] ClearBlankRow(string[] aryRows)
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

  }

}