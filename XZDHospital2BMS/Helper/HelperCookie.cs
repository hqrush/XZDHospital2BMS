using System;
using System.Web;

namespace Helper
{

  public class HelperCookie
  {

    public HelperCookie()
    {
    }

    private const string CRYPTO_TYPE = "DES";

    // ==============================设置Cookie（有4个重载方法）

    /// <summary>
    /// 设置 Name Value
    /// </summary>
    public static void setCookie(string strName, string strValue)
    {
      strName = HelperCrypto.encode(strName, CRYPTO_TYPE);
      strValue = HelperCrypto.encode(strValue, CRYPTO_TYPE);
      HttpCookie objCK = HttpContext.Current.Request.Cookies[strName];
      if (objCK == null)
      {
        objCK = new HttpCookie(strName);
      }
      objCK.Value = strValue;
      HttpContext.Current.Response.AppendCookie(objCK);
    }

    /// <summary>
    /// 设置 Name, Value, intExpires（以分钟为单位）
    /// </summary>
    /// <param name="strName"></param>
    /// <param name="strKey"></param>
    /// <param name="strValue"></param>
    /// <param name="intExpires"></param>
    public static void setCookie(string strName, string strValue, int intExpires)
    {
      strName = HelperCrypto.encode(strName, CRYPTO_TYPE);
      strValue = HelperCrypto.encode(strValue, CRYPTO_TYPE);

      HttpCookie objCK = HttpContext.Current.Request.Cookies[strName];
      if (objCK == null)
      {
        objCK = new HttpCookie(strName);
      }
      objCK.Value = strValue;
      objCK.Expires = DateTime.Now.AddMinutes(intExpires);
      HttpContext.Current.Response.AppendCookie(objCK);
    }

    /// <summary>
    /// 设置 Name, key, Value
    /// </summary>
    public static void setCookie(string strName, string strKey, string strValue)
    {
      strName = HelperCrypto.encode(strName, CRYPTO_TYPE);
      strKey = HelperCrypto.encode(strKey, CRYPTO_TYPE);
      strValue = HelperCrypto.encode(strValue, CRYPTO_TYPE);

      HttpCookie objCK = HttpContext.Current.Request.Cookies[strName];
      if (objCK == null)
      {
        objCK = new HttpCookie(strName);
      }
      objCK[strKey] = strValue;
      HttpContext.Current.Response.AppendCookie(objCK);
    }

    /// <summary>
    /// 设置 Name, key, Value, intExpires（以分钟为单位）
    /// </summary>
    /// <param name="strName"></param>
    /// <param name="strKey"></param>
    /// <param name="strValue"></param>
    /// <param name="intExpires"></param>
    public static void setCookie(string strName, string strKey, string strValue, int intExpires)
    {
      strName = HelperCrypto.encode(strName, CRYPTO_TYPE);
      strKey = HelperCrypto.encode(strKey, CRYPTO_TYPE);
      strValue = HelperCrypto.encode(strValue, CRYPTO_TYPE);

      HttpCookie objCK = HttpContext.Current.Request.Cookies[strName];
      if (objCK == null)
      {
        objCK = new HttpCookie(strName);
      }
      objCK[strKey] = strValue;
      objCK.Expires = DateTime.Now.AddMinutes(intExpires);
      HttpContext.Current.Response.AppendCookie(objCK);
    }

    // ==============================读取Cookie

    // 根据 name 读取 value
    public static string getCookie(string strName)
    {
      string strReturn = "";
      strName = HelperCrypto.encode(strName, CRYPTO_TYPE);
      if (HttpContext.Current.Request.Cookies != null &&
        HttpContext.Current.Request.Cookies[strName] != null)
      {
        strReturn = HttpContext.Current.Request.Cookies[strName].Value.ToString();
        strReturn = HelperCrypto.decode(strReturn, CRYPTO_TYPE);
      }
      return strReturn;
    }

    // 根据 name 和 key 读取 value
    public static string getCookie(string strName, string strKey)
    {
      string strReturn = "";
      strName = HelperCrypto.encode(strName, CRYPTO_TYPE);
      strKey = HelperCrypto.encode(strKey, CRYPTO_TYPE);
      if (HttpContext.Current.Request.Cookies != null &&
        HttpContext.Current.Request.Cookies[strName] != null &&
        HttpContext.Current.Request.Cookies[strName][strKey] != null)
      {
        strReturn = HttpContext.Current.Request.Cookies[strName][strKey].ToString();
        strReturn = HelperCrypto.decode(strReturn, CRYPTO_TYPE);
      }
      return strReturn;
    }

    // ==============================清除cookie

    // 根据 name 清除 value
    public static void removeCookie(string strName)
    {
      setCookie(strName, "", "", -1);
    }

    // 根据 name 和 key 清除 value
    public static void removeCookie(string strName, string strKey)
    {
      setCookie(strName, strKey, "", -1);
    }

  }
}
