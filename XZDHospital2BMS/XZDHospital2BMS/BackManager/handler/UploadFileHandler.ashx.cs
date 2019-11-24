using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace XZDHospital2BMS.BackManager.handler
{

  /// <summary>
  /// UploadFileHandler 的摘要说明
  /// </summary>
  public class UploadFileHandler : IHttpHandler
  {

    // 上传目录
    const string path = "/UploadFile/";
    //上传临时目录
    const string tempPath = "/UploadFile/temp/";

    public void ProcessRequest(HttpContext context)
    {
      context.Response.ContentType = "text/plain";
      //如果进行了分片
      if (context.Request.Form.AllKeys.Any(m => m == "chunk"))
      {
        //取得chunk和chunks
        //当前分片在上传分片中的顺序（从0开始）
        int chunk = Convert.ToInt32(context.Request.Form["chunk"]);
        //总分片数
        int chunks = Convert.ToInt32(context.Request.Form["chunks"]);
        if (string.IsNullOrEmpty(context.Request["guid"]))
          throw new Exception("[guid]不能为空");
        //根据GUID创建用该GUID命名的临时文件夹
        string folder = context.Server.MapPath(tempPath + context.Request["guid"] + "/");
        string tempFile = folder + chunk;
        //建立临时传输文件夹
        if (!Directory.Exists(Path.GetDirectoryName(folder)))
          Directory.CreateDirectory(folder);
        FileStream addFile = new FileStream(tempFile, FileMode.Append, FileAccess.Write);
        BinaryWriter AddWriter = new BinaryWriter(addFile);
        //获得上传的分片数据流
        HttpPostedFile file = context.Request.Files[0];
        Stream stream = file.InputStream;
        BinaryReader TempReader = new BinaryReader(stream);
        //将上传的分片追加到临时文件末尾
        AddWriter.Write(TempReader.ReadBytes((int)stream.Length));
        //关闭BinaryReader文件阅读器
        TempReader.Close();
        stream.Close();
        AddWriter.Close();
        addFile.Close();
        TempReader.Dispose();
        stream.Dispose();
        AddWriter.Dispose();
        addFile.Dispose();
        string filePath = "";
        if (chunk == chunks - 1)
        {
          //合并后的文件
          filePath = path + DateTime.Now.ToString("yyyy/MM/") +
            Guid.NewGuid() + Path.GetExtension(file.FileName);
          ProcessRequest(folder, context.Server.MapPath(filePath));
        }
        context.Response.Write("{\"chunked\" : true, \"hasError\" : false, \"filePath\" : \"" + filePath + "\"}");
      }
      else
      {
        //没有分片，直接保存
        //合并后的文件
        string filePath = path + DateTime.Now.ToString("yyyy/MM/") +
          Guid.NewGuid() + Path.GetExtension(context.Request.Files[0].FileName);
        context.Request.Files[0].SaveAs(context.Server.MapPath(filePath));
        context.Response.Write("{\"chunked\" : true, \"hasError\" : false, \"filePath\" : \"" + filePath + "\"}");
      }
    }

    /// <summary>
    /// 合并文件
    /// </summary>
    /// <param name="sourcePath">源数据文件夹</param>
    /// <param name="filePath">合并后的文件</param>
    private void ProcessRequest(string sourcePath, string filePath)
    {
      if (!Directory.Exists(Path.GetDirectoryName(filePath)))
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
      DirectoryInfo dicInfo = new DirectoryInfo(sourcePath);
      if (Directory.Exists(Path.GetDirectoryName(sourcePath)))
      {
        FileInfo[] files = dicInfo.GetFiles();
        foreach (FileInfo file in files.OrderBy(f => int.Parse(f.Name)))
        {
          FileStream addFile = new FileStream(filePath, FileMode.Append, FileAccess.Write);
          BinaryWriter AddWriter = new BinaryWriter(addFile);
          //获得上传的分片数据流
          Stream stream = file.Open(FileMode.Open);
          BinaryReader TempReader = new BinaryReader(stream);
          //将上传的分片追加到临时文件末尾
          AddWriter.Write(TempReader.ReadBytes((int)stream.Length));
          //关闭BinaryReader文件阅读器
          TempReader.Close();
          stream.Close();
          AddWriter.Close();
          addFile.Close();
          TempReader.Dispose();
          stream.Dispose();
          AddWriter.Dispose();
          addFile.Dispose();
        }
        Directory.Delete(sourcePath, true);
      }
    }

    /// <summary>
    /// 删除文件夹
    /// </summary>
    /// <param name="strPath"></param>
    private static void DeleteFolder(string strPath)
    {
      if (Directory.GetDirectories(strPath).Length > 0)
        foreach (string fl in Directory.GetDirectories(strPath)) Directory.Delete(fl, true);
      //删除这个目录下的所有文件
      if (Directory.GetFiles(strPath).Length > 0)
        foreach (string f in Directory.GetFiles(strPath)) System.IO.File.Delete(f);
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