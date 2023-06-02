using System;

namespace BLL.PropertyClasses.Rejection
{
    public class MFGRejectionToMakable_TransferProperty
    {
        public Int64 transfer_id { get; set; }
        public string transfer_date { get; set; }
        public Int64 lot_srno { get; set; }
        public Int64 kapan_id { get; set; }
        public Int64 purity_id { get; set; }
        public Int64 manager_id { get; set; }
        public string type { get; set; }
        public Int64 section_id { get; set; }
        public decimal pcs { get; set; }
        public decimal carat { get; set; }
        public decimal rate { get; set; }
        public decimal amount { get; set; }

    }
}
