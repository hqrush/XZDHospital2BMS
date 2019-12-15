using System;
using System.IO;
using System.Text;
using System.Web;

namespace Helper
{

  public class HelperFile
  {

    private static string strRootPath = HttpContext.Current.Request.PhysicalApplicationPath;

    public HelperFile()
    {
    }

    /// <summary>
    /// 拷贝文件
    /// </summary>
    /// <param name="strSourceFileFullPath">原文件完整路径</param>
    /// <param name="strTargetFileFullPath">目标文件完整路径</param>
    /// <param name="boolReWrite">是否覆盖</param>
    public static void CopyFile(string strSourceFileFullPath,
      string strTargetFileFullPath,
      bool boolReWrite)
    {
      string strTargetDir = strTargetFileFullPath.Substring(0, strTargetFileFullPath.LastIndexOf('\\'));
      CreateDirectory(strTargetDir);
      try
      {
        File.Copy(strSourceFileFullPath, strTargetFileFullPath, boolReWrite);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>
    /// 根据完整目录路径名称创建目录
    /// </summary>
    public static void CreateDirectory(string strFullPath)
    {
      try
      {
        if (!Directory.Exists(strFullPath))
          Directory.CreateDirectory(strFullPath);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>
    /// 根据文件的包含路径的完整名称删除文件
    /// </summary>
    public static void DeleteFile(string strFilePath)
    {
      try
      {
        if (File.Exists(strFilePath))
          File.Delete(strFilePath);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    ///  <summary> 
    /// 获取指定驱动器的空间总大小(单位为B) 
    ///  </summary> 
    ///  <param name="strHardDiskName">只需输入代表驱动器的字母即可</param> 
    ///  <returns>空间总大小(单位为B)</returns> 
    public static long GetHardDiskSpace(string strHardDiskName)
    {
      long lngTotalSize = new long();
      strHardDiskName = strHardDiskName + ":\\";
      DriveInfo[] aryDrives = DriveInfo.GetDrives();
      foreach (DriveInfo objDrive in aryDrives)
      {
        if (objDrive.Name == strHardDiskName)
        {
          lngTotalSize = objDrive.TotalSize / (1024 * 1024 * 1024);
        }
      }
      return lngTotalSize;
    }

    ///  <summary> 
    /// 获取指定驱动器的剩余空间总大小(单位为B) 
    ///  </summary> 
    ///  <param name="strHardDiskName">只需输入代表驱动器的字母即可</param> 
    ///  <returns>剩余空间大小(单位为B)</returns>
    public static long GetHardDiskFreeSpace(string strHardDiskName)
    {
      long lngFreeSpace = new long();
      strHardDiskName = strHardDiskName + ":\\";
      DriveInfo[] aryDrives = DriveInfo.GetDrives();
      foreach (DriveInfo objDrive in aryDrives)
      {
        if (objDrive.Name == strHardDiskName)
        {
          lngFreeSpace = objDrive.TotalFreeSpace / (1024 * 1024 * 1024);
        }
      }
      return lngFreeSpace;
    }

    /// <summary>
    /// 根据相对网站根目录的txt文件路径，读取此文件数据并存入 string 中
    /// </summary>
    /// <param name="strFileName">相对网站根目录的txt文件路径</param>
    /// <returns>存放txt文件内容的string</returns>
    public static string ReadTxt(string strFileName)
    {
      string strReturn = "";
      Encoding objEncoding = Encoding.GetEncoding("utf-8");
      string strFullFileName = strRootPath + strFileName;
      using (FileStream objFS = new FileStream(strFullFileName, FileMode.OpenOrCreate, FileAccess.Read))
      {
        using (StreamReader objSR = new StreamReader(objFS, objEncoding))
        {
          objSR.BaseStream.Seek(0, SeekOrigin.Begin);
          strReturn = objSR.ReadToEnd();
        }
      }
      return strReturn;
    }

    /// <summary>
    /// 根据相对网站根目录的txt文件路径，将string格式内容写入此文件中
    /// </summary>
    /// <param name="strContent">文本内容</param>
    /// <param name="strFileName">相对网站根目录的txt文件路径</param>
    public static void WriteTxt(string strContent, string strFileName)
    {
      Encoding objEncoding = Encoding.GetEncoding("utf-8");
      string strFullFileName = strRootPath + strFileName;
      using (FileStream objFS = new FileStream(strFullFileName, FileMode.OpenOrCreate))
      {
        using (StreamWriter objSW = new StreamWriter(objFS, objEncoding))
        {
          objSW.Write(strContent);
          objSW.Flush();
        }
      }
    }

  }

}