namespace BLL.PropertyClasses.Master
{
    public class SubProcessRate_MasterProperty
    {
        public long rate_id { get; set; }
        public string rate_date { get; set; }
        public decimal rate { get; set; }
        public int process_id { get; set; }
        public string process { get; set; }
        public int sub_process_id { get; set; }
        public string sub_process { get; set; }
    }
}
