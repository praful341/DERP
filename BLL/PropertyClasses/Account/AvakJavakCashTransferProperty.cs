using System;

namespace BLL.PropertyClasses.Account
{
    public class AvakJavakCashTransferProperty
    { 
        public Int64 cash_transfer_id { get; set; }
        public string cash_transfer_date { get; set; }
        public Int64 from_party_id { get; set; }
        public Int64 to_party_id { get; set; }
        public decimal amount { get; set; }
        public string remarks { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string location_id { get; set; }
    }
}
