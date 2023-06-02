namespace BLL.PropertyClasses.Master.MFG
{
    public class MfgProductionCapacity_MasterProperty
    {
        public int production_capacity_id { get; set; }
        public string capacity_date { get; set; }
        public int? assort_id { get; set; }
        public int? sieve_id { get; set; }
        public decimal? capacity { get; set; }
        public decimal? target { get; set; }
        public int? company_id { get; set; }
        public int? branch_id { get; set; }
        public int? location_id { get; set; }
        public int? department_id { get; set; }
    }
}
