namespace BLL.PropertyClasses.Transaction
{
    public class SaleReturn_Property
    {
        #region "Master"
        public int company_id { get; set; }
        public int branch_id { get; set; }
        public int location_id { get; set; }
        public int department_id { get; set; }
        public string return_date { get; set; }
        public int delivery_type_id { get; set; }
        public decimal net_amount { get; set; }
        public int form_id { get; set; }
        public int Party_Id { get; set; }
        public int Broker_Id { get; set; }
        public int Term_Days { get; set; }
        public int Add_On_Days { get; set; }
        public string Special_Remark { get; set; }
        public string Client_Remark { get; set; }
        public string Payment_Remark { get; set; }
        public decimal Gross_Amount { get; set; }
        public decimal Brokerage_Per { get; set; }
        public decimal Brokerage_Amt { get; set; }
        public decimal Discount_Per { get; set; }
        public decimal Discount_Amt { get; set; }
        public decimal Interest_Per { get; set; }
        public decimal Interest_Amt { get; set; }
        public decimal Shipping_Charge { get; set; }

        #endregion

        #region "Details"
        public int sale_return_id { get; set; }
        public int return_detail_id { get; set; }
        public int assort_id { get; set; }
        public int sieve_id { get; set; }
        public int sub_sieve_id { get; set; }
        public string invoice_no { get; set; }
        public decimal total_pcs { get; set; }
        public decimal total_carat { get; set; }
        public string remarks { get; set; }
        public int pcs { get; set; }
        public decimal carat { get; set; }
        public decimal rate { get; set; }
        public decimal amount { get; set; }
        public decimal discount { get; set; }
        public decimal old_carat { get; set; }
        public int old_pcs { get; set; }
        public int flag { get; set; }
        public int old_assort_id { get; set; }
        public int old_sieve_id { get; set; }
        public int old_sub_sieve_id { get; set; }
        public decimal current_rate { get; set; }
        public decimal current_amount { get; set; }
        #endregion
    }
}
