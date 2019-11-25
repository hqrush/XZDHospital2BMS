using System.Configuration;
using System.Drawing;

namespace Model
{

  public class ModelUploadFileConfig
  {
    //ConstantChar                字符串常量，用来起随机的目录名或者文件名
    //UploadFileRootDir           上传文件保存的目录名，相对于根路径，默认值是uploadfile
    //OPFlag                      上传是否成功的标志
    //OPMessage                   上传过程中的信息
    //ServerFileFullPath          文件保存在服务器上的完整路径，包括文件名
    //ClientFileFullPath          客户端的文件完整路径，包括文件名
    //ServerFileHasWMFullPath     生成水印后的图像文件路径
    //AllowUploadImageFileExt     允许上传的图像文件扩展名，只读属性
    //AllowUploadFileExt          允许上传的文件扩展名，只读属性
    //AllowUploadImageFileSize    允许上传的图像文件大小
    //AllowUploadSwfFileSize      允许上传的flash文件大小
    //AllowUploadTxtFileSize      允许上传的文本文件大小
    //AllowUploadZipFileSize      允许上传的压缩文件大小
    //AllowUploadMediaFileSize    允许上传的多媒体文件大小
    //WaterMarkTxtFlag            是否使用文本水印
    //WaterMarkTxtString          文本水印字符串
    //WaterMarkTxtSize            文本水印文本大小
    //WaterMarkTxtFont            文本水印文本字体
    //WaterMarkTxtColor           文本水印文本颜色
    //WaterMarkImageFlag          是否使用图片水印标志
    //WaterMarkImagePath          服务器端的水印文件路径
    //WaterMarkMarginTop          水印的上边距
    //WaterMarkMarginRight        水印的右边距
    //WaterMarkMarginBottom       水印的下边距
    //WaterMarkMarginLeft         水印的左边距
    //WaterMarkTransparent        水印透明度
    //ThumbFlag                   是否生成缩略图标志
    //Thumb1FilePath              缩略图小文件路径
    //Thumb2FilePath              缩略图中文件路径
    //Thumb3FilePath              缩略图大文件路径
    //ThumbImageWidth             缩略图的宽度
    //ThumbImageHeight            缩略图的高度
    //CompressRatio               压缩率
    //FileSize                    文件大小
    //ServerFileName              服务器端文件名
    //ClientFileName              客户端文件名
    //ExtName                     扩展名
    //PicWidth                    图像文件的宽度
    //PicHeight                   图像文件的高度

    /// <summary>
    /// 上传文件相关设置的model
    /// </summary>
    public ModelUploadFileConfig()
    {
    }

    /// <summary>
    /// 字符串常量，用来起随机的目录名或者文件名
    /// </summary>
    private char[] _ConstantChar = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

    /// <summary>
    /// 上传文件保存的目录名，相对于根路径，默认值是uploadfile
    /// </summary>
    private string _UploadFileRootDir = "/UploadFile";

    /// <summary>
    /// 操作标志
    /// </summary>
    private bool _OPFlag = false;

    /// <summary>
    /// 操作信息
    /// </summary>
    private string _OPMessage = "";

    /// <summary>
    /// 服务器端文件保存路径
    /// </summary>
    private string _ServerFileFullPath = "";

    /// <summary>
    /// 客户端文件路径
    /// </summary>
    private string _ClientFileFullPath = "";

    /// <summary>
    /// 生成水印后的图像文件路径
    /// </summary>
    private string _ServerFileHasWMFullPath = "";

    /// <summary>
    /// 允许上传什么类型的文件
    /// </summary>
    private string _AllowUploadFileExt = "gif,jpg,jpeg,bmp,png,tiff,swf,txt,doc,docx,xls,xlsx,chm,pdf,zip,rar,avi,flv,mp4,wma,mpg,mpeg,mp3,wav";

    /// <summary>
    /// 允许上传什么类型的图像文件
    /// </summary>
    private string _AllowUploadImageFileExt = "jpg,jpeg,gif,png,bmp,tiff";

    /// <summary>
    /// 允许上传的图像文件大小，以K为单位
    /// </summary>
    private int _AllowUploadImageFileSize = 10240;

    /// <summary>
    /// 允许上传的flash文件大小
    /// </summary>
    private int _AllowUploadSwfFileSize = 10240;

    /// <summary>
    /// 允许上传的文本文件大小
    /// </summary>        
    private int _AllowUploadTxtFileSize = 1000;

    /// <summary>
    /// 允许上传的压缩文件大小
    /// </summary>        
    private int _AllowUploadZipFileSize = 50000;

    /// <summary>
    /// 允许上传的多媒体文件大小
    /// </summary>        
    private int _AllowUploadMediaFileSize = 512000;

