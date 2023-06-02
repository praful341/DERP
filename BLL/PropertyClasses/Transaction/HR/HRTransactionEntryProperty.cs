using System;

namespace BLL.PropertyClasses.Master.HR
{
    public class HRTransactionEntryProperty
    {
        public Int64 sr_no { get; set; }
        public Int64 union_id { get; set; }

        public Int64 ex_union_id { get; set; }

        public Int64 manager_id { get; set; }
        public Int64 factory_id { get; set; }
        public Int64 fact_department_id { get; set; }
        public Int64 employee_id { get; set; }
        public string transaction_date { get; set; }
        public string transaction_time { get; set; }
        public int total_qty { get; set; }
        public decimal current_rate { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public Int64 book_no { get; set; }
        public string cl_date { get; set; }
    }
}
