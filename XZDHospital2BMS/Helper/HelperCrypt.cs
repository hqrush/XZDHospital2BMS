using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Helper
{

  public class HelperCrypto
  {

    // 8位加密密钥
    private static string strDESKey = "RU#h!0@5";

    /// <summary>
    /// 加密
    /// </summary>
    public static string encode(string strSource, string strCrypto)
    {
      if ("DES".Equals(strCrypto))
        return encodeDES(strSource, strDESKey);
      else if ("AES".Equals(strCrypto))
        return encodeAES(strSource);
      else
        return encodeDES(strSource, strDESKey);
    }

    /// <summary>
    /// 解密
    /// </summary>
    public static string decode(string strSource, string strCrypto)
    {
      if ("DES".Equals(strCrypto))
        return decodeDES(strSource, strDESKey);
      else if ("AES".Equals(strCrypto))
        return decodeAES(strSource);
      else
        return decodeDES(strSource, strDESKey);
    }

    // 下面是DES加解密算法

    /// <summary>
    /// DES加密算法，strKey为8位或16位
    /// </summary>
    /// <param name="strSource">需要加密的字符串</param>
    /// <param name="strKey">密钥</param>
    /// <returns></returns>
    private static string encodeDES(string strSource, string strKey)
    {
      StringBuilder sbResult = new StringBuilder();
      try
      {
        DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
        byte[] aryByteInput = Encoding.Default.GetBytes(strSource);
        objDES.Key = ASCIIEncoding.ASCII.GetBytes(strKey);
        objDES.IV = ASCIIEncoding.ASCII.GetBytes(strKey);
        using (MemoryStream objMS = new MemoryStream())
        {
          using (CryptoStream objCS = new CryptoStream(objMS, objDES.CreateEncryptor(), CryptoStreamMode.Write))
          {
            objCS.Write(aryByteInput, 0, aryByteInput.Length);
            objCS.FlushFinalBlock();
            foreach (byte b in objMS.ToArray()) sbResult.AppendFormat("{0:X2}", b);
            sbResult.ToString();
          }
        }
      }
      catch { }
      return sbResult.ToString();
    }

    /// <summary>
    /// DES解密算法，strKey为8位或16位
    /// </summary>
    /// <param name="strSource">需要解密的字符串</param>
    /// <param name="strKey">密钥</param>
    /// <returns></returns>
    private static string decodeDES(string strSource, string strKey)
    {
      try
      {
        DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
        byte[] aryInputByte = new byte[strSource.Length / 2];
        for (int x = 0; x < strSource.Length / 2; x++)
        {
          int i = (Convert.ToInt32(strSource.Substring(x * 2, 2), 16));
          aryInputByte[x] = (byte)i;
        }
        objDES.Key = ASCIIEncoding.ASCII.GetBytes(strKey);
        objDES.IV = ASCIIEncoding.ASCII.GetBytes(strKey);

        byte[] aryResult = null;
        using (MemoryStream objMS = new MemoryStream())
        {
          using (CryptoStream objCS = new CryptoStream(objMS, objDES.CreateDecryptor(), CryptoStreamMode.Write))
          {
            objCS.Write(aryInputByte, 0, aryInputByte.Length);
            objCS.FlushFinalBlock();
          }
          aryResult = objMS.ToArray();
        }
        return System.Text.Encoding.Default.GetString(aryResult);
      }
      catch
      {
      }
      return "";
    }

    // 下面是AES加解密算法

    private static SymmetricAlgorithm objSA = new RijndaelManaged();

    /// <summary>  
    /// 获得密钥  
    /// </summary>  
    /// <returns>密钥</returns>
    private static byte[] getLegalKey()
    {
      string strTemp = "Guz(%_&&91O_29H$y_uBI10_25Ftm_aT5&f_vHUFC_y76*h_%(RuS_h$lhj_!y6&(_*jkP8_7jH76";
      objSA.GenerateKey();
      byte[] aryByte = objSA.Key;
      int intKeyLength = aryByte.Length;
      if (strTemp.Length > intKeyLength)
        strTemp = strTemp.Substring(0, intKeyLength);
      else if (strTemp.Length < intKeyLength)
        strTemp = strTemp.PadRight(intKeyLength, ' ');
      return ASCIIEncoding.ASCII.GetBytes(strTemp);
    }

    /// <summary>  
    /// 获得初始向量IV
    /// </summary>
    /// <returns>初试向量IV</returns>  
    private static byte[] getLegalIV()
    {
      // key 随便设置一个Key
      string strTemp = "acle&_&(!)@_$jgjd_jfasr_weojk_sdf$%_#@!^!_@&#*a_jke^!_@#jka";
      objSA.GenerateIV();
      byte[] aryByte = objSA.IV;
      int intIVLength = aryByte.Length;
      if (strTemp.Length > intIVLength)
        strTemp = strTemp.Substring(0, intIVLength);
      else if (strTemp.Length < intIVLength)
        strTemp = strTemp.PadRight(intIVLength, ' ');
      return ASCIIEncoding.ASCII.GetBytes(strTemp);
    }

    /// <summary>  
    /// AES加密方法  
    /// </summary>  
    /// <param name="strSource">待加密的串</param>  
    /// <returns>经过加密的串</returns>  
    private static string encodeAES(string strSource)
    {
      byte[] arySourceByte = UTF8Encoding.UTF8.GetBytes(strSource);
      byte[] aryByteResult = null;
      using (MemoryStream objMS = new MemoryStream())
      {
        objSA.Key = getLegalKey();
        objSA.IV = getLegalIV();
        ICryptoTransform objICTrans = objSA.CreateEncryptor();
        using (CryptoStream objCS = new CryptoStream(objMS, objICTrans, CryptoStreamMode.Write))
        {
          objCS.Write(arySourceByte, 0, arySourceByte.Length);
          objCS.FlushFinalBlock();
          aryByteResult = objMS.ToArray();
        }
      }
      return Convert.ToBase64String(aryByteResult);
    }

    /// <summary>  
    /// AES解密方法  
    /// </summary>  
    /// <param name="strSource">待解密的串</param>  
    /// <returns>经过解密的串</returns>  
    private static string decodeAES(string strSource)
    {
      string strResult = "";
      byte[] arySourceByte = Convert.FromBase64String(strSource);
      using (MemoryStream objMS = new MemoryStream(arySourceByte, 0, arySourceByte.Length))
      {
        objSA.Key = getLegalKey();
        objSA.IV = getLegalIV();
        ICryptoTransform objICTrans = objSA.CreateDecryptor();
        using (CryptoStream objCS = new CryptoStream(objMS, objICTrans, CryptoStreamMode.Read))
        {
          StreamReader objSR = new StreamReader(objCS);
          strResult = objSR.ReadToEnd();
        }
      }
      return strResult;
    }

  }

}
