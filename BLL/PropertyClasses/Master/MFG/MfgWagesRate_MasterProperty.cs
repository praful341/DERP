using System;

namespace BLL.PropertyClasses.Master
{
    public class MfgWagesRate_MasterProperty
    {
        public Int64 wages_rate_id { get; set; }
        public int? company_id { get; set; }
        public int? branch_id { get; set; }
        public int? location_id { get; set; }
        public int? department_id { get; set; }
        public int? rough_sieve_id { get; set; }
        public int? process_id { get; set; }
        public int? sub_process_id { get; set; }
        public string wages_type { get; set; }
        public decimal? per_carat_rate { get; set; }
        public decimal? per_pcs_rate { get; set; }
        public decimal? per_hrs_rate { get; set; }
        public bool? active { get; set; }
        public string remarks { get; set; }
        public int? packet_type_id { get; set; }
    }
}