    /// <summary>
    /// 是否使用文本水印
    /// </summary>
    private bool _WaterMarkTxtFlag = false;

    /// <summary>
    /// 文本水印字符串
    /// </summary>
    private string _WaterMarkTxtString = "";

    /// <summary>
    /// 文本水印文本大小
    /// </summary>
    private float _WaterMarkTxtSize = 12f;

    /// <summary>
    /// 文本水印文本字体
    /// </summary>
    private string _WaterMarkTxtFont = "Verdana";

    /// <summary>
    /// 文本水印文本颜色
    /// </summary>
    private Color _WaterMarkTxtColor = Color.Red;


    /// <summary>
    /// 是否使用图片水印标志
    /// </summary>
    private bool _WaterMarkImageFlag = false;

    /// <summary>
    /// 水印文件服务器端路径
    /// </summary>        
    private string _WaterMarkImagePath = "/image/WaterMask.png";

    /// <summary>
    /// 水印的上边距
    /// </summary>
    private int _WaterMarkMarginTop = 0;

    /// <summary>
    /// 水印的右边距
    /// </summary>
    private int _WaterMarkMarginRight = 0;

    /// <summary>
    /// 水印的下边距
    /// </summary>
    private int _WaterMarkMarginBottom = 0;

    /// <summary>
    /// 水印的左边距
    /// </summary>
    private int _WaterMarkMarginLeft = 0;


    /// <summary>
    /// 水印透明度
    /// </summary>
    private int _WaterMarkTransparent = 80;

    /// <summary>
    /// 是否生成缩略图标志
    /// </summary>
    private bool _ThumbFlag = false;

    /// <summary>
    /// 缩略图小文件路径
    /// </summary>
    private string _Thumb1FilePath = "";

    /// <summary>
    /// 缩略图中文件路径
    /// </summary>
    private string _Thumb2FilePath = "";

    /// <summary>
    /// 缩略图大文件路径
    /// </summary>
    private string _Thumb3FilePath = "";

    /// <summary>
    /// 缩略图的宽度
    /// </summary>
    private int _ThumbImageWidth = 80;

    /// <summary>
    /// 缩略图的高度
    /// </summary>
    private int _ThumbImageHeight = 80;

    /// <summary>
    /// 压缩率
    /// </summary>
    private int _CompressRatio = 80;

    /// <summary>
    /// 文件大小
    /// </summary>
    private int _FileSize = 0;

    /// <summary>
    /// 服务器端文件名
    /// </summary>
    private string _ServerFileName = "";

    /// <summary>
    /// 客户端文件名
    /// </summary>
    private string _ClientFileName = "";

    /// <summary>
    /// 扩展名
    /// </summary>
    private string _ExtName = "";

    /// <summary>
    /// 图像文件的宽度
    /// </summary>
    private int _PicWidth = 0;

    /// <summary>
    /// 图像文件的高度
    /// </summary>
    private int _PicHeight = 0;

    public char[] ConstantChar
    {
      get
      {
        return _ConstantChar;
      }
      set
      {
        _ConstantChar = value;
      }
    }

    public string UploadFileRootDir
    {
      get
      {
        return _UploadFileRootDir;
      }
      set
      {
        _UploadFileRootDir = value;
      }
    }

    public bool OPFlag
    {
      get
      {
        return _OPFlag;
      }
      set
      {
        _OPFlag = value;
      }
    }

    public string OPMessage
    {
      get
      {
        return _OPMessage;
      }
      set
      {
        _OPMessage = value;
      }
    }

    public string ServerFileFullPath
    {
      get
      {
        return _ServerFileFullPath;
      }
      set
      {
        _ServerFileFullPath = value;
      }
    }

    public string ClientFileFullPath
    {
      get
      {
        return _ClientFileFullPath;
      }
      set
      {
        _ClientFileFullPath = value;
      }
    }

    public string ServerFileHasWMFullPath
    {
      get
      {
        return _ServerFileHasWMFullPath;
      }
      set
      {
        _ServerFileHasWMFullPath = value;
      }
    }

    public string AllowUploadFileExt
    {
      get
      {
        return _AllowUploadFileExt;
      }
      set
      {
        _AllowUploadFileExt = value;
      }
    }

    public string AllowUploadImageFileExt
    {
      get
      {
        return _AllowUploadImageFileExt;
      }
      set
      {
        _AllowUploadImageFileExt = value;
      }
    }

    public int AllowUploadImageFileSize
    {
      get
      {
        return _AllowUploadImageFileSize;
      }
      set
      {
        _AllowUploadImageFileSize = value;
      }
    }

    public int AllowUploadSwfFileSize
    {
      get
      {
        return _AllowUploadSwfFileSize;
      }
      set
      {
        _AllowUploadSwfFileSize = value;
      }
    }

