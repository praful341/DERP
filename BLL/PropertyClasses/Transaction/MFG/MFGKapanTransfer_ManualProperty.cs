using System;

namespace BLL.PropertyClasses.Transaction.MFG
{
    public class MFGKapanTransfer_ManualProperty
    {
        public Int64 transfer_id { get; set; }
        public int from_kapan_id { get; set; }
        public int to_kapan_id { get; set; }
        public decimal carat { get; set; }
        public decimal rate { get; set; }
        public decimal amount { get; set; }
        public string transfer_date { get; set; }

    }
}
