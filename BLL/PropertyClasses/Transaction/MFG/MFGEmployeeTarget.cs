namespace BLL.PropertyClasses.Transaction
{
    public class MFGEmployeeTargetProperty
    {
        public long emp_performance_id { get; set; }
        public string performance_date { get; set; }
        public long? rough_cut_id { get; set; }
        public decimal? target_days { get; set; }
        public decimal? achivement_days { get; set; }
        public long? manager_id { get; set; }
        public long? employee_id { get; set; }
        public long? form_id { get; set; }
        public long? sub_process_id { get; set; }
        public int? company_id { get; set; }
        public int? branch_id { get; set; }
        public int? location_id { get; set; }
        public int? department_id { get; set; }
    }
}
