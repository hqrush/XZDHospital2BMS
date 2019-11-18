using System;

namespace Model
{

  [Serializable]
  public class ModelSalesCompany
  {

    private int _id = 0;
    private string _name = "";
    private int _id_admin = 0;
    private DateTime _time_create = DateTime.Now;

    public ModelSalesCompany() { }

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

    public string name
    {
      get
      {
        return _name;
      }

      set
      {
        _name = value;
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

  }

}