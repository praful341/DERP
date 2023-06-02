using System;

namespace BLL.PropertyClasses.Transaction.MFG
{
    public class MFGDepartmentCostingProperty
    {
        public Int64? department_costing_id { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int yearmonth { get; set; }
        public long? department_id { get; set; }
        public long? ledger_id { get; set; }
        public long? lot_srno { get; set; }
        public string lot_srno_list { get; set; }
        public long? opening_pcs { get; set; }
        public long? out_pcs { get; set; }
        public long? in_pcs { get; set; }
        public long? closing_pcs { get; set; }
        public decimal? amount { get; set; }
        public decimal? target { get; set; }
        public string costing_date { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
    }
}
