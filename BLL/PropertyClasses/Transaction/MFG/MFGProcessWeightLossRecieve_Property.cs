using System;

namespace BLL.PropertyClasses.Transaction.MFG
{
    public class MFGProcessWeightLossRecieve_Property
    {
        public Int64? issue_id { get; set; }
        public long recieve_id { get; set; }
        public long lot_id { get; set; }
        public long lot_srno { get; set; }
        public long receive_union_id { get; set; }
        public int rough_cut_id { get; set; }
        public long kapan_id { get; set; }
        public long union_id { get; set; }
        public string receive_date { get; set; }
        public long? manager_id { get; set; }
        public long? form_id { get; set; }
        public long? employee_id { get; set; }
        public long? process_id { get; set; }
        public long? sub_process_id { get; set; }
        public long? machine_id { get; set; }
        public int? pcs { get; set; }
        public decimal? carat { get; set; }
        public int? rr_pcs { get; set; }
        public decimal? rr_carat { get; set; }
        public int? loss_pcs { get; set; }
        public decimal? loss_carat { get; set; }
        public int? lost_pcs { get; set; }
        public decimal? lost_carat { get; set; }
        public int? rejection_pcs { get; set; }
        public decimal? rejection_carat { get; set; }
        public decimal? carat_plus { get; set; }
        public decimal? final_weight { get; set; }
        public int? is_outstanding { get; set; }
        public long? history_union_id { get; set; }
        public int? braking_pcs { get; set; }
        public decimal? braking_carat { get; set; }
        public int? resoing_pcs { get; set; }
        public decimal? resoing_carat { get; set; }
        public int? total_flag { get; set; }
        public long boiling_lot_id { get; set; }
    }
}
