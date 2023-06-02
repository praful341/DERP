using System;

namespace BLL.PropertyClasses.Rejection
{
    public class MFGRejectionInternalTransferProperty
    {
        public Int64 from_purity_id { get; set; }
        public Int64 to_purity_id { get; set; }
        public Int64 transfer_id { get; set; }
        public string transfer_date { get; set; }
        public string type { get; set; }
        public decimal total_carat { get; set; }
        public decimal pcs { get; set; }
        public decimal carat { get; set; }
        public decimal rate { get; set; }
        public decimal amount { get; set; }
        public string remarks { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public decimal diff_carat { get; set; }
    }
}
