namespace BLL.PropertyClasses.Transaction.MFG
{
    public class MFGLotSplitProperty
    {
        public long? rough_lot_id { get; set; }
        public long rough_cut_id { get; set; }
        public int kapan_id { get; set; }
        public long union_id { get; set; }
        public long receive_union_id { get; set; }
        public long issue_union_id { get; set; }
        public long mix_union_id { get; set; }
        public string receive_date { get; set; }
        public long? rough_sieve_id { get; set; }
        public long? lot_srno { get; set; }
        public int? issue_id { get; set; }
        public int? manager_id { get; set; }
        public int? is_split { get; set; }
        public int? employee_id { get; set; }
        public int? process_id { get; set; }
        public int? sub_process_id { get; set; }
        public decimal? rate { get; set; }
        public decimal? amount { get; set; }
        public int? pcs { get; set; }
        public decimal? carat { get; set; }
        public int? balance_pcs { get; set; }
        public decimal? balance_carat { get; set; }
        public int? form_id { get; set; }
        public bool? is_outstanding { get; set; }
        public int? flag { get; set; }
        public decimal? recieve_carat { get; set; }
        public decimal? loss_carat { get; set; }
        public decimal? plus_carat { get; set; }
        public decimal? total_loss_carat { get; set; }
        public decimal? total_plus_carat { get; set; }
        public long? history_union_id { get; set; }
    }
}
