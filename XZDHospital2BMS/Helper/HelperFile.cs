using System;
using System.IO;

namespace Helper
{

  public class HelperFile
  {
    public HelperFile()
    {
    }

    /// <summary>
    /// 拷贝文件
    /// </summary>
    /// <param name="strSourceFileFullPath">原文件完整路径</param>
    /// <param name="strTargetFileFullPath">目标文件完整路径</param>
    /// <param name="boolReWrite">是否覆盖</param>
    public static void CopyFile(string strSourceFileFullPath, string strTargetFileFullPath, bool boolReWrite)
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

  }

}