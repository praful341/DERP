namespace BLL.PropertyClasses.Master.MFG
{
    public class InsuranceRate_MasterProperty
    {
        public int insurance_rate_id { get; set; }
        public int quality_id { get; set; }
        public int rough_sieve_id { get; set; }
        public string rate_date { get; set; }
        public decimal rate { get; set; }
        public int? company_id { get; set; }
        public int? branch_id { get; set; }
        public int? location_id { get; set; }
        public int? department_id { get; set; }
    }
}
