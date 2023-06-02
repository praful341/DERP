namespace BLL.PropertyClasses.Master.MFG
{
    public class MfgMachine_MasterProperty
    {
        public int machine_id { get; set; }
        public string machine_name { get; set; }
        public string manual_code { get; set; }
        public string model_no { get; set; }
        public string serial_no { get; set; }
        public int? company_id { get; set; }
        public int? branch_id { get; set; }
        public int? location_id { get; set; }
        public int? department_id { get; set; }
        public int? team_id { get; set; }
        public int? group_id { get; set; }
        public int? process_id { get; set; }
        public string vendor_name { get; set; }
        public string category_name { get; set; }
        public int? machine_type_id { get; set; }
        public bool? active { get; set; }
        public string remarks { get; set; }
        public string date_acquired { get; set; }
        public string installation_date { get; set; }
        public string purchase_date { get; set; }
        public decimal purchase_rate { get; set; }
        public string robert_kit_date { get; set; }
        public decimal? depriciable_life { get; set; }
        public decimal? electricity_per_hrs { get; set; }
        public string machine_activity { get; set; }
    }
}
