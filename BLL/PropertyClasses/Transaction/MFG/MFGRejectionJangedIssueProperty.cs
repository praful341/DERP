namespace BLL.PropertyClasses.Transaction.MFG
{
    public class MFGRejectionJangedIssueProperty
    {
        public long janged_id { get; set; }
        public long janged_detail_id { get; set; }
        public long union_id { get; set; }
        public long rej_purity_id { get; set; }
        public int sr_no { get; set; }
        public string invoice_date { get; set; }
        public string due_date { get; set; }
        public string type { get; set; }
        public decimal gross_rate { get; set; }
        public decimal gross_amount { get; set; }
        public decimal add_per { get; set; }
        public decimal add_amount { get; set; }
        public decimal less_per { get; set; }
        public decimal less_amount { get; set; }
        public string invoice_no { get; set; }
        public long? rejection_party_id { get; set; }
        public long? broker_id { get; set; }
        public string rejection_broker_name { get; set; }
        public int? company_id { get; set; }
        public int? branch_id { get; set; }
        public int? location_id { get; set; }
        public int? department_id { get; set; }
        public decimal? total_pcs { get; set; }
        public decimal? total_carat { get; set; }
        public string remarks { get; set; }
        public int? due_days { get; set; }
        public decimal? net_amount { get; set; }
        public long form_id { get; set; }
        public string group_name { get; set; }
        public decimal? pcs { get; set; }
        public decimal? carat { get; set; }
        public decimal? rate { get; set; }
        public decimal? amount { get; set; }
        public string currency { get; set; }
        public int currency_type_id { get; set; }
        public decimal exchange_rate { get; set; }
    }
}
