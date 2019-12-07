using System;

namespace Model
{

  [Serializable]
  public class ModelCheckoutRecord
  {

    private int _id = 0;
    private int _id_contract = 0;
    private int _id_goods = 0;
    private decimal _amount = 0;

    public ModelCheckoutRecord() { }

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

    public int id_goods
    {
      get
      {
        return _id_goods;
      }

      set
      {
        _id_goods = value;
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

  }

}