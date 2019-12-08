using System;

namespace Model
{

  [Serializable]
  public class ModelMedicalRecord
  {

    private int _id = 0;
    private string _sickbed_number = "";
    private string _name_real = "";
    private string _sex = "";
    private DateTime _birthday = DateTime.Now;
    private string _department = "";
    private string _name_disease = "";
    private DateTime _time_in = DateTime.Now;
    private DateTime _time_out = DateTime.Now;
    private string _situation_out = "";
    private string _situation_in = "";
    private string _photo_urls = "";
    private string _comment = "";
    private DateTime _time_create = DateTime.Now;
    private int _id_admin = 0;
    private int _is_deleted = 0;

    public ModelMedicalRecord() { }

    public int id { get => _id; set => _id = value; }
    public string sickbed_number { get => _sickbed_number; set => _sickbed_number = value; }
    public string name_real { get => _name_real; set => _name_real = value; }
    public string sex { get => _sex; set => _sex = value; }
    public DateTime birthday { get => _birthday; set => _birthday = value; }
    public string department { get => _department; set => _department = value; }
    public string name_disease { get => _name_disease; set => _name_disease = value; }
    public DateTime time_in { get => _time_in; set => _time_in = value; }
    public DateTime time_out { get => _time_out; set => _time_out = value; }
    public string situation_out { get => _situation_out; set => _situation_out = value; }
    public string situation_in { get => _situation_in; set => _situation_in = value; }
    public string photo_urls { get => _photo_urls; set => _photo_urls = value; }
    public string comment { get => _comment; set => _comment = value; }
    public DateTime time_create { get => _time_create; set => _time_create = value; }
    public int id_admin { get => _id_admin; set => _id_admin = value; }
    public int is_deleted { get => _is_deleted; set => _is_deleted = value; }

  }

}