using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.PropertyClasses.Master.MFG
{
    public class MfgMachineItem_MasterProperty
    {
        public Int64 item_id { get; set; }
        public Int64 item_company_id { get; set; }
        public string item_name { get; set; }
        public int sequence_no { get; set; }
        public bool? active { get; set; }
        public string remarks { get; set; }
    }
}
