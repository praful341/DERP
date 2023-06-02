namespace BLL.PropertyClasses.Transaction
{
    public class MFGProcessIssueProperty
    {
        public long Issue_id { get; set; }
        public long rough_cut_id { get; set; }
        public long lot_id { get; set; }
        public long? form_id { get; set; }
        public string issue_date { get; set; }
        public long? manager_id { get; set; }
        public long? employee_id { get; set; }
        public long? process_id { get; set; }
        public long? sub_process_id { get; set; }
        public long? lot_srno { get; set; }
        public long? rough_sieve_id { get; set; }
        public long? purity_id { get; set; }
        public long? rough_clarity_id { get; set; }
        public long? quality_id { get; set; }
        public int? flag { get; set; }
        public int? pcs { get; set; }
        public decimal? carat { get; set; }
        public decimal? rate { get; set; }
        public decimal? amount { get; set; }
        public int? prd_id { get; set; }
        public long union_id { get; set; }
        public long issue_union_id { get; set; }
        public int kapan_id { get; set; }
        public long? history_union_id { get; set; }
        public int? rejection_pcs { get; set; }
        public decimal? rejection_carat { get; set; }
        public int? rr_pcs { get; set; }
        public decimal? rr_carat { get; set; }
        public int? resoing_pcs { get; set; }
        public decimal? resoing_carat { get; set; }
        public int? breakage_pcs { get; set; }
        public decimal? breakage_carat { get; set; }
        public string Messgae { get; set; }
        public string temp_sieve_name { get; set; }
        public string temp_purity_name { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public int? location_id { get; set; }
    }
}
