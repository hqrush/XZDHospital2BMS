﻿using System;

namespace Model
{

  [Serializable]
  public class ModelInventoryRecord
  {

    private int _id = 0;
    private int _id_contract = 0;
    private int _id_goods = 0;
    private int _amount_real = 0;
    private int _amount_show = 0;

    public ModelInventoryRecord() { }

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

    public int amount_real
    {
      get
      {
        return _amount_real;
      }

      set
      {
        _amount_real = value;
      }
    }

    public int amount_show
    {
      get
      {
        return _amount_show;
      }

      set
      {
        _amount_show = value;
      }
    }

  }

}