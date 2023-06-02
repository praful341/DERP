namespace BLL.PropertyClasses.Transaction
{
    public class Memo_RecieveProperty
    {
        #region "Master"
        public int company_id { get; set; }
        public int branch_id { get; set; }
        public int location_id { get; set; }
        public int department_id { get; set; }
        public string memo_date { get; set; }
        public int delivery_type_id { get; set; }
        public int form_id { get; set; }
        public int Party_Id { get; set; }
        public int Broker_Id { get; set; }
        public string Special_Remark { get; set; }
        public string Client_Remark { get; set; }
        public string Payment_Remark { get; set; }
        public int memo_master_id { get; set; }
        public int final_days { get; set; }
        public string final_due_date { get; set; }
        public string due_date { get; set; }

        #endregion

        #region "Details"     
        public int memo_id { get; set; }
        public string memo_no { get; set; }
        public string remarks { get; set; }
        public int assort_id { get; set; }
        public int sieve_id { get; set; }
        public int sub_sieve_id { get; set; }
        public int rec_pcs { get; set; }
        public decimal rec_carat { get; set; }
        public int rej_pcs { get; set; }
        public decimal rej_carat { get; set; }
        public decimal rej_per { get; set; }
        public int loss_pcs { get; set; }
        public decimal loss_carat { get; set; }
        public decimal loss_rate { get; set; }
        public decimal loss_amount { get; set; }
        public decimal rec_rate { get; set; }
        public decimal rec_amount { get; set; }
        public decimal current_rate { get; set; }
        public decimal current_amount { get; set; }
        public int term_days { get; set; }
        public decimal discount_per { get; set; }
        public decimal discount_amount { get; set; }
        public decimal net_amount { get; set; }
        public int flag { get; set; }
        public decimal exchange_rate { get; set; }
        public string rate_type { get; set; }
        public decimal purchase_rate { get; set; }
        public decimal purchase_amount { get; set; }
        #endregion        
    }
}
