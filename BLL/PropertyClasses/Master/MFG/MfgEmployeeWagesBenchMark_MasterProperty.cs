using System;

namespace BLL.PropertyClasses.Master.MFG
{
    public class MfgEmployeeWagesBenchMark_MasterProperty
    {
        public Int64 benchmark_id { get; set; }
        public string group_name { get; set; }
        public Int64 rough_sieve_id { get; set; }
        public int? sequence_no { get; set; }
        public string remarks { get; set; }
        public decimal rate { get; set; }
        public decimal size { get; set; }
    }
}
