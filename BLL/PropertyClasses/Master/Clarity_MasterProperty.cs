using System;

namespace BLL.PropertyClasses.Master
{
    public class Clarity_MasterProperty
    {
        public Int64 purity_id { get; set; }
        public string purity_name { get; set; }
        public int purity_group_id { get; set; }
        public int color_id { get; set; }
        public int sequence_no { get; set; }
        public bool? active { get; set; }
        public string remarks { get; set; }
    }
}
