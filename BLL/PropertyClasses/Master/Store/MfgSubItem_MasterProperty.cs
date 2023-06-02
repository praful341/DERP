using System;

namespace BLL.PropertyClasses.Master.Store
{
    public class MfgSubItem_MasterProperty
    {
        public Int64 sub_item_id { get; set; }
        public Int64 item_id { get; set; }
        public string sub_item_name { get; set; }
        public string opening_date { get; set; }
        public decimal opening_quantity { get; set; }
        public decimal opening_rate { get; set; }
        public decimal opening_amt { get; set; }
        public string model_no { get; set; }
        public Int32 warranty_year { get; set; }
        public Int32 warranty_month { get; set; }
        public string company_name { get; set; }
        public string item_code { get; set; }
        public decimal ton { get; set; }
        public string type { get; set; }
        public Int64 party_id { get; set; }
        public int? sequence_no { get; set; }
    }
}
