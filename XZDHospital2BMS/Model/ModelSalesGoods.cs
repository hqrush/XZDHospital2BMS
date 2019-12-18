using System;

namespace Model
{
  public class ModelSalesGoods
  {

    private int _id = 0;
    private int _id_contract = 0;
    private string _name_product = "";
    private string _type = "";
    private string _name_factory = "";
    private string _unit = "";
    private decimal _amount = 0;
    private decimal _price_unit = 0;
    private decimal _price_total = 0;
    private string _batch_number = "";
    private DateTime _validity_period = DateTime.Now;
    private string _approval_number = "";
    private string _comment = "";
    private string _photo_urls = "";
    private int _id_admin = 0;
    private DateTime _time_add = DateTime.Now;
    private decimal _amount_stock = 0;

    public ModelSalesGoods() { }

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

    public int id_contract
    {
      get
      {
        return _id_contract;
      }

      set
      {
        _id_contract = value;
      }
    }

    public string name_product
    {
      get
      {
        return _name_product;
      }

      set
      {
        _name_product = value;
      }
    }

    public string type
    {
      get
      {
        return _type;
      }

      set
      {
        _type = value;
      }
    }

    public string name_factory
    {
      get
      {
        return _name_factory;
      }

      set
      {
        _name_factory = value;
      }
    }

    public string unit
    {
      get
      {
        return _unit;
      }

      set
      {
        _unit = value;
      }
    }

    public decimal amount
    {
      get
      {
        return _amount;
      }

      set
      {
        _amount = value;
      }
    }

    public decimal price_unit
    {
      get
      {
        return _price_unit;
      }

      set
      {
        _price_unit = value;
      }
    }

    public decimal price_total
    {
      get
      {
        return _price_total;
      }

      set
      {
        _price_total = value;
      }
    }

    public string batch_number
    {
      get
      {
        return _batch_number;
      }

      set
      {
        _batch_number = value;
      }
    }

    public DateTime validity_period
    {
      get
      {
        return _validity_period;
      }

      set
      {
        _validity_period = value;
      }
    }

    public string approval_number
    {
      get
      {
        return _approval_number;
      }

      set
      {
        _approval_number = value;
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

    public decimal amount_stock
    {
      get
      {
        return _amount_stock;
      }
      set
      {
        _amount_stock = value;
      }
    }

  }

}