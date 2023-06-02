using System;

namespace BLL.PropertyClasses.Master.MFG
{
    public class MfgWeightValidation_MasterProperty
    {
        public Int64 weight_id { get; set; }
        public int company_id { get; set; }
        public int branch_id { get; set; }
        public int department_id { get; set; }
        public int location_id { get; set; }
        public int process_id { get; set; }
        public decimal value_from { get; set; }
        public decimal value_to { get; set; }
        public string validation_type { get; set; }
        public bool? active { get; set; }
        public string remarks { get; set; }
    }
}
