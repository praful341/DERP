using System;

namespace BLL.PropertyClasses.Master.Store
{
    public class StorePartyMasterProperty
    {
        public Int64 party_id { get; set; }
        public string party_name { get; set; }
        public string contect_person { get; set; }
        public string mobile_no { get; set; }
        public string address { get; set; }
        public Int64 party_group_id { get; set; }
        public string cst_no { get; set; }
        public string gst_no { get; set; }
        public string pan_no { get; set; }
    }
}
