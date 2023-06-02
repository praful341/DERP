using System;

namespace BLL.PropertyClasses.Master.MFG
{
    public class MfgGroup_MasterProperty
    {
        public Int64 group_id { get; set; }
        public string group_name { get; set; }
        public int sequence_no { get; set; }
        public bool? active { get; set; }
        public string remarks { get; set; }
    }
}
