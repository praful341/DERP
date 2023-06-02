namespace BLL.PropertyClasses.Transaction
{
    public class CostPrice_ImportProperty
    {
        #region "Master"
        public int cost_rate_id { get; set; }
        public int sieve_id { get; set; }
        public int assort_id { get; set; }
        public string cost_rate_date { get; set; }
        public int rate_type_id { get; set; }
        public decimal rate { get; set; }
        public int currency_id { get; set; }
        public int sequence_no { get; set; }
        #endregion
    }
}
