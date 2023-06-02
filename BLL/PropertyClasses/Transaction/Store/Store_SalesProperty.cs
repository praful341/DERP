using System;

namespace BLL.PropertyClasses.Transaction.Store
{
    public class Store_SalesProperty
    {
        public Int64 sales_detail_id { get; set; }
        public Int64 sales_id { get; set; }
        public Int64 item_id { get; set; }
        public Int64 sub_item_id { get; set; }
        public decimal qty { get; set; }
        public int unit { get; set; }
        public decimal rate { get; set; }
        public decimal amount { get; set; }
        public string remarks { get; set; }
        public string sales_date { get; set; }
        public string bill_date { get; set; }
        public Int64 party_id { get; set; }
        public Int64 sales_no { get; set; }
        public string s_bill_no { get; set; }
        public decimal total_qty { get; set; }
        public decimal total_rate { get; set; }
        public decimal total_amount { get; set; }
        public Int64 company_id { get; set; }
        public Int64 branch_id { get; set; }
        public Int64 location_id { get; set; }
        public Int64 department_id { get; set; }
        public Int64 form_id { get; set; }
        public string item_condition { get; set; }
    }
}
