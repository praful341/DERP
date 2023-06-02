namespace BLL.PropertyClasses.Transaction.MFG
{
    public class MFGDepartmentTransferProperty
    {
        public long transfer_id { get; set; }
        public string transfer_date { get; set; }
        public long union_id { get; set; }
        public long lot_srno { get; set; }
        public long? form_id { get; set; }
        public long? manager_id { get; set; }
        public long? wages_sieve_id { get; set; }
        public long? packet_type_wages_id { get; set; }
        public long? to_department_id { get; set; }
        public long? to_manager_id { get; set; }
        public long? cut_id { get; set; }
        public long? lot_id { get; set; }
        public int? pcs { get; set; }
        public decimal? carat { get; set; }
        public int? rr_pcs { get; set; }
        public decimal? rr_carat { get; set; }
        public string receive_date { get; set; }
        public int kapan_id { get; set; }
        public int to_process_id { get; set; }
        public long? history_union_id { get; set; }
        public long? janged_no { get; set; }
        public bool? is_confirm { get; set; }
    }
}
