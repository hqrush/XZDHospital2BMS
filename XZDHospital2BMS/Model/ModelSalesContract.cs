using System;

namespace Model
{

  [Serializable]
  public class ModelSalesContract
  {

    private int _id = 0;
    private int _id_company = 0;
    private int _id_admin = 0;
    private DateTime _time_sign = DateTime.Now;
    private DateTime _time_create = DateTime.Now;
    private string _photo_urls = "";
    private string _comment = "";
    private int _is_deleted = 0;

    public ModelSalesContract() { }

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

    public int id_company
    {
      get
      {
        return _id_company;
      }

      set
      {
        _id_company = value;
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

    public DateTime time_sign
    {
      get
      {
        return _time_sign;
      }

      set
      {
        _time_sign = value;
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

    public int is_deleted
    {
      get
      {
        return _is_deleted;
      }

      set
      {
        _is_deleted = value;
      }
    }

  }

}