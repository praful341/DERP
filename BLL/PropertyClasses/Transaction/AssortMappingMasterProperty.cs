using System;

namespace BLL.PropertyClasses.Transaction
{
    public class AssortMappingMasterProperty
    {
        #region "Master"
        public Int64 assort_mapping_id { get; set; }
        public Int64 assort_union_id { get; set; }
        public string assort_mapping_date { get; set; }
        public int from_assort_id { get; set; }
        public int to_assort_id { get; set; }
        public int from_sieve_id { get; set; }
        public int to_sieve_id { get; set; }
        public decimal percentage { get; set; }
        public int company_id { get; set; }
        public int branch_id { get; set; }
        public int location_id { get; set; }
        public int form_id { get; set; }
        #endregion
    }
}
