using System;

namespace Model
{

  [Serializable]
  public class ModelAdmin
  {

    private int _id = 0;
    private string _username = "";
    private string _password = "";
    private string _salt = "";
    private string _real_name = "";
    private string _id_card = "";
    private string _mobile_phone = "";
    private string _avatar_url = "";
    private DateTime _time_add = DateTime.Now;
    private DateTime _time_last_login = DateTime.Now;
    private int _enabled = 1;
    private string _purviews = "";
    private int _is_deleted = 0;

    public ModelAdmin() { }

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

    public string username
    {
      get
      {
        return _username;
      }

      set
      {
        _username = value;
      }
    }

    public string password
    {
      get
      {
        return _password;
      }

      set
      {
        _password = value;
      }
    }

    public string salt
    {
      get
      {
        return _salt;
      }

      set
      {
        _salt = value;
      }
    }

    public string real_name
    {
      get
      {
        return _real_name;
      }

      set
      {
        _real_name = value;
      }
    }

    public string id_card
    {
      get
      {
        return _id_card;
      }

      set
      {
        _id_card = value;
      }
    }

    public string mobile_phone
    {
      get
      {
        return _mobile_phone;
      }

      set
      {
        _mobile_phone = value;
      }
    }

    public string avatar_url
    {
      get
      {
        return _avatar_url;
      }

      set
      {
        _avatar_url = value;
      }
    }

    public DateTime time_add
    {
      get
      {
        return _time_add;
      }

      set
      {
        _time_add = value;
      }
    }

    public DateTime time_last_login
    {
      get
      {
        return _time_last_login;
      }

      set
      {
        _time_last_login = value;
      }
    }

    public int enabled
    {
      get
      {
        return _enabled;
      }

      set
      {
        _enabled = value;
      }
    }

    public string purviews
    {
      get
      {
        return _purviews;
      }

      set
      {
        _purviews = value;
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