using System;

namespace BLL.PropertyClasses.Master
{
    public class BenchMark_MasterProperty
    {
        public Int64 benchmark_id { get; set; }
        public string benchmark_date { get; set; }
        public Int64 process_id { get; set; }
        public Int64 sub_process_id { get; set; }
        public Int64 company_id { get; set; }
        public Int64 location_id { get; set; }
        public Int64 branch_id { get; set; }
        public Int64 department_id { get; set; }
        public Int64 total_pcs { get; set; }
        public decimal total_carat { get; set; }
    }
}
