using System;

namespace BLL.PropertyClasses.Rejection
{
    public class MFGRejectionPurity_MasterProperty
    {
        public Int64 purity_id { get; set; }
        public string purity_name { get; set; }
        public string group_name { get; set; }
        public decimal? final_rate { get; set; }
        public int? sequence_no { get; set; }
        public bool? active { get; set; }
        public string remarks { get; set; }
    }
}
