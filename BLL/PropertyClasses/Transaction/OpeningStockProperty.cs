namespace BLL.PropertyClasses.Transaction
{
    public class OpeningStockProperty
    {
        #region "Master"
        public string opening_date { get; set; }
        public int sieve_id { get; set; }
        public int assort_id { get; set; }
        public int? shape_id { get; set; }
        public int? color_id { get; set; }
        public int? purity_id { get; set; }
        public int? cut_id { get; set; }
        public int? polish_id { get; set; }
        public int? symmetry_id { get; set; }
        public int? fluorescence_id { get; set; }
        public long pcs { get; set; }
        public decimal carat { get; set; }
        public decimal parakhrate { get; set; }
        public decimal parakhamount { get; set; }
        public int currency_id { get; set; }
        public int status_id { get; set; }
        #endregion
    }
}
