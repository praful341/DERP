using System;

namespace BLL.PropertyClasses.Rejection
{
    public class MFGRejection_TransferProperty
    {
        public int transfer_id { get; set; }
        public int kapan_id { get; set; }
        public int cut_id { get; set; }
        public int process_id { get; set; }
        public Int64 prediction_id { get; set; }
        public int from_clarity_id { get; set; }
        public int from_purity_id { get; set; }
        public int to_purity_id { get; set; }
        public int old_to_purity_id { get; set; }
        public decimal from_carat { get; set; }
        public decimal from_rate { get; set; }
        public decimal from_amount { get; set; }
        public decimal to_carat { get; set; }
        public decimal to_rate { get; set; }
        public decimal to_amount { get; set; }
        public string transfer_date { get; set; }
        public long? lot_srno { get; set; }
        public Int64 dept_transfer_id { get; set; }

    }
}
