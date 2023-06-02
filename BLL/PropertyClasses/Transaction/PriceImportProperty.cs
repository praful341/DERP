using System;

namespace BLL.PropertyClasses.Transaction
{
    public class PriceImportProperty
    {
        #region "Sales Price Import"
        public int rate_id { get; set; }
        public Int64 rate_detail_id { get; set; }
        public Int64 range_id { get; set; }
        public Int64 color_id { get; set; }
        public int flag { get; set; }
        public int sieve_id { get; set; }
        public int assort_id { get; set; }
        public string color_name { get; set; }
        public string size_name { get; set; }
        public string rate_date { get; set; }
        public int rate_type_id { get; set; }
        public decimal rate { get; set; }
        public int currency_id { get; set; }
        public int sequence_no { get; set; }
        public decimal per_pcs { get; set; }
        public decimal per_carat { get; set; }
        public decimal janged_per_carat { get; set; }
        public int purity_id { get; set; }
        public int count { get; set; }
        public decimal from_rate { get; set; }
        public decimal to_rate { get; set; }
        public string range { get; set; }

        #endregion

        #region "Rough Price Import"
        public int rough_rate_id { get; set; }
        public int rough_clarity_id { get; set; }
        public int rough_sieve_id { get; set; }
        public string remarks { get; set; }
        public Int64 form_id { get; set; }
        #endregion
    }
}
