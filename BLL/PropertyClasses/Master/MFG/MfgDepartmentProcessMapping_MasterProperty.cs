using System;

namespace BLL.PropertyClasses.Master.MFG
{
    public class MfgDepartmentProcessMapping_MasterProperty
    {
        public Int64 dept_process_mapping_id { get; set; }
        public Int64 process_mapping_id { get; set; }
        public int company_id { get; set; }
        public int branch_id { get; set; }
        public int location_id { get; set; }
        public int department_id { get; set; }
        public int process_id { get; set; }
        public int sub_process_id { get; set; }
        public int sequence_no { get; set; }
        public bool? is_default { get; set; }
        public bool? active { get; set; }
        public string remarks { get; set; }
    }
}
