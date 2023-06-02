namespace BLL.PropertyClasses.Transaction.MFG
{
    public class MFGJangedReceive_Property
    {
        public long Issue_id { get; set; }
        public long? previous_janged_id { get; set; }
        public long janged_no { get; set; }
        public long receive_union_id { get; set; }
        public long janged_srno { get; set; }
        public long lot_srno { get; set; }
        public long? previous_janged_no { get; set; }
        public long? rough_quality_id { get; set; }
        public string janged_date { get; set; }
        public long? party_id { get; set; }
        public long? manager_id { get; set; }
        public long? to_manager_id { get; set; }
        public long? employee_id { get; set; }
        public long? process_id { get; set; }
        public long? sub_process_id { get; set; }
        public long? kapan_id { get; set; }
        public long? rough_cut_id { get; set; }
        public long? lot_id { get; set; }
        public int? pcs { get; set; }
        public decimal? carat { get; set; }
        public decimal? loss_carat { get; set; }
        public decimal? rate { get; set; }
        public decimal? amount { get; set; }
        public long? from_company_id { get; set; }
        public long? from_branch_id { get; set; }
        public long? from_location_id { get; set; }
        public long? from_department_id { get; set; }
        public int? is_outside { get; set; }
        public long? form_id { get; set; }
        public long union_id { get; set; }
        public long issue_union_id { get; set; }
        public long janged_union_id { get; set; }
        public long dept_union_id { get; set; }
        public long? rough_clarity_id { get; set; }
        public long? rough_sieve_id { get; set; }
        public long? purity_id { get; set; }
        public decimal? carat_plus { get; set; }
        public int? loss_count { get; set; }
        public long? history_union_id { get; set; }
    }
}
