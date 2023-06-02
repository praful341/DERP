namespace BLL.PropertyClasses.Master.MFG
{
    public class ProcessRate_MasterProperty
    {
        public int process_rate_id { get; set; }
        public int process_id { get; set; }
        public int sub_process_id { get; set; }
        public string rate_date { get; set; }
        public decimal rate { get; set; }
        public int packet_type_id { get; set; }
        public int? company_id { get; set; }
        public int? branch_id { get; set; }
        public int? location_id { get; set; }
        public int? department_id { get; set; }
    }
}
