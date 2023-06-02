using System;

namespace BLL.PropertyClasses.Master.Store
{
    public class MfgPartyGroup_MasterProperty
    {
        public Int64 party_group_id { get; set; }
        public string party_group_name { get; set; }
        public int sequence_no { get; set; }
        public bool? active { get; set; }
        public string remarks { get; set; }
    }
}
