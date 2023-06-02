namespace BLL.PropertyClasses.Master.MFG
{
    public class MfgPrice_SettingProperty
    {
        public int? setting_id { get; set; }
        public int? company_id { get; set; }
        public int? branch_id { get; set; }
        public int? location_id { get; set; }
        public int? currency_id { get; set; }
        public int? rate_type_id { get; set; }
        public string setting_name { get; set; }
        public string setting_column { get; set; }
        public decimal per { get; set; }
        public int? days { get; set; }
    }
}
