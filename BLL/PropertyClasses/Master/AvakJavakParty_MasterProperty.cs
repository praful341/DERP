using System;

namespace BLL.PropertyClasses.Master
{
    public class AvakJavakParty_MasterProperty
    {
        public Int64 party_id { get; set; }
        public string date { get; set; }
        public string party_name { get; set; }
        public string address { get; set; }
        public string phone_1 { get; set; }
        public string phone_2 { get; set; }
        public decimal opening_balance { get; set; }
        public Int64 party_group_id { get; set; }
        public string division { get; set; }
        public bool? active { get; set; }
        public string party_group_name { get; set; }
    }
}
