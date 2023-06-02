using System;

namespace BLL.PropertyClasses.Account
{
    public class MFGRoughSaleStatus_Property
    {
        public Int64 rough_party_id { get; set; }
        public Int64 rough_broker_id { get; set; }
        public Int64 rough_purchase_id { get; set; }
        public Int64 union_id { get; set; }
        public string invoice_date { get; set; }
        public decimal mfg_carat { get; set; }
        public decimal mfg_pending { get; set; }
        public decimal mfg_working { get; set; }
        public decimal assort_sale { get; set; }
        public decimal assort_pending { get; set; }
        public decimal assort_working { get; set; }
        public decimal sale_carat { get; set; }
        public decimal sale_rate { get; set; }
        public decimal cl_carat { get; set; }
        public string delivery_date { get; set; }
        public int is_delivery { get; set; }
        public Int64 form_id { get; set; }
        public decimal net_rate { get; set; }
        public decimal net_amount { get; set; }
        public int term_days { get; set; }
        public string due_date { get; set; }
        public int is_final_dispatch { get; set; }
        public decimal exchange_rate { get; set; }
        public decimal inr_amount { get; set; }
        public decimal closing_amount { get; set; }
    }
}
