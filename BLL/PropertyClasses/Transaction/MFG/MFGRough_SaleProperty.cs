namespace BLL.PropertyClasses.Transaction.MFG
{
    public class MFGRough_SaleProperty
    {
        public long invoice_id { get; set; }
        public string invoice_date { get; set; }
        public int sr_no { get; set; }
        public int invoice_no { get; set; }
        public long? kapan_id { get; set; }
        public long? party_id { get; set; }
        public long? broker_id { get; set; }
        public int? company_id { get; set; }
        public int? branch_id { get; set; }
        public int? location_id { get; set; }
        public int? department_id { get; set; }
        public int? currency_id { get; set; }
        public decimal? total_carat { get; set; }
        public decimal? total_rate { get; set; }
        public decimal? total_amount { get; set; }
        public decimal? exchange_rate { get; set; }
        public string remarks { get; set; }
        public string special_remarks { get; set; }
        public string client_remarks { get; set; }
        public string payment_remarks { get; set; }
        public int? due_days { get; set; }
        public decimal? brokerage_per { get; set; }
        public decimal? brokerage_amount { get; set; }
        public decimal? premium_per { get; set; }
        public decimal? premium_amount { get; set; }
        public decimal? discount_per { get; set; }
        public decimal? discount_amount { get; set; }
        public decimal? net_amount { get; set; }
    }
}
