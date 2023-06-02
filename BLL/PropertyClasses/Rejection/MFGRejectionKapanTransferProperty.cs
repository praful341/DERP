using System;

namespace BLL.PropertyClasses.Rejection
{
    public class MFGRejectionKapanTransferProperty
    {
        public Int64 from_kapan_id { get; set; }
        public Int64 to_kapan_id { get; set; }
        public Int64 transfer_id { get; set; }
        public string transfer_date { get; set; }
        public string remarks { get; set; }
        public decimal pcs { get; set; }
        public decimal carat { get; set; }
        public decimal rate { get; set; }
        public decimal amount { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
    }
}
