using System;

namespace BLL.PropertyClasses.Master.HR
{
    public class HRRate_MasterProperty
    {
        public Int64 rate_id { get; set; }
        public decimal rate { get; set; }
        public Int64 factory_id { get; set; }
        public Int64 fact_department_id { get; set; }
        public string rate_date { get; set; }
        public string insert_date { get; set; }
    }
}
