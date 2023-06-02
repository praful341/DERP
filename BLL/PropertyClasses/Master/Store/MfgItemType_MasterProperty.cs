using System;

namespace BLL.PropertyClasses.Master.Store
{
    public class MfgItemType_MasterProperty
    {
        public Int64 item_type_id { get; set; }
        public string item_type_name { get; set; }
        public int sequence_no { get; set; }
        public bool? active { get; set; }
        public string remarks { get; set; }
    }
}
