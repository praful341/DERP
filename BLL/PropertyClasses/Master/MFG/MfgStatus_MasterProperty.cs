using System;

namespace BLL.PropertyClasses.Master.MFG
{
    public class MfgStatus_MasterProperty
    {
        public Int64 status_id { get; set; }
        public string status_name { get; set; }
        public int sequence_no { get; set; }
        public bool? active { get; set; }
        public string remarks { get; set; }
    }
}
