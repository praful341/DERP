using System;

namespace BLL.PropertyClasses.Transaction
{
    public class MixSplitProperty
    {
        public Int64 mixsplit_id { get; set; }
        public Int64 mixsplit_srno { get; set; }
        public string mixsplit_date { get; set; }
        public string mixsplit_time { get; set; }
        public int company_id { get; set; }
        public int branch_id { get; set; }
        public int location_id { get; set; }
        public int count { get; set; }
        public int department_id { get; set; }
        public int from_assort_id { get; set; }
        public int from_sieve_id { get; set; }
        public int from_sub_sieve_id { get; set; }
        public int from_pcs { get; set; }
        public decimal from_carat { get; set; }
        public decimal from_rate { get; set; }
        public decimal from_amount { get; set; }
        public int to_assort_id { get; set; }
        public int to_sieve_id { get; set; }
        public int to_sub_sieve_id { get; set; }
        public int assort_id { get; set; }
        public int sieve_id { get; set; }
        public int to_pcs { get; set; }
        public decimal to_carat { get; set; }
        public decimal to_rate { get; set; }
        public decimal to_amount { get; set; }
        public int mixsplit_type_id { get; set; }
        public int currency_id { get; set; }
        public int rate_type_id { get; set; }
        public int transaction_type_id { get; set; }
        public int form_id { get; set; }
        public int user_id { get; set; }
        public string entry_date { get; set; }
        public string entry_time { get; set; }
        public string ip_address { get; set; }
        public string trn_type { get; set; }
        public string type { get; set; }
        public string mixsplit_fromdate { get; set; }
        public string mixsplit_todate { get; set; }
        public string from_assort { get; set; }
        public string to_assort { get; set; }
        public string from_sieve { get; set; }
        public string To_sieve { get; set; }
        public decimal loss_carat { get; set; }
        public decimal carat_plus { get; set; }
        public string company_memo_no { get; set; }
        public string party_memo_no { get; set; }
        public string slip_no { get; set; }
        public Int64 from_invoice_id { get; set; }
    }
}