    public int AllowUploadTxtFileSize
    {
      get
      {
        return _AllowUploadTxtFileSize;
      }
      set
      {
        _AllowUploadTxtFileSize = value;
      }
    }

    public int AllowUploadZipFileSize
    {
      get
      {
        return _AllowUploadZipFileSize;
      }
      set
      {
        _AllowUploadZipFileSize = value;
      }
    }

    public int AllowUploadMediaFileSize
    {
      get
      {
        return _AllowUploadMediaFileSize;
      }
      set
      {
        _AllowUploadMediaFileSize = value;
      }
    }

    public bool WaterMarkTxtFlag
    {
      get
      {
        return _WaterMarkTxtFlag;
      }
      set
      {
        _WaterMarkTxtFlag = value;
      }
    }

    public string WaterMarkTxtString
    {
      get
      {
        return _WaterMarkTxtString;
      }
      set
      {
        _WaterMarkTxtString = value;
      }
    }

    public float WaterMarkTxtSize
    {
      get
      {
        return _WaterMarkTxtSize;
      }
      set
      {
        _WaterMarkTxtSize = value;
      }
    }

    public string WaterMarkTxtFont
    {
      get
      {
        return _WaterMarkTxtFont;
      }
      set
      {
        _WaterMarkTxtFont = value;
      }
    }

    public Color WaterMarkTxtColor
    {
      get
      {
        return _WaterMarkTxtColor;
      }
      set
      {
        _WaterMarkTxtColor = value;
      }
    }

    public bool WaterMarkImageFlag
    {
      get
      {
        return _WaterMarkImageFlag;
      }
      set
      {
        _WaterMarkImageFlag = value;
      }
    }

    public string WaterMarkImagePath
    {
      get
      {
        return _WaterMarkImagePath;
      }
      set
      {
        _WaterMarkImagePath = value;
      }
    }

    public int WaterMarkMarginTop
    {
      get
      {
        return _WaterMarkMarginTop;
      }
      set
      {
        _WaterMarkMarginTop = value;
      }
    }

    public int WaterMarkMarginRight
    {
      get
      {
        return _WaterMarkMarginRight;
      }
      set
      {
        _WaterMarkMarginRight = value;
      }
    }

    public int WaterMarkMarginBottom
    {
      get
      {
        return _WaterMarkMarginBottom;
      }
      set
      {
        _WaterMarkMarginBottom = value;
      }
    }

    public int WaterMarkMarginLeft
    {
      get
      {
        return _WaterMarkMarginLeft;
      }
      set
      {
        _WaterMarkMarginLeft = value;
      }
    }

    public int WaterMarkTransparent
    {
      get
      {
        return _WaterMarkTransparent;
      }
      set
      {
        _WaterMarkTransparent = value;
      }
    }

    public bool ThumbFlag
    {
      get
      {
        return _ThumbFlag;
      }
      set
      {
        _ThumbFlag = value;
      }
    }

    public string Thumb1FilePath
    {
      get
      {
        return _Thumb1FilePath;
      }
      set
      {
        _Thumb1FilePath = value;
      }
    }

    public string Thumb2FilePath
    {
      get
      {
        return _Thumb2FilePath;
      }
      set
      {
        _Thumb2FilePath = value;
      }
    }

    public string Thumb3FilePath
    {
      get
      {
        return _Thumb3FilePath;
      }
      set
      {
        _Thumb3FilePath = value;
      }
    }

    public int ThumbImageWidth
    {
      get
      {
        return _ThumbImageWidth;
      }
      set
      {
        _ThumbImageWidth = value;
      }
    }

    public int ThumbImageHeight
    {
      get
      {
        return _ThumbImageHeight;
      }
      set
      {
        _ThumbImageHeight = value;
      }
    }

    public int CompressRatio
    {
      get
      {
        return _CompressRatio;
      }
      set
      {
        _CompressRatio = value;
      }
    }

    public int FileSize
    {
      get
      {
        return _FileSize;
      }
      set
      {
        _FileSize = value;
      }
    }

    public string ServerFileName
    {
      get
      {
        return _ServerFileName;
      }
      set
      {
        _ServerFileName = value;
      }
    }

    public string ClientFileName
    {
      get
      {
        return _ClientFileName;
      }
      set
      {
        _ClientFileName = value;
      }
    }

    public string ExtName
    {
      get
      {
        return _ExtName;
      }
      set
      {
        _ExtName = value;
      }
    }

    public int PicWidth
    {
      get
      {
        return _PicWidth;
      }
      set
      {
        _PicWidth = value;
      }
    }

    public int PicHeight
    {
      get
      {
        return _PicHeight;
      }
      set
      {
        _PicHeight = value;
      }
    }

  }

}