using System;

namespace Model
{

  [Serializable]
  public class ModelCheckoutContract
  {

    private int _id = 0;
    private int _id_admin = 0;
    private DateTime _time_create = DateTime.Now;
    private string _name_unit = "";
    private string _name_department = "";
    private string _name_sign = "";
    private string _photo_urls = "";
    private string _comment = "";

    public ModelCheckoutContract() { }

    public int id
    {
      get
      {
        return _id;
      }

      set
      {
        _id = value;
      }
    }

    public int id_admin
    {
      get
      {
        return _id_admin;
      }

      set
      {
        _id_admin = value;
      }
    }

    public DateTime time_create
    {
      get
      {
        return _time_create;
      }

      set
      {
        _time_create = value;
      }
    }

    public string name_unit
    {
      get
      {
        return _name_unit;
      }

      set
      {
        _name_unit = value;
      }
    }

    public string name_department
    {
      get
      {
        return _name_department;
      }

      set
      {
        _name_department = value;
      }
    }

    public string name_sign
    {
      get
      {
        return _name_sign;
      }

      set
      {
        _name_sign = value;
      }
    }

    public string photo_urls
    {
      get
      {
        return _photo_urls;
      }

      set
      {
        _photo_urls = value;
      }
    }

    public string comment
    {
      get
      {
        return _comment;
      }

      set
      {
        _comment = value;
      }
    }

  }

}