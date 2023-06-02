using System;

namespace BLL.PropertyClasses.Master.MFG
{
    public class MfgMiscellaneousEntryMasterProperty
    {
        public int miscellaneous_id { set; get; }
        public string party_name { set; get; }
        public DateTime purchase_date { set; get; }
        public long mobile_number { set; get; }
        public string item_name { set; get; }
        public int qty { set; get; }
        public decimal rate { set; get; }
        public decimal amount { set; get; }

        public string department { set; get; }
        public string remark { set; get; }
        public int branch_id { set; get; }
        public int manager_id { set; get; }
    }
}
