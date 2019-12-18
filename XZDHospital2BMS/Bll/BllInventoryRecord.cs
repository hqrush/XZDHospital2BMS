﻿using Dal;
using Model;
using System.Data;

namespace Bll
{
  public class BllInventoryRecord
  {

    public static int add(ModelInventoryRecord model)
    {
      return DalInventoryRecord.add(model);
    }

    public static void deleteById(int intId)
    {
      DalInventoryRecord.deleteById(intId);
    }

    public static void update(ModelInventoryRecord model)
    {
      DalInventoryRecord.update(model);
    }

    public static void updateRealById(decimal dcmAmountReal, int intId)
    {
      DalInventoryRecord.updateRealById(dcmAmountReal, intId);
    }

    public static void updateShowById(decimal dcmAmountShow, int intId)
    {
      DalInventoryRecord.updateShowById(dcmAmountShow, intId);
    }

    public static ModelInventoryRecord getById(int intId)
    {
      return DalInventoryRecord.getById(intId);
    }

    public static DataTable getAll(int intContractId)
    {
      return DalInventoryRecord.getAll(intContractId);
    }

    public static DataTable getPage(int intContractId, int intPage, int intPageSize)
    {
      return DalInventoryRecord.getPage(intContractId, intPage, intPageSize);
    }

    public static int getRecordsAmount(int intContractId)
    {
      return DalInventoryRecord.getRecordsAmount(intContractId);
    }

    public static decimal getPriceTotalStock(int intContractId)
    {
      return DalInventoryRecord.getPriceTotalStock(intContractId);
    }

    public static decimal[] getPriceTotalInventory(int intContractId)
    {
      return DalInventoryRecord.getPriceTotalInventory(intContractId);
    }

    public static void setRecord(int intContractId)
    {
      DalInventoryRecord.setRecord(intContractId);
    }

  }

}
