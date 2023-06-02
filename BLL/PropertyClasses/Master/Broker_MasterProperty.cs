namespace BLL.PropertyClasses.Master
{
    public class Broker_MasterProperty
    {
        public int broker_id { get; set; }
        public string broker_type { get; set; }
        public string broker_name { get; set; }
        public decimal? brokerage { get; set; }
        public string address { get; set; }
        public int? city_id { get; set; }
        public string pincode { get; set; }
        public int? state_id { get; set; }
        public int? country_id { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public bool? active { get; set; }
        public string remarks { get; set; }
        public string aadhar_no { get; set; }
        public string pan_no { get; set; }
        public string aadhar_path { get; set; }
        public string pan_path { get; set; }
    }
}
