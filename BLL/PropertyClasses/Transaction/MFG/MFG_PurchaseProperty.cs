namespace BLL.PropertyClasses.Transaction
{
    public class MFG_PurchaseProperty
    {
        #region "Master"
        public long purchase_id { get; set; }
        public long rough_purchase_id { get; set; }
        public string date { get; set; }
        public string shiping_date { get; set; }
        public int? fin_year_id { get; set; }
        public string party_invoice_no { get; set; }
        public string invoice_no { get; set; }
        public int? party_id { get; set; }
        public int? broker_id { get; set; }
        public int? sight_type_id { get; set; }
        public int? source_id { get; set; }
        public int? article_id { get; set; }
        public int? group_id { get; set; }
        public int? team_id { get; set; }
        public int? janged_sieve_id { get; set; }
        public int? rough_type_id { get; set; }
        public int? company_id { get; set; }
        public int? branch_id { get; set; }
        public int? location_id { get; set; }
        public int? department_id { get; set; }
        public int? currency_id { get; set; }
        public decimal? exchange_rate { get; set; }
        public string remarks { get; set; }
        public string special_remarks { get; set; }
        public string client_remarks { get; set; }
        public string payment_remarks { get; set; }
        public int? due_days { get; set; }
        public int? term_days { get; set; }
        public decimal? brokrage_per { get; set; }
        public decimal? brokrage_amount { get; set; }
        public decimal? premium_per { get; set; }
        public decimal? premium_amount { get; set; }
        public decimal? other_expence { get; set; }
        public decimal? net_amount { get; set; }
        public int? form_id { get; set; }
        public string series { get; set; }
        public string due_date { get; set; }
        public string currency_type { get; set; }
        public decimal? shiping_rate { get; set; }
        public decimal? shiping_per { get; set; }
        public decimal? net_rate { get; set; }
        public bool? is_inward { get; set; }
        public long lot_srno { get; set; }
        public string type { get; set; }
        #endregion

        #region "Details"
        public int? lot_id { get; set; }
        public int? rough_sieve_id { get; set; }
        public int? rough_shade_id { get; set; }
        public decimal? rate { get; set; }
        #endregion

        #region "Details"
        public long purchase_detail_id { get; set; }
        public int? pcs { get; set; }
        public decimal? carat { get; set; }
        public decimal? amount { get; set; }
        #endregion
    }
}
