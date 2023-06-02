namespace BLL.PropertyClasses.Transaction
{
    public class BranchTransfer_ConfirmProperty
    {
        public int bt_id { get; set; }
        public string type { get; set; }
        public int assort_id { get; set; }
        public int sieve_id { get; set; }
        public int bt_detail_id { get; set; }
        public int to_company_id { get; set; }
        public int to_branch_id { get; set; }
        public int to_location_id { get; set; }
        public int to_department_id { get; set; }
        public int from_company_id { get; set; }
        public int from_branch_id { get; set; }
        public int from_location_id { get; set; }
        public int from_department_id { get; set; }
        public int pcs { get; set; }
        public decimal carat { get; set; }
        public decimal amount { get; set; }
        public decimal rate { get; set; }
        public decimal discount { get; set; }
        public int currency_id { get; set; }
        public int rate_type_id { get; set; }
        public decimal current_rate { get; set; }
        public decimal current_amount { get; set; }
    }
}
