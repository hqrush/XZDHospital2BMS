using System;
using System.IO;
using System.Drawing;
using System.Web;
using Model;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Helper
{

  #region 修改上传大小的配置
  /*
      需要修改的是
      在 C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\CONFIG目录里，
      找到文件maxRequestLength="4096"
      将值修改大一些，例如：102400
      这个参数的单位应该是KB的

      以上方法是修改全局的，如果需要修改一个项目，那么是修改项目里的Web.config文件

      在<system.web></system.web>之间添加，
      <httpRuntime useFullyQualifiedRedirectUrl="true" maxRequestLength="21000" executionTimeout="300" />
      其中，
      maxRequestLength：设置上传文件的最大值，单位：KB。（默认是4096KB，即4M）
      executionTimeout：设置超时时间，单位：秒。（默认是90秒）
      */
  #endregion

  public class HelperUploadFile
  {

    public HelperUploadFile()
    {
    }

    // 用.net的FileUpload控件上传单个文件
    public static ModelUploadFileConfig SaveULFileFromControl(System.Web.UI.WebControls.FileUpload objFU,
      ModelUploadFileConfig objConfig)
    {
      if (!objFU.HasFile)
      {
        objConfig.OPFlag = false;
        objConfig.OPMessage = "指定文件不存在";
        return objConfig;
      }
      string strULFileName = objFU.PostedFile.FileName;
      string strULFileType = strULFileName.Substring(strULFileName.LastIndexOf(".") + 1).ToLower();
      if (!CheckFileExt(strULFileType, objConfig))
      {
        objConfig.OPFlag = false;
        objConfig.OPMessage = "上传文件的格式不正确";
        return objConfig;
      }
      int intAllowSize = SetDefaultSize(strULFileType, objConfig);
      if (Math.Round(Convert.ToDouble(objFU.PostedFile.ContentLength / 1024), 2) > intAllowSize)
      {
        objConfig.OPFlag = false;
        objConfig.OPMessage = "系统允许您上传的文件类型的大小不能超过" + intAllowSize + "k";
        return objConfig;
      }
      if (objConfig.ThumbFlag)
      {
        if (!CheckImageFileExt(strULFileType, objConfig))
        {
          objConfig.OPFlag = false;
          objConfig.OPMessage = "不是图像文件，不能生成缩略图";
          return objConfig;
        }
      }
      if (objConfig.WaterMarkImageFlag)
      {
        if (!CheckImageFileExt(strULFileType, objConfig))
        {
          objConfig.OPFlag = false;
          objConfig.OPMessage = "不是图像文件，不能生成水印";
          return objConfig;
        }
      }
      //开始保存文件
      string strRootPath = HttpContext.Current.Request.PhysicalApplicationPath;
      string strServerFilePath = CreateDateDir(SetDefaultDir(strULFileType, objConfig)) +
                                     SetRnd14FileName() +
                                     "." + strULFileType;
      string strServerFileFullPath = strRootPath + strServerFilePath;
      try
      {
        objFU.PostedFile.SaveAs(strServerFileFullPath);
      }
      catch (Exception ex)
      {
        objConfig.OPFlag = false;
        objConfig.OPMessage = ex.Message;
        return objConfig;
      }
      //文件成功保存到服务器端
      objConfig.OPFlag = true;
      objConfig.ServerFilePath = strServerFilePath;
      objConfig.ServerFileFullPath = strServerFileFullPath;
      //如果是图片文件，可能有生成缩略图操作，或者加文字或图片水印操作
      if (CheckImageFileExt(strULFileType, objConfig))
      {
        // 生成缩略图
        if (objConfig.ThumbFlag)
          SetThumbImage(objConfig);
        // 加水印
        if (objConfig.WaterMarkTxtFlag)
          DrawTxtWaterMark(objConfig);
        else if (objConfig.WaterMarkImageFlag)
          DrawPicWaterMark(objConfig);
      }
      if (objConfig.OPFlag)
        objConfig.OPMessage = "上传成功";
      return objConfig;
    }

    public static ModelUploadFileConfig SaveULFileFromHPF(HttpPostedFile objHPF,
      ModelUploadFileConfig objConfig)
    {
      if (objHPF == null)
      {
        objConfig.OPFlag = false;
        objConfig.OPMessage = "文件不存在";
        return objConfig;
      }
      string strULFileName = objHPF.FileName;
      string strULFileType = strULFileName.Substring(strULFileName.LastIndexOf(".") + 1).ToLower();
      if (!CheckFileExt(strULFileType, objConfig))
      {
        objConfig.OPFlag = false;
        objConfig.OPMessage = "上传文件的格式不正确";
        return objConfig;
      }
      int intAllowSize = SetDefaultSize(strULFileType, objConfig);
      if (Math.Round(Convert.ToDouble(objHPF.ContentLength / 1024), 2) > intAllowSize)
      {
        objConfig.OPFlag = false;
        objConfig.OPMessage = "系统允许您上传的文件类型的大小不能超过" + intAllowSize + "k";
        return objConfig;
      }
      if (objConfig.ThumbFlag)
      {
        if (!CheckImageFileExt(strULFileType, objConfig))
        {
          objConfig.OPFlag = false;
          objConfig.OPMessage = "不是图像文件，不能生成缩略图";
          return objConfig;
        }
      }
      if (objConfig.WaterMarkImageFlag)
      {
        if (!CheckImageFileExt(strULFileType, objConfig))
        {
          objConfig.OPFlag = false;
          objConfig.OPMessage = "不是图像文件，不能生成水印";
          return objConfig;
        }
      }
      //开始保存文件
      string strRootPath = HttpContext.Current.Request.PhysicalApplicationPath;
      string strServerFilePath = CreateDateDir(SetDefaultDir(strULFileType, objConfig)) +
                                     SetRnd14FileName() +
                                     "." + strULFileType;
      string strServerFileFullPath = strRootPath + strServerFilePath;
      try
      {
        objHPF.SaveAs(strServerFileFullPath);
      }
      catch (Exception ex)
      {
        objConfig.OPFlag = false;
        objConfig.OPMessage = ex.Message;
        return objConfig;
      }
      //文件成功保存到服务器端
      objConfig.OPFlag = true;
      objConfig.ServerFilePath = strServerFilePath;
      objConfig.ServerFileFullPath = strServerFileFullPath;
      // 判断是不是图片文件，如果是图片文件，可能有生成缩略图操作，或者加文字或图片水印操作
      if (CheckImageFileExt(strULFileType, objConfig))
      {
        // 生成缩略图
        if (objConfig.ThumbFlag)
          SetThumbImage(objConfig);
        // 加文字水印
        if (objConfig.WaterMarkTxtFlag)
          DrawTxtWaterMark(objConfig);
        // 加图片水印
        else if (objConfig.WaterMarkImageFlag)
          DrawPicWaterMark(objConfig);
      }
      if (objConfig.OPFlag)
        objConfig.OPMessage = "上传成功";
      return objConfig;
    }

    //检查上传的文件扩展名
    private static bool CheckFileExt(string strFileExt, ModelUploadFileConfig objConfig)
    {
      string[] aryAllowFileExt = objConfig.AllowUploadFileExt.Split(',');
      for (int i = 0; i <= aryAllowFileExt.Length - 1; i++)
      {
        if (strFileExt == aryAllowFileExt[i])
        {
          return true;
        }
      }
      return false;
    }

    //检查上传的图像文件扩展名
    private static bool CheckImageFileExt(string strFileExt, ModelUploadFileConfig objConfig)
    {
      string[] aryAllowFileExt = objConfig.AllowUploadImageFileExt.Split(',');
      for (int i = 0; i <= aryAllowFileExt.Length - 1; i++)
      {
        if (strFileExt == aryAllowFileExt[i])
        {
          return true;
        }
      }
      return false;
    }

    //根据文件扩展名设置要上传到哪个目录
    private static string SetDefaultDir(string strFileExt, ModelUploadFileConfig objConfig)
    {
      string strReturn = "";
      switch (strFileExt)
      {
        case "gif":
        case "jpg":
        case "jpeg":
        case "bmp":
        case "png":
        case "tiff":
        case "wmf":
        case "ico":
          strReturn = objConfig.UploadFileRootDir + "/image/";
          break;
        case "swf":
          strReturn = objConfig.UploadFileRootDir + "/flash/";
          break;
        case "xls":
        case "xlsx":
          strReturn = objConfig.UploadFileRootDir + "/excel/";
          break;
        case "txt":
        case "doc":
        case "docx":
        case "chm":
        case "pdf":
          strReturn = objConfig.UploadFileRootDir + "/document/";
          break;
        case "zip":
        case "rar":
          strReturn = objConfig.UploadFileRootDir + "/zip/";
          break;
        case "avi":
        case "mp3":
        case "mp4":
        case "wma":
        case "wav":
        case "flv":
          strReturn = objConfig.UploadFileRootDir + "/media/";
          break;
        default:
          strReturn = objConfig.UploadFileRootDir + "/document/";
          break;
      }
      return strReturn;
    }

    //设置上传文件的大小限制
    private static int SetDefaultSize(string strFileExt, ModelUploadFileConfig objConfig)
    {
      int intReturn = 0;
      switch (strFileExt)
      {
        case "gif":
        case "jpg":
        case "jpeg":
        case "bmp":
        case "png":
        case "tiff":
        case "wmf":
        case "ico":
          intReturn = objConfig.AllowUploadImageFileSize;
          break;
        case "swf":
          intReturn = objConfig.AllowUploadSwfFileSize;
          break;
        case "txt":
        case "doc":
        case "docx":
        case "xls":
        case "xlsx":
        case "chm":
        case "pdf":
          intReturn = objConfig.AllowUploadTxtFileSize;
          break;
        case "zip":
        case "rar":
          intReturn = objConfig.AllowUploadZipFileSize;
          break;
        case "avi":
        case "flv":
        case "mp3":
        case "mp4":
        case "wma":
        case "wav":
          intReturn = objConfig.AllowUploadMediaFileSize;
          break;
        default:
          intReturn = 10;
          break;
      }
      return intReturn;
    }

    //绘制文本水印
    private static void DrawTxtWaterMark(ModelUploadFileConfig objConfig)
    {
      if (!File.Exists(objConfig.ServerFileFullPath))
      {
        objConfig.OPFlag = false;
        objConfig.OPMessage = "文件不存在，加水印失败。";
        return;
      }
      Image objImg = Image.FromFile(objConfig.ServerFileFullPath);
      switch (objImg.PixelFormat)
      {
        case PixelFormat.Format1bppIndexed:
        case PixelFormat.Format4bppIndexed:
        case PixelFormat.Format8bppIndexed:
        case PixelFormat.Undefined:
        case PixelFormat.Format16bppArgb1555:
        case PixelFormat.Format16bppGrayScale:
          objConfig.OPFlag = false;
          objConfig.OPMessage = "上传的图片包含不正确的像素格式，不能加水印！";
          objImg.Dispose();
          return;
        default:
          break;
      }
      Graphics g = null;
      Font f = new Font("Verdana", 12f, FontStyle.Bold);
      Brush b = new SolidBrush(Color.Red);
      try
      {
        g = Graphics.FromImage(objImg);
        g.DrawImage(objImg, 0, 0, objImg.Width, objImg.Height);
        g.DrawString(objConfig.WaterMarkTxtString, f, b, Convert.ToSingle(objConfig.WaterMarkMarginRight), Convert.ToSingle(objConfig.WaterMarkMarginBottom));
        objConfig.ServerFileHasWMFullPath = RenameFile(objConfig.ServerFileFullPath);
        objImg.Save(objConfig.ServerFileHasWMFullPath);
        objConfig.OPFlag = true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        f.Dispose();
        b.Dispose();
        g.Dispose();
        objImg.Dispose();
        HelperFile.DeleteFile(objConfig.ServerFileHasWMFullPath);
      }
    }

    //绘制图片水印
    private static void DrawPicWaterMark(ModelUploadFileConfig objConfig)
    {
      if (!File.Exists(objConfig.ServerFileFullPath))
      {
        objConfig.OPFlag = false;
        objConfig.OPMessage = "图片文件不存在，加水印失败。";
        return;
      }
      string strRootPath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath;
      objConfig.WaterMarkImagePath = strRootPath + objConfig.WaterMarkImagePath;
      if (!File.Exists(objConfig.WaterMarkImagePath))
      {
        objConfig.OPFlag = false;
        objConfig.OPMessage = "水印图片文件不存在，加水印失败。";
        return;
      }
      //将服务器端要加水印的图像文件载入到Image对象里
      Image objImg = Image.FromFile(objConfig.ServerFileFullPath);
      switch (objImg.PixelFormat)
      {
        case PixelFormat.Format1bppIndexed:
        case PixelFormat.Format4bppIndexed:
        case PixelFormat.Format8bppIndexed:
        case PixelFormat.Undefined:
        case PixelFormat.Format16bppArgb1555:
        case PixelFormat.Format16bppGrayScale:
          objConfig.OPFlag = false;
          objConfig.OPMessage = "上传的图片包含不正确的像素格式，不能加水印。";
          objImg.Dispose();
          return;
      }
      //将水印图像文件载入到另一个Image对象里
      Image objWMImg = Image.FromFile(objConfig.WaterMarkImagePath);
      // 将保存了图像文件的Image对象存到一个Graphics对象里
      Graphics g = Graphics.FromImage(objImg);
      MemoryStream objMS = new MemoryStream();
      ImageAttributes imgAttr = new ImageAttributes();
      FileStream objFS = null;
      try
      {
        //获取要绘制图形坐标
        int x = objImg.Width - objConfig.WaterMarkMarginRight;
        int y = objImg.Height - objConfig.WaterMarkMarginBottom;
        //设置颜色矩阵
        float[][] matrixItems = {
          new float[] {
            1,
            0,
            0,
            0,
            0
          },
          new float[] {
            0,
            1,
            0,
            0,
            0
          },
          new float[] {
            0,
            0,
            1,
            0,
            0
          },
          new float[] {
            0,
            0,
            0,
            Convert.ToSingle(objConfig.WaterMarkTransparent) / 100f,
            0
          },
          new float[] {
            0,
            0,
            0,
            0,
            1
          }
        };
        ColorMatrix colorMatrix = new ColorMatrix(matrixItems);
        imgAttr.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        //在Graphics对象里绘制水印图像，此时，和这个Graphics对象相关联的服务器文件也跟着被画上了水印
        g.DrawImage(objWMImg, new Rectangle(x, y, objWMImg.Width, objWMImg.Height), 0, 0, objWMImg.Width, objWMImg.Height, GraphicsUnit.Pixel, imgAttr);
        //读取原来服务器端要写入水印的文件的文件类型
        FileInfo objFile = new FileInfo(objConfig.ServerFileFullPath);
        ImageFormat imageType = default(ImageFormat);
        switch (objFile.Extension.ToLower())
        {
          case ".jpg":
            imageType = ImageFormat.Jpeg;
            break;
          case ".jpeg":
            imageType = ImageFormat.Jpeg;
            break;
          case ".gif":
            imageType = ImageFormat.Gif;
            break;
          case ".png":
            imageType = ImageFormat.Png;
            break;
          case ".bmp":
            imageType = ImageFormat.Bmp;
            break;
          case ".tif":
            imageType = ImageFormat.Tiff;
            break;
          case ".wmf":
            imageType = ImageFormat.Wmf;
            break;
          case ".ico":
            imageType = ImageFormat.Icon;
            break;
          default:
            imageType = ImageFormat.Jpeg;
            break;
        }
        // 将已经画好水印的Image对象保存到内存流中
        objImg.Save(objMS, imageType);
        // 将内存流以数组形式保存到图像数组里
        byte[] imgData = objMS.ToArray();
        //将写了水印的图像数组存入改了名的文件
        objFS = new FileStream(RenameFile(objConfig.ServerFileFullPath), FileMode.Create, FileAccess.Write);
        if ((objFS != null))
        {
          objFS.Write(imgData, 0, imgData.Length);
        }
        objConfig.OPFlag = true;
        objConfig.ServerFileHasWMFullPath = RenameFile(objConfig.ServerFileFullPath);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        objFS.Close();
        objFS.Dispose();
        objFS = null;
        objImg.Dispose();
        objImg = null;
        objWMImg.Dispose();
        objWMImg = null;
        g.Dispose();
        g = null;
        objMS.Close();
        objMS.Dispose();
        objMS = null;
        imgAttr.Dispose();
        imgAttr = null;
        HelperFile.DeleteFile(objConfig.ServerFileHasWMFullPath);
      }
    }

    /// <summary>
    /// 在原文件名称的基础上加个字符串“1”
    /// </summary>
    /// <param name="strOld">原文件名称</param>
    /// <returns>在原文件名称的基础上加个字符串“1”</returns>
    private static string RenameFile(string strOldFileFullPath)
    {
      string strFileExt = strOldFileFullPath.Substring(strOldFileFullPath.LastIndexOf(".") + 1);
      string strFileName = strOldFileFullPath.Substring(0, strOldFileFullPath.LastIndexOf("."));
      return strFileName + "1." + strFileExt;
    }

    ///<summary> 
    /// 根据参数提供的宽度，高度缩小图片
    /// </summary> 
    /// <param name="strOldPic">源图文件名(包括路径)</param>
    /// <param name="intWidth">缩小至宽度</param>
    /// <param name="intHeight">缩小至高度</param>
    private static void ResizePic(string strOldPic, int intWidth, int intHeight)
    {
      Bitmap objPic = new Bitmap(strOldPic);
      Bitmap objNewPic = new Bitmap(objPic, intWidth, intHeight);
      try
      {
        objNewPic.Save(RenameFile(strOldPic));
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        objPic.Dispose();
        objPic = null;
        objNewPic.Dispose();
        objNewPic = null;
      }
    }

    /// <summary> 
    /// 按比例缩小图片，自动计算高度 
    /// </summary> 
    /// <param name="strOldPic">源图文件名(包括路径)</param>
    /// <param name="intWidth">缩小至宽度</param> 
    private static void ResizePicByWidth(string strOldPic, int intWidth)
    {
      Bitmap objPic = new Bitmap(strOldPic);
      int intHeight = 0;
      if (objPic.Width > intWidth)
      {
        double dbTemp = Math.Round(((double)intWidth / (double)objPic.Width), 2);
        intHeight = (int)Math.Ceiling(dbTemp * (double)objPic.Height);
      }
      else
      {
        intWidth = objPic.Width;
        intHeight = objPic.Height;
      }
      Bitmap objNewPic = new Bitmap(objPic, intWidth, intHeight);
      try
      {
        objNewPic.Save(RenameFile(strOldPic));
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        objPic.Dispose();
        objPic = null;
        objNewPic.Dispose();
        objNewPic = null;
      }
    }

    /// <summary> 
    /// 按比例缩小图片，自动计算宽度 
    /// </summary>
    /// <param name="strOldPic">源图文件名(包括路径)</param>
    /// <param name="intHeight">缩小至高度</param> 
    private static void ResizePicByHeight(string strOldPic, int intHeight)
    {
      Bitmap objPic = new Bitmap(strOldPic);
      int intWidth = 0;
      if (objPic.Width > intHeight)
      {
        intWidth = Convert.ToInt32((intHeight / objPic.Height) * objPic.Width);
      }
      else
      {
        intWidth = objPic.Width;
        intHeight = objPic.Height;
      }
      Bitmap objNewPic = new Bitmap(objPic, intWidth, intHeight);
      try
      {
        objNewPic.Save(RenameFile(strOldPic));
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        objPic.Dispose();
        objPic = null;
        objNewPic.Dispose();
        objNewPic = null;
      }
    }

    //生成缩略图
    private static ModelUploadFileConfig SetThumbImage(ModelUploadFileConfig objConfig)
    {
      if (!File.Exists(objConfig.ServerFileFullPath))
      {
        objConfig.OPFlag = false;
        objConfig.OPMessage = "文件不存在，生成缩略图失败。";
        return objConfig;
      }
      Image.GetThumbnailImageAbort objCallb = null;
      Image objImg = null;
      Image objImgThumb = null;
      try
      {
        objImg = Image.FromFile(objConfig.ServerFileFullPath);
        objImgThumb = objImg.GetThumbnailImage(objConfig.ThumbImageWidth, objConfig.ThumbImageHeight, objCallb, new IntPtr());
        string strFileExt = objConfig.ServerFileFullPath.Substring(objConfig.ServerFileFullPath.LastIndexOf(".") + 1);
        string strFileName = objConfig.ServerFileFullPath.Substring(0, objConfig.ServerFileFullPath.LastIndexOf("."));
        objConfig.Thumb1FilePath = strFileName + "tn." + strFileExt;
        objImgThumb.Save(objConfig.Thumb1FilePath);
        objConfig.OPFlag = true;
        objConfig.OPMessage = "生成缩略图成功！";
        return objConfig;
      }
      catch (Exception ex)
      {
        objConfig.OPFlag = false;
        objConfig.OPMessage = ex.Message;
        return objConfig;
      }
      finally
      {
        objImg.Dispose();
        objImgThumb.Dispose();
      }
    }

    /// <summary>
    /// 用ffmpeg截取视频文件一副图做缩略图
    /// </summary>
    /// <param name="strFFMPEGPath">FFMPEG插件相对根目录的路径</param>
    /// <param name="strVideoFilePath">视频文件路径，包括文件名（不是硬盘上路径，是相对网站根目录的路径）</param>
    /// <param name="strThumbWidth">缩略图的宽度</param>
    /// <param name="strThumbHeight">缩略图的高度</param>
    /// <returns>生成的缩略图的路径（不是硬盘上路径，是相对网站根目录的路径）</returns>
    public static string CatchThumb(string strFFMPEGPath, string strVideoFilePath, string strThumbWidth, string strThumbHeight)
    {
      // 比如视频文件地址是：/upload/video_fy/201505/17/GFwGDkqFGeykZg.flv
      // 则在同目录下生成同路径同名不同扩展名的jpeg文件
      // 输出的缩略图的路径为：/upload/video_fy/201505/17/GFwGDkqFGeykZg.jpg
      try
      {
        string strRootPath = HttpContext.Current.Request.PhysicalApplicationPath;
        string strFFMpegFullPath = strRootPath + strFFMPEGPath;
        if ((!File.Exists(strFFMpegFullPath)) || (!File.Exists(HttpContext.Current.Server.MapPath(strVideoFilePath))))
        {
          return "";
        }
        string strVideoFileFullPath = strRootPath + strVideoFilePath;
        //缩略图的路径，这就是最后的函数输出结果。如:D:\Video\Web\FlvFile\User1\0001.jpg 
        string strThumbPath = strVideoFilePath.Substring(0, strVideoFilePath.LastIndexOf(".")) + ".jpg";
        //缩略图的完全物理路径，如:D:\Video\Web\FlvFile\User1\0001.jpg 
        string strThumbFullPath = strRootPath + strThumbPath;
        //截图的尺寸大小,配置在Web.Config中,如:<add key="CatchFlvImgSize" value="140*110" /> 
        string strThumbSize = strThumbWidth + "*" + strThumbHeight;
        System.Diagnostics.ProcessStartInfo objPSI = new System.Diagnostics.ProcessStartInfo(strFFMpegFullPath);
        objPSI.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        //此处组合成ffmpeg.exe文件需要的参数即可,此处命令在ffmpeg 0.4.9调试通过
        objPSI.Arguments = " -i " + strVideoFileFullPath + " -y -f mjpeg -ss 3 -t 0.001 -s " +
            strThumbSize + " " + strThumbFullPath;
        try
        {
          System.Diagnostics.Process.Start(objPSI);
        }
        catch
        {
          return "";
        }
        // sleep(5000毫秒)
        System.Threading.Thread.Sleep(3000);
        if (File.Exists(strThumbFullPath))
          return strThumbPath;
        else
          return "";
      }
      catch
      {
        return "";
      }
    }

    //产生随机14位的文件名
    private static string SetRnd14FileName()
    {
      return HelperUtility.getRandomNumber(14);
    }

    //功能：根据时间创建目录
    //输入参数："uploadfiles/images/"
    //输出参数："uploadfiles/images/2018/03/21/"
    private static string CreateDateDir(string strDefaultDirName)
    {
      string strRootPath = HttpContext.Current.Request.PhysicalApplicationPath;
      string strPath = strDefaultDirName;
      strPath += DateTime.Now.ToString("yyyy") + "/";
      strPath += DateTime.Now.ToString("MM") + "/";
      strPath += DateTime.Now.ToString("dd") + "/";
      HelperFile.CreateDirectory(strRootPath + strPath);
      return strPath;
    }

  }

}