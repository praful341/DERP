using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.PropertyClasses.Master.MFG
{
    public class MfgMachineParty_MasterProperty
    {
        public Int64 party_id { get; set; }
        public string party_name { get; set; }
        public bool? active { get; set; }
        public string remarks { get; set; }
        public string gst_number { get; set; }
        public long mobile_number { get; set; }
    }
}
