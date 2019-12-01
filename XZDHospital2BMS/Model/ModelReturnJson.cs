using System;

namespace Model
{

  [Serializable]
  public class ModelReturnJson
  {

    public ModelReturnJson() { }

    private string statusCode = "";
    private string message = "";
    private string data = "";

    public override string ToString()
    {
      string strReturn = "{ \"StatusCode\" : \"" + StatusCode + "\", ";
      strReturn += "\"Message\" : \"" + Message + "\", ";
      strReturn += "\"Data\" : \"" + Data + "\" }";
      return strReturn;
    }

    public string StatusCode { get => statusCode; set => statusCode = value; }
    public string Message { get => message; set => message = value; }
    public string Data { get => data; set => data = value; }

  }

}
